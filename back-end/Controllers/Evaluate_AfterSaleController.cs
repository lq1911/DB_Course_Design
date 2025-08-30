using BackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("api/admin/after-sales")]
    [Authorize] // 在控制器级别添加此特性，该控制器下所有方法都需要认证
    public class Evaluate_AfterSaleController : ControllerBase
    {
        private readonly IEvaluate_AfterSaleService _evaluateAfterSaleService;

        public Evaluate_AfterSaleController(IEvaluate_AfterSaleService evaluateAfterSaleService)
        {
            _evaluateAfterSaleService = evaluateAfterSaleService;
        }

        [HttpGet("mine")]
        public async Task<IActionResult> GetAfterSaleApplicationsForAdmin()
        {
            // 从 Token 中安全地获取管理员 ID
            var adminIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(adminIdString, out int adminId))
            {
                return Unauthorized("无效的Token");
            }

            var applicationDtos = await _evaluateAfterSaleService.GetApplicationsForAdminAsync(adminId);

            if (applicationDtos == null)
            {
                // 如果找不到资源，按照 RESTful 规范返回 404 Not Found
                return NotFound();
            }

            return Ok(applicationDtos);
        }
    }
}
