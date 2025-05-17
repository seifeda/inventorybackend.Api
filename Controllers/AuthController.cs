using Microsoft.AspNetCore.Mvc;
using inventorybackend.Api.DTOs.Auth;
using inventorybackend.Api.Interfaces;

namespace inventorybackend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto>> Login(LoginDto loginDto)
        {
            try
            {
                var response = await _authService.LoginAsync(loginDto);
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthResponseDto>> Register(RegisterDto registerDto)
        {
            try
            {
                var response = await _authService.RegisterAsync(registerDto);
                return CreatedAtAction(nameof(Login), new { username = response.Username }, response);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("validate-token")]
        public async Task<ActionResult<bool>> ValidateToken([FromBody] string token)
        {
            var isValid = await _authService.ValidateTokenAsync(token);
            return Ok(isValid);
        }
    }
}
