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

        public AuthController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }


        [HttpPost("login")]
        public IActionResult Login(LoginRequestDto request)
        {
            if (request.Username != "admin" ||
                request.Password != "Admin@123")
            {
                return Unauthorized(new ApiResponseDto<object>
                {
                    Success = false,
                    Message = "Invalid username or password.",
                    Data = null
                });
            }

            var token = _tokenService.GenerateToken(request.Username);

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