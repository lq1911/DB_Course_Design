using BackEnd.DTOs.User;
using BackEnd.Services;
using Microsoft.AspNetCore.Mvc;
namespace BackEnd.Controllers
{
    [Route("api/user/profile")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileService _userService;

        public UserProfileController(IUserProfileService userService)
        {
            _userService = userService;
        }

        [HttpGet("userProfile")]
        public async Task<ActionResult<UserProfileDto>> GetUserProfile([FromQuery] int userId)
        {
            if (userId <= 0)
            {
                return BadRequest("用户ID无效");
            }

            var userProfile = await _userService.GetUserProfileAsync(userId);

            if (userProfile == null)
            {
                return NotFound("用户不存在");
            }

            return Ok(userProfile);
        }
        [HttpGet("address")]
        public async Task<ActionResult<UserAddressDto>> GetUserAddress([FromQuery] int userId)
        {
            if (userId <= 0)
            {
                return BadRequest("用户ID无效");
            }

            var userAddress = await _userService.GetUserAddressAsync(userId);
            
            if (userAddress == null)
            {
                return NotFound("用户地址信息不存在");
            }

            return Ok(userAddress);
        }
    }
}