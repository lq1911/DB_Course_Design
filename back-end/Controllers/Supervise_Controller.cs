using BackEnd.Dtos.ViolationPenalty;
using BackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("api/admin/violation-penalties")]
    [Authorize] // 在控制器级别添加此特性，该控制器下所有方法都需要认证
    public class Supervise_Controller : ControllerBase
    {
        private readonly ISupervise_Service _superviseService;

        public Supervise_Controller(ISupervise_Service superviseService)
        {
            _superviseService = superviseService;
        }

        [HttpGet("mine")]
        public async Task<IActionResult> GetViolationPenaltiesForAdmin()
        {
            // 从 Token 中安全地获取管理员 ID
            var adminIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(adminIdString, out int adminId))
            {
                return Unauthorized("无效的Token");
            }

            var penaltyDtos = await _superviseService.GetViolationPenaltiesForAdminAsync(adminId);

            if (penaltyDtos == null)
            {
                // 如果找不到资源，按照 RESTful 规范返回 404 Not Found
                return NotFound();
            }

            return Ok(penaltyDtos);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateViolationPenalty([FromBody] SetViolationPenaltyInfo request)
        {
            // 添加模型状态检查
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .Select(x => new { Field = x.Key, Errors = x.Value.Errors.Select(e => e.ErrorMessage) })
                    .ToList();

                return BadRequest(new
                {
                    success = false,
                    message = "模型验证失败",
                    errors = errors
                });
            }
            // 验证请求数据
            if (request == null)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "请求数据不能为空"
                });
            }

            // 调用服务层处理业务逻辑
            var result = await _superviseService.UpdateViolationPenaltyAsync(request);

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
