using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using BackEnd.Dtos.AfterSale;
using BackEnd.Services.Interfaces;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("api/aftersale")]
    public class AfterSalesController : ControllerBase
    {
        private readonly IAfterSaleService _afterSaleService;

        public AfterSalesController(IAfterSaleService afterSaleService)
        {
            _afterSaleService = afterSaleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAfterSales([FromQuery] int page, [FromQuery] int pageSize, [FromQuery] string? keyword)
        {
            if (page < 1 || pageSize < 1)
            {
                return BadRequest(new { code = 400, message = "页码和每页数量必须大于0" });
            }

            var result = await _afterSaleService.GetAfterSalesAsync(page, pageSize, keyword);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAfterSaleById(int id)
        {
            var afterSale = await _afterSaleService.GetAfterSaleByIdAsync(id);
            if (afterSale == null)
            {
                return NotFound(new { code = 404, message = "售后申请不存在" });
            }
            return Ok(afterSale);
        }

        [HttpPost("{id}/decide")]
        public async Task<IActionResult> ProcessAfterSale(int id, [FromBody] ProcessAfterSaleDto processDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { code = 400, message = "请求参数错误" });
            }

            if (processDto.Action != "approve" && processDto.Action != "reject" && processDto.Action != "negotiate")
            {
                return BadRequest(new { code = 400, message = "无效的处理动作" });
            }

            var result = await _afterSaleService.ProcessAfterSaleAsync(id, processDto);
            return Ok(result);
        }
    }
}