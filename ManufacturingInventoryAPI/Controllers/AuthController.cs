using ManufacturingInventoryAPI.DTOs;
using ManufacturingInventoryAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ManufacturingInventoryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;

        public AuthController(
            ITokenService tokenService,
            IUserService userService)
        {
            _tokenService = tokenService;
            _userService = userService;
        }


        [HttpPost("login")]
        public async Task<IActionResult>Login(LoginRequestDto request)
        {
            var user = await _userService.ValidateCredentialsAsync(
                                                request.Username,
                                                request.Password);

            if (user == null)
            {
                return Unauthorized(new ApiResponseDto<object>
                {
                    Success = false,
                    Message = "Invalid username or password.",
                    Data = null
                });
            }

            var token = _tokenService.GenerateToken(user.UserName, user.Role);

            return Ok(new ApiResponseDto<LoginResponseDto>
            {
                Success = true,
                Message = "Login successful.",
                Data = new LoginResponseDto
                {
                    Token = token,
                    ExpiresAt = DateTime.UtcNow.AddMinutes(60)
                }
            });
        }
    }
}