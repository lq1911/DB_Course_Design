using Microsoft.AspNetCore.Mvc;
using BackEnd.Dtos.User;
using System.ComponentModel.DataAnnotations;
using BackEnd.Services.Interfaces;


namespace BackEnd.Controllers
{
    [ApiController]
    [Route("api")]
    public class UserPlaceOrderController : ControllerBase
    {
        private readonly IUserPlaceOrderService _orderService;
        private readonly ILogger<UserPlaceOrderController> _logger;

        public UserPlaceOrderController(IUserPlaceOrderService orderService, ILogger<UserPlaceOrderController> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        /// 创建订单
        [HttpPost("store/order/create")]
        [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseDto>> CreateOrder([FromBody][Required] CreateOrderDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var response = await _orderService.CreateOrderAsync(dto);
                return Ok(response);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new ResponseDto { Success = false, Message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "创建订单时发生错误");
                return StatusCode(500, new ResponseDto { Success = false, Message = "服务器内部错误，创建订单失败" });
            }
        }
        /// <summary>
        /// 更新账户信息
        /// </summary>
        [HttpPut("account/update")]
        [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseDto>> UpdateAccountInfo([FromBody][Required] UpdateAccountDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                
                var response = await _orderService.UpdateAccountAsync(dto);
                return Ok(response);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new ResponseDto { Success = false, Message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "更新账户信息时发生错误");
                return StatusCode(500, new ResponseDto { Success = false, Message = "服务器内部错误，更新账户信息失败" });
            }
        }

        /// <summary>
        /// 新增或更新收货地址
        /// </summary>
        [HttpPut("account/address/save")]
        [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseDto>> SaveOrUpdateAddress([FromBody][Required] SaveAddressDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var response = await _orderService.SaveOrUpdateAddressAsync(dto);
                return Ok(response);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new ResponseDto { Success = false, Message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "保存或更新收货地址时发生错误");
                return StatusCode(500, new ResponseDto { Success = false, Message = "服务器内部错误，保存或更新收货地址失败" });
            }
        }
    }
}
