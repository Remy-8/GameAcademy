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
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserResponseDto>>> GetUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponseDto>> GetUser(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);

            if (user == null)
                return NotFound($"No se encontró el usuario con ID {id}");

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<UserResponseDto>> CreateUser(CreateUserDto createUserDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var user = await _userService.CreateUserAsync(createUserDto);
                return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserResponseDto>> UpdateUser(int id, UpdateUserDto updateUserDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var user = await _userService.UpdateUserAsync(id, updateUserDto);

                if (user == null)
                    return NotFound($"No se encontró el usuario con ID {id}");

                return Ok(user);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var deleted = await _userService.DeleteUserAsync(id);

            if (!deleted)
                return NotFound($"No se encontró el usuario con ID {id}");

            return NoContent();
        }

        [HttpGet("top/{count}")]
        public async Task<ActionResult<IEnumerable<UserResponseDto>>> GetTopUsers(int count)
        {
            var users = await _userService.GetTopUsersAsync(count);
            return Ok(users);
        }
    }
}