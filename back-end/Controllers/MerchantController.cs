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

        // 临时方法：写死商家ID为3，用于测试
        private int GetCurrentSellerId()
        {
            // TODO: 等登录系统完成后，从用户会话中获取商家ID
            return 3; // 临时写死为3进行测试
        }

        // GET: api/shop/overview (前端需要的接口)
        [HttpGet("/api/shop/overview")]
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

        // GET: api/shop/info (前端需要的接口)
        [HttpGet("/api/shop/info")]
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

        // GET: api/merchant/info (前端需要的接口)
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

        // PATCH: api/shop/status (前端需要的接口)
        [HttpPatch("/api/shop/status")]
        public async Task<ActionResult<CommonResponseDto>> ToggleBusinessStatus([FromBody] ToggleBusinessStatusRequestDto request)
        {
            try
            {
                Console.WriteLine("=== Controller层: 开始处理切换营业状态请求 ===");
                Console.WriteLine($"请求数据: IsOpen={request.IsOpen}");
                
                if (!ModelState.IsValid)
                {
                    Console.WriteLine("=== Controller层: 模型验证失败 ===");
                    foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                    {
                        Console.WriteLine($"验证错误: {error.ErrorMessage}");
                    }
                    return BadRequest(ModelState);
                }

                Console.WriteLine("=== Controller层: 模型验证通过 ===");
                var sellerId = GetCurrentSellerId();
                Console.WriteLine($"当前商家ID: {sellerId}");
                
                Console.WriteLine("=== Controller层: 调用Service层切换营业状态 ===");
                var result = await _merchantService.ToggleBusinessStatusAsync(sellerId, request);
                Console.WriteLine($"Service层返回结果: {System.Text.Json.JsonSerializer.Serialize(result)}");
                
                return Ok(new { data = result });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"=== Controller层: 切换营业状态异常 ===");
                Console.WriteLine($"异常类型: {ex.GetType().Name}");
                Console.WriteLine($"异常消息: {ex.Message}");
                Console.WriteLine($"异常堆栈: {ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"内部异常: {ex.InnerException.Message}");
                }
                return StatusCode(500, new { error = ex.Message, details = ex.StackTrace });
            }
        }

        // PATCH: api/shop/update-field (前端需要的接口)
        [HttpPatch("/api/shop/update-field")]
        public async Task<ActionResult<CommonResponseDto>> UpdateShopField([FromBody] UpdateShopFieldRequestDto request)
        {
            try
            {
                Console.WriteLine("=== Controller层: 开始处理更新店铺字段请求 ===");
                Console.WriteLine($"请求数据: Field={request.Field}, Value={request.Value}");
                
                if (!ModelState.IsValid)
                {
                    Console.WriteLine("=== Controller层: 模型验证失败 ===");
                    foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                    {
                        Console.WriteLine($"验证错误: {error.ErrorMessage}");
                    }
                    return BadRequest(ModelState);
                }

                Console.WriteLine("=== Controller层: 模型验证通过 ===");
                var sellerId = GetCurrentSellerId();
                Console.WriteLine($"当前商家ID: {sellerId}");
                
                Console.WriteLine("=== Controller层: 调用Service层更新店铺字段 ===");
                var result = await _merchantService.UpdateShopFieldAsync(sellerId, request);
                Console.WriteLine($"Service层返回结果: {System.Text.Json.JsonSerializer.Serialize(result)}");
                
                return Ok(new { data = result });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"=== Controller层: 更新店铺字段异常 ===");
                Console.WriteLine($"异常类型: {ex.GetType().Name}");
                Console.WriteLine($"异常消息: {ex.Message}");
                Console.WriteLine($"异常堆栈: {ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"内部异常: {ex.InnerException.Message}");
                }
                return StatusCode(500, new { error = ex.Message, details = ex.StackTrace });
            }
        }

        // GET: api/merchant/test-db-connection (保留用于调试)
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