using Microsoft.AspNetCore.Mvc;
using inventorybackend.Api.DTOs.User;
using inventorybackend.Api.Interfaces.Services;

namespace inventorybackend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetById(int id)
        {
            try
            {
                var user = await _userService.GetByIdAsync(id);
                return Ok(user);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet("username/{username}")]
        public async Task<ActionResult<UserDto>> GetByUsername(string username)
        {
            try
            {
                var user = await _userService.GetByUsernameAsync(username);
                return Ok(user);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet("email/{email}")]
        public async Task<ActionResult<UserDto>> GetByEmail(string email)
        {
            try
            {
                var user = await _userService.GetByEmailAsync(email);
                return Ok(user);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetActiveUsers()
        {
            var users = await _userService.GetActiveUsersAsync();
            return Ok(users);
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> Create(CreateUserDto createDto)
        {
            try
            {
                var user = await _userService.CreateAsync(createDto);
                return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserDto>> Update(int id, UpdateUserDto updateDto)
        {
            try
            {
                var user = await _userService.UpdateAsync(id, updateDto);
                return Ok(user);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _userService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPatch("{id}/status")]
        public async Task<ActionResult> UpdateStatus(int id, [FromBody] bool isActive)
        {
            try
            {
                await _userService.UpdateStatusAsync(id, isActive);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPatch("{id}/password")]
        public async Task<ActionResult> UpdatePassword(int id, [FromBody] PasswordUpdateDto passwordUpdate)
        {
            try
            {
                await _userService.UpdatePasswordAsync(id, passwordUpdate.CurrentPassword, passwordUpdate.NewPassword);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

    public class PasswordUpdateDto
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
