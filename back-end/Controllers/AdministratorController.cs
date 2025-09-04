using BackEnd.Dtos.Administrator;
using BackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("api/admin/info")]
    [Authorize] // 在控制器级别添加此特性，该控制器下所有方法都需要认证
    public class AdminController : ControllerBase
    {
        private readonly IAdministratorService _administratorService;

        public AdminController(IAdministratorService administratorService)
        {
            _administratorService = administratorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAdministratorInfo()
        {
            // 从 Token 中安全地获取管理员 ID
            var adminIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(adminIdString, out int adminId))
            {
                return Unauthorized("无效的Token");
            }

            var adminInfo = await _administratorService.GetAdministratorInfoAsync(adminId);

            if (adminInfo == null)
            {
                // 如果找不到资源，按照 RESTful 规范返回 404 Not Found
                return NotFound("管理员信息未找到");
            }

            return Ok(adminInfo);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAdministratorInfo([FromBody] GetAdminInfo request)
        {
            // 验证请求数据
            if (request == null)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "请求数据不能为空"
                });
            }

            // 从 Token 中安全地获取管理员 ID
            var adminIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(adminIdString, out int adminId))
            {
                return Unauthorized("无效的Token");
            }

            // 调用服务层处理业务逻辑
            var result = await _administratorService.UpdateAdministratorInfoAsync(adminId, request);

            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}

