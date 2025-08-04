using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackEnd.Dtos.Merchant;
using BackEnd.Services.Interfaces;
using BackEnd.Data;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("api/merchant")]
    public class MerchantController : ControllerBase
    {
        private readonly IMerchantService _merchantService;
        private readonly AppDbContext _context;

        public MerchantController(IMerchantService merchantService, AppDbContext context)
        {
            _merchantService = merchantService;
            _context = context;
        }

        // 临时方法：写死商家ID为1，用于测试
        private int GetCurrentSellerId()
        {
            // TODO: 等登录系统完成后，从用户会话中获取商家ID
            return 1; // 临时写死为1进行测试
        }

        // GET: api/merchant/shop/overview
        [HttpGet("shop/overview")]
        public async Task<ActionResult<ShopOverviewResponseDto>> GetShopOverview()
        {
            try
            {
                Console.WriteLine("=== 开始处理店铺概览请求 ===");
                var sellerId = GetCurrentSellerId();
                Console.WriteLine($"当前商家ID: {sellerId}");
                
                var result = await _merchantService.GetShopOverviewAsync(sellerId);
                Console.WriteLine($"店铺概览数据: {System.Text.Json.JsonSerializer.Serialize(result)}");
                
                return Ok(new { data = result });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"=== 店铺概览请求异常 ===");
                Console.WriteLine($"异常类型: {ex.GetType().Name}");
                Console.WriteLine($"异常消息: {ex.Message}");
                Console.WriteLine($"堆栈跟踪: {ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"内部异常: {ex.InnerException.Message}");
                }
                return StatusCode(500, new { error = ex.Message, details = ex.StackTrace });
            }
        }

        // GET: api/shop/overview (为了兼容前端)
        [HttpGet("/api/shop/overview")]
        public async Task<ActionResult<ShopOverviewResponseDto>> GetShopOverviewForFrontend()
        {
            try
            {
                Console.WriteLine("=== 开始处理前端店铺概览请求 ===");
                var sellerId = GetCurrentSellerId();
                Console.WriteLine($"当前商家ID: {sellerId}");
                
                var result = await _merchantService.GetShopOverviewAsync(sellerId);
                Console.WriteLine($"店铺概览数据: {System.Text.Json.JsonSerializer.Serialize(result)}");
                
                return Ok(new { data = result });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"=== 前端店铺概览请求异常 ===");
                Console.WriteLine($"异常类型: {ex.GetType().Name}");
                Console.WriteLine($"异常消息: {ex.Message}");
                Console.WriteLine($"堆栈跟踪: {ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"内部异常: {ex.InnerException.Message}");
                }
                return StatusCode(500, new { error = ex.Message, details = ex.StackTrace });
            }
        }

        // GET: api/merchant/shop/info
        [HttpGet("shop/info")]
        public async Task<ActionResult<ShopInfoResponseDto>> GetShopInfo()
        {
            try
            {
                Console.WriteLine("=== 开始处理店铺信息请求 ===");
                var sellerId = GetCurrentSellerId();
                Console.WriteLine($"当前商家ID: {sellerId}");
                
                var result = await _merchantService.GetShopInfoAsync(sellerId);
                Console.WriteLine($"店铺信息数据: {System.Text.Json.JsonSerializer.Serialize(result)}");
                return Ok(new { data = result });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"=== 店铺信息请求异常 ===");
                Console.WriteLine($"异常类型: {ex.GetType().Name}");
                Console.WriteLine($"异常消息: {ex.Message}");
                Console.WriteLine($"堆栈跟踪: {ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"内部异常: {ex.InnerException.Message}");
                }
                return StatusCode(500, new { error = ex.Message, details = ex.StackTrace });
            }
        }

        // GET: api/shop/info (为了兼容前端)
        [HttpGet("/api/shop/info")]
        public async Task<ActionResult<ShopInfoResponseDto>> GetShopInfoForFrontend()
        {
            try
            {
                Console.WriteLine("=== 开始处理前端店铺信息请求 ===");
                var sellerId = GetCurrentSellerId();
                Console.WriteLine($"当前商家ID: {sellerId}");
                
                var result = await _merchantService.GetShopInfoAsync(sellerId);
                Console.WriteLine($"店铺信息数据: {System.Text.Json.JsonSerializer.Serialize(result)}");
                return Ok(new { data = result });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"=== 前端店铺信息请求异常 ===");
                Console.WriteLine($"异常类型: {ex.GetType().Name}");
                Console.WriteLine($"异常消息: {ex.Message}");
                Console.WriteLine($"堆栈跟踪: {ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"内部异常: {ex.InnerException.Message}");
                }
                return StatusCode(500, new { error = ex.Message, details = ex.StackTrace });
            }
        }

        // GET: api/merchant/info
        [HttpGet("info")]
        public async Task<ActionResult<MerchantInfoResponseDto>> GetMerchantInfo()
        {
            try
            {
                Console.WriteLine("=== 开始处理商家信息请求 ===");
                var sellerId = GetCurrentSellerId();
                Console.WriteLine($"当前商家ID: {sellerId}");
                
                var result = await _merchantService.GetMerchantInfoAsync(sellerId);
                Console.WriteLine($"商家信息数据: {System.Text.Json.JsonSerializer.Serialize(result)}");
                return Ok(new { data = result });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"=== 商家信息请求异常 ===");
                Console.WriteLine($"异常类型: {ex.GetType().Name}");
                Console.WriteLine($"异常消息: {ex.Message}");
                Console.WriteLine($"堆栈跟踪: {ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"内部异常: {ex.InnerException.Message}");
                }
                return StatusCode(500, new { error = ex.Message, details = ex.StackTrace });
            }
        }

        // POST: api/merchant/shop/status
        [HttpPost("shop/status")]
        public async Task<ActionResult<CommonResponseDto>> ToggleBusinessStatus([FromBody] ToggleBusinessStatusRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var sellerId = GetCurrentSellerId();
                var result = await _merchantService.ToggleBusinessStatusAsync(sellerId, request);
                return Ok(new { data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // POST: api/merchant/shop/update-field
        [HttpPost("shop/update-field")]
        public async Task<ActionResult<CommonResponseDto>> UpdateShopField([FromBody] UpdateShopFieldRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var sellerId = GetCurrentSellerId();
                var result = await _merchantService.UpdateShopFieldAsync(sellerId, request);
                return Ok(new { data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // GET: api/merchant/test-db-connection
        [HttpGet("test-db-connection")]
        public async Task<ActionResult> TestDatabaseConnection()
        {
            try
            {
                var canConnect = await _context.Database.CanConnectAsync();
                
                if (canConnect)
                {
                    var storeCount = await _context.Stores.CountAsync();
                    var sellerCount = await _context.Sellers.CountAsync();
                    
                    return Ok(new { 
                        success = true, 
                        message = "数据库连接成功",
                        storeCount = storeCount,
                        sellerCount = sellerCount
                    });
                }
                else
                {
                    return StatusCode(500, new { 
                        success = false, 
                        message = "无法连接到数据库" 
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    success = false, 
                    message = "数据库连接错误", 
                    error = ex.Message 
                });
            }
        }
    }
} 