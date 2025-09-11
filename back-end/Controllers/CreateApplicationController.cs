using BackEnd.Dtos.AfterSaleApplication;
using BackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("api/user/applications")]
    [Authorize] // 在控制器级别添加此特性，该控制器下所有方法都需要认证
    public class CreateApplicationController : ControllerBase
    {
        private readonly ICreateApplicationService _createApplicationService;

        public CreateApplicationController(ICreateApplicationService createApplicationService)
        {
            _createApplicationService = createApplicationService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateApplication([FromBody] CreateApplicationDto request)
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

            // 从 Token 中安全地获取用户 ID
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(userIdString, out int userId))
            {
                return Unauthorized("无效的Token");
            }

            // 调用服务层处理业务逻辑
            var result = await _createApplicationService.CreateApplicationAsync(request, userId);

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
