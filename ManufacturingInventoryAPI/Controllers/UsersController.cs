using ManufacturingInventoryAPI.DTOs;
using ManufacturingInventoryAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManufacturingInventoryAPI.Controllers
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


        [HttpPost]
        public async Task<ActionResult<ApiResponseDto<UserResponseDto>>> Create(
                                                        UserCreateDto userDto)
        {
            var user = await _userService.CreateAsync(userDto);

            return Ok(new ApiResponseDto<UserResponseDto>
            {
                Success = true,
                Message = "User created successfully.",
                Data = user
            });
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponseDto<IEnumerable<UserResponseDto>>>> GetAll()
        {
            var users = await _userService.GetAllAsync();

            return Ok(new ApiResponseDto<IEnumerable<UserResponseDto>>
            {
                Success = true,
                Message = "Users fetched successfully.",
                Data = users
            });
        }


        [HttpGet("{username}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponseDto<UserResponseDto>>> GetByUsername(
                                                                        string username)
        {
            var user = await _userService.GetByUsernameAsync(username);

            if (user == null)
            {
                return NotFound(new ApiResponseDto<object>
                {
                    Success = false,
                    Message = "User not found.",
                    Data = null
                });
            }

            return Ok(new ApiResponseDto<UserResponseDto>
            {
                Success = true,
                Message = "User fetched successfully.",
                Data = user
            });
        }

    }
}
