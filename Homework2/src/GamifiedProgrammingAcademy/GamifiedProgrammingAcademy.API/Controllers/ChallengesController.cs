using Microsoft.AspNetCore.Mvc;
using GamifiedProgrammingAcademy.API.Contracts;
using GamifiedProgrammingAcademy.API.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class ChallengesController : ControllerBase
{
    private readonly IChallengeService _challengeService;

    public ChallengesController(IChallengeService challengeService)
    {
        _challengeService = challengeService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ChallengeResponseDto>>> GetChallenges()
    {
        var challenges = await _challengeService.GetAllChallengesAsync();
        return Ok(challenges);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ChallengeResponseDto>> GetChallenge(int id)
    {
        var challenge = await _challengeService.GetChallengeByIdAsync(id);

        if (challenge == null)
            return NotFound($"No se encontró el desafío con ID {id}");

        return Ok(challenge);
    }

    [HttpPost]
    public async Task<ActionResult<ChallengeResponseDto>> CreateChallenge(CreateChallengeDto createChallengeDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var challenge = await _challengeService.CreateChallengeAsync(createChallengeDto);
            return CreatedAtAction(nameof(GetChallenge), new { id = challenge.Id }, challenge);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ChallengeResponseDto>> UpdateChallenge(int id, UpdateChallengeDto updateChallengeDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var challenge = await _challengeService.UpdateChallengeAsync(id, updateChallengeDto);

            if (challenge == null)
                return NotFound($"No se encontró el desafío con ID {id}");

            return Ok(challenge);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteChallenge(int id)
    {
        var deleted = await _challengeService.DeleteChallengeAsync(id);

        if (!deleted)
            return NotFound($"No se encontró el desafío con ID {id}");

        return NoContent();
    }
}