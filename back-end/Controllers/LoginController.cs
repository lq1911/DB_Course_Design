using BackEnd.Dtos.AuthRequest;
using BackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _loginService.LoginAsync(request);

            if (result.Success)
                return Ok(result);
            else
                return Unauthorized(result);
        }

        [HttpPost("logout")]
        [Authorize] // 确保只有携带有效Token的用户才能访问此接口
        public async Task<IActionResult> Logout()
        {
            try
            {
                // 从Token中解析出用户ID (这和你其他接口的做法一样)
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
                {
                    return Unauthorized("无效的Token");
                }

                await _loginService.LogoutAsync(userId);

                return Ok(new { message = "登出成功" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"服务器内部错误: {ex.Message}");
            }
        }
    }
}

