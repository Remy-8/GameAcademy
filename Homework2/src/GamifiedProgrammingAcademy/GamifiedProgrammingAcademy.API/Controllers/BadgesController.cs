#nullable disable
using Microsoft.AspNetCore.Mvc;
using GamifiedProgrammingAcademy.API.Contracts;
using GamifiedProgrammingAcademy.API.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GamifiedProgrammingAcademy.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BadgesController : ControllerBase
    {
        private readonly IBadgeService _badgeService;

        public BadgesController(IBadgeService badgeService)
        {
            _badgeService = badgeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BadgeResponseDto>>> GetBadges()
        {
            var badges = await _badgeService.GetAllBadgesAsync();
            return Ok(badges);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BadgeResponseDto>> GetBadge(int id)
        {
            var badge = await _badgeService.GetBadgeByIdAsync(id);

            if (badge == null)
                return NotFound($"No se encontró el badge con ID {id}");

            return Ok(badge);
        }

        [HttpPost]
        public async Task<ActionResult<BadgeResponseDto>> CreateBadge(CreateBadgeDto createBadgeDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var badge = await _badgeService.CreateBadgeAsync(createBadgeDto);
                return CreatedAtAction(nameof(GetBadge), new { id = badge.Id }, badge);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BadgeResponseDto>> UpdateBadge(int id, UpdateBadgeDto updateBadgeDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var badge = await _badgeService.UpdateBadgeAsync(id, updateBadgeDto);

                if (badge == null)
                    return NotFound($"No se encontró el badge con ID {id}");

                return Ok(badge);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBadge(int id)
        {
            var deleted = await _badgeService.DeleteBadgeAsync(id);

            if (!deleted)
                return NotFound($"No se encontró el badge con ID {id}");

            return NoContent();
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<BadgeResponseDto>>> GetUserBadges(int userId)
        {
            var badges = await _badgeService.GetUserBadgesAsync(userId);
            return Ok(badges);
        }

        [HttpPost("award")]
        public async Task<IActionResult> AwardBadge([FromBody] AwardBadgeDto request)
        {
            var success = await _badgeService.AwardBadgeToUserAsync(request.UserId, request.BadgeId);

            if (!success)
                return BadRequest("No se pudo otorgar el badge al usuario");

            return Ok("Badge otorgado exitosamente");
        }
    }
}