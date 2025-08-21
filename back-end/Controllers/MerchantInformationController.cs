using Microsoft.AspNetCore.Mvc;
using BackEnd.Dtos.MerchantInfo;
using BackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Seller")] // 仅商家角色可访问
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
            // 从认证信息中获取商家ID
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            
            var result = await _merchantInformationService.GetMerchantInfoAsync(userId);
            if (!result.Success)
                return BadRequest(new { code = 400, success = false, message = result.Message });

            return Ok(new { code = 200, success = true, data = result.Data });
        }

        // 更新商家信息接口
        [HttpPut("profile")]
        public async Task<IActionResult> SaveShopInfo([FromBody] UpdateMerchantProfileDto dto)
        {
            // 从认证信息中获取商家ID
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            
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