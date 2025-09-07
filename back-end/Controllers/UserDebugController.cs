using BackEnd.Dtos.User;
using BackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("api")]
    [Produces("application/json")]
    public class UserDebugController : ControllerBase
    {
        private readonly IUserDebugService _userDebugService;

        public UserDebugController(IUserDebugService userDebugService)
        {
            _userDebugService = userDebugService;
        }

        /// <summary>
        /// 1. 用户简介接口
        /// 根据用户ID获取个人信息（昵称、电话、头像、默认地址）
        /// </summary>
        /// <param name="userId">用户ID</param>
        [HttpGet("user/home/userinfo")]
        [ProducesResponseType(typeof(UserInfoResponseDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<UserInfoResponseDto>> GetUserInfo([FromQuery] int userId)
        {
            var result = await _userDebugService.GetUserInfoAsync(userId);
            return Ok(result);
        }

        /// <summary>
        /// 2. 提交订单接口
        /// 提交购物车生成订单，并记录支付时间、用户信息及店铺信息
        /// </summary>
        /// <param name="dto">订单提交信息</param>
        [HttpPost("store/checkout")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SubmitOrder([FromBody] SubmitOrderRequestDto dto)
        {
            await _userDebugService.SubmitOrderAsync(dto);
            return Ok(new { Message = "订单提交成功" });
        }

        /// <summary>
        /// 3. 获取用户ID接口
        /// 根据手机号或邮箱获取对应的用户ID
        /// </summary>
        /// <param name="dto">包含手机号或邮箱的请求对象</param>
        [HttpPost("getid")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(GetUserIdResponseDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<GetUserIdResponseDto>> GetUserId([FromBody] GetUserIdRequestDto dto)
        {
            var result = await _userDebugService.GetUserIdAsync(dto);
            return Ok(result);
        }
    }
}
