using BackEnd.DTOs.User;
using BackEnd.Services;
using Microsoft.AspNetCore.Mvc;
namespace BackEnd.Controllers
{
    [Route("api/user/profile")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserProfileController(IUserService userService)
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
    }
}