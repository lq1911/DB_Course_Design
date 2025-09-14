using Microsoft.AspNetCore.Mvc;
using BackEnd.Dtos.MerchantInfo;
using BackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("api/merchant")]
    [Authorize]
    public class MerchantInformationController : ControllerBase
    {
        private readonly IMerchantInformationService _merchantInformationService;

        public MerchantInformationController(IMerchantInformationService merchantInformationService)
        {
            _merchantInformationService = merchantInformationService;
        }

        // 获取商家信息接口
        [HttpGet("profile")]
        public async Task<IActionResult> FetchMerchantInfo()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(userIdString, out int userId))
            {
                return Unauthorized("无效的Token");
            }

            var result = await _merchantInformationService.GetMerchantInfoAsync(userId);
            if (!result.Success)
                return BadRequest(new { code = 400, success = false, message = result.Message });

            return Ok(new { code = 200, success = true, data = result.Data });
        }

        // 更新商家信息接口
        [HttpPut("profile")]
        public async Task<IActionResult> SaveShopInfo([FromBody] UpdateMerchantProfileDto dto)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(userIdString, out int userId))
            {
                return Unauthorized("无效的Token");
            }

            var result = await _merchantInformationService.UpdateMerchantInfoAsync(userId, dto);
            if (!result.Success)
                return BadRequest(new { code = 400, success = false, message = result.Message });

            return Ok(new
            {
                code = 200,
                success = true,
                message = result.Message,
                data = result.Data
            });
        }
    }
}
