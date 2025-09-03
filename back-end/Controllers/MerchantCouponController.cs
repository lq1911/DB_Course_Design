using BackEnd.Dtos.Merchant;
using BackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BackEnd.Controllers
{
    /// <summary>
    /// 商家优惠券管理控制器
    /// </summary>
    [ApiController]
    [Route("api/merchant")]
    public class MerchantCouponController : ControllerBase
    {
        private readonly ICouponService _couponService;
        private readonly ILogger<MerchantCouponController> _logger;

        public MerchantCouponController(ICouponService couponService, ILogger<MerchantCouponController> logger)
        {
            _couponService = couponService;
            _logger = logger;
        }

        /// <summary>
        /// 临时方法：获取当前商家ID（默认值）
        /// TODO: 等登录系统完成后，从用户会话中获取商家ID
        /// </summary>
        private int GetCurrentSellerId()
        {
            // 临时写死为3进行测试，后续需要从JWT Token或Session中获取
            return 3;
        }

        /// <summary>
        /// 获取详细的错误信息
        /// </summary>
        private string GetDetailedErrorMessage(Exception ex)
        {
            var errorDetails = new List<string>();
            
            // 添加主要错误信息
            errorDetails.Add($"错误类型: {ex.GetType().Name}");
            errorDetails.Add($"错误消息: {ex.Message}");
            
            // 处理Entity Framework相关错误
            if (ex is Microsoft.EntityFrameworkCore.DbUpdateException dbEx)
            {
                errorDetails.Add("数据库更新错误:");
                errorDetails.Add($"- 内部异常: {dbEx.InnerException?.GetType().Name}");
                errorDetails.Add($"- 内部消息: {dbEx.InnerException?.Message}");
                
                // 处理Oracle特定错误
                if (dbEx.InnerException is Oracle.ManagedDataAccess.Client.OracleException oracleEx)
                {
                    errorDetails.Add("Oracle数据库错误:");
                    errorDetails.Add($"- 错误代码: {oracleEx.Number}");
                    errorDetails.Add($"- 错误消息: {oracleEx.Message}");
                    
                    // 根据错误代码提供具体说明
                    switch (oracleEx.Number)
                    {
                        case 50032:
                            errorDetails.Add("- 说明: 列包含NULL数据，可能是必填字段未提供值");
                            break;
                        case 1400:
                            errorDetails.Add("- 说明: 必填字段不能为NULL");
                            break;
                        case 2291:
                            errorDetails.Add("- 说明: 外键约束违反，引用的记录不存在");
                            break;
                        case 1:
                            errorDetails.Add("- 说明: 唯一约束违反，数据重复");
                            break;
                        default:
                            errorDetails.Add($"- 说明: Oracle错误代码 {oracleEx.Number}");
                            break;
                    }
                }
            }
            
            // 处理验证错误
            if (ex is ArgumentException argEx)
            {
                errorDetails.Add("参数验证错误:");
                errorDetails.Add($"- 参数名: {argEx.ParamName ?? "未知"}");
                errorDetails.Add($"- 错误消息: {argEx.Message}");
            }
            
            // 添加堆栈跟踪（仅前几行，避免信息过多）
            if (ex.StackTrace != null)
            {
                var stackLines = ex.StackTrace.Split('\n').Take(3);
                errorDetails.Add("堆栈跟踪:");
                foreach (var line in stackLines)
                {
                    errorDetails.Add($"- {line.Trim()}");
                }
            }
            
            return string.Join("; ", errorDetails);
        }

        /// <summary>
        /// 获取优惠券列表（分页）
        /// GET /api/merchant/coupons
        /// </summary>
        [HttpGet("coupons")]
        [ProducesResponseType(typeof(ApiResponse<CouponListResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<CouponListResponseDto>>> GetCoupons(
            [FromQuery, Required] int page = 1,
            [FromQuery, Required] int pageSize = 10)
        {
            try
            {
                _logger.LogInformation("获取优惠券列表请求 - 页码: {Page}, 页大小: {PageSize}", page, pageSize);

                if (page < 1 || pageSize < 1 || pageSize > 100)
                {
                    return BadRequest(new ApiResponse<CouponListResponseDto>
                    {
                        code = 400,
                        message = "页码必须大于0，页大小必须在1-100之间"
                    });
                }

                var sellerId = GetCurrentSellerId();
                var result = await _couponService.GetCouponsAsync(sellerId, page, pageSize);

                return Ok(new ApiResponse<CouponListResponseDto>
                {
                    code = 200,
                    message = "获取优惠券列表成功",
                    data = result
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取优惠券列表失败");
                return StatusCode(500, new ApiResponse<CouponListResponseDto>
                {
                    code = 500,
                    message = "获取优惠券列表失败，请稍后重试"
                });
            }
        }

        /// <summary>
        /// 获取优惠券统计信息
        /// GET /api/merchant/coupons/stats
        /// </summary>
        [HttpGet("coupons/stats")]
        [ProducesResponseType(typeof(ApiResponse<CouponStatsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<CouponStatsDto>>> GetCouponStats()
        {
            try
            {
                _logger.LogInformation("获取优惠券统计信息请求");

                var sellerId = GetCurrentSellerId();
                var result = await _couponService.GetStatsAsync(sellerId);

                return Ok(new ApiResponse<CouponStatsDto>
                {
                    code = 200,
                    message = "获取统计信息成功",
                    data = result
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取优惠券统计信息失败");
                return StatusCode(500, new ApiResponse<CouponStatsDto>
                {
                    code = 500,
                    message = "获取统计信息失败，请稍后重试"
                });
            }
        }

        /// <summary>
        /// 创建优惠券
        /// POST /api/merchant/coupons
        /// </summary>
        [HttpPost("coupons")]
        [ProducesResponseType(typeof(ApiResponse<CreateCouponResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<CreateCouponResponseDto>>> CreateCoupon([FromBody] CreateCouponRequestDto request)
        {
            try
            {
                _logger.LogInformation("创建优惠券请求 - 名称: {Name}, 类型: {Type}", request.name, request.type);

                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                    return BadRequest(new ApiResponse<CreateCouponResponseDto>
                    {
                        code = 400,
                        message = $"请求数据验证失败: {string.Join(", ", errors)}"
                    });
                }

                var sellerId = GetCurrentSellerId();
                var result = await _couponService.CreateCouponAsync(sellerId, request);

                return Ok(new ApiResponse<CreateCouponResponseDto>
                {
                    code = 200,
                    message = "优惠券创建成功",
                    data = result
                });
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "创建优惠券参数错误");
                return BadRequest(new ApiResponse<CreateCouponResponseDto>
                {
                    code = 400,
                    message = ex.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "创建优惠券失败");
                
                // 提供详细的错误信息
                var errorMessage = GetDetailedErrorMessage(ex);
                
                return StatusCode(500, new ApiResponse<CreateCouponResponseDto>
                {
                    code = 500,
                    message = $"创建优惠券失败: {errorMessage}",
                    data = null
                });
            }
        }

        /// <summary>
        /// 更新优惠券
        /// PUT /api/merchant/coupons/{id}
        /// </summary>
        [HttpPut("coupons/{id}")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<object>>> UpdateCoupon(int id, [FromBody] CreateCouponRequestDto request)
        {
            try
            {
                _logger.LogInformation("更新优惠券请求 - ID: {Id}, 名称: {Name}", id, request.name);

                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                    return BadRequest(new ApiResponse<object>
                    {
                        code = 400,
                        message = $"请求数据验证失败: {string.Join(", ", errors)}"
                    });
                }

                var updateRequest = new UpdateCouponRequestDto
                {
                    id = id,
                    name = request.name,
                    type = request.type,
                    value = request.value,
                    minAmount = request.minAmount,
                    discountAmount = request.discountAmount,
                    storeId = request.storeId,
                    totalQuantity = request.totalQuantity,
                    startTime = request.startTime,
                    endTime = request.endTime,
                    description = request.description
                };

                var sellerId = GetCurrentSellerId();
                await _couponService.UpdateCouponAsync(sellerId, updateRequest);

                return Ok(new ApiResponse<object>
                {
                    code = 200,
                    message = "优惠券更新成功"
                });
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "更新优惠券参数错误 - ID: {Id}", id);
                if (ex.Message.Contains("不存在"))
                {
                    return NotFound(new ApiResponse<object>
                    {
                        code = 404,
                        message = ex.Message
                    });
                }
                return BadRequest(new ApiResponse<object>
                {
                    code = 400,
                    message = ex.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "更新优惠券失败 - ID: {Id}", id);
                
                // 提供详细的错误信息
                var errorMessage = GetDetailedErrorMessage(ex);
                
                return StatusCode(500, new ApiResponse<object>
                {
                    code = 500,
                    message = $"更新优惠券失败: {errorMessage}"
                });
            }
        }

        /// <summary>
        /// 删除优惠券
        /// DELETE /api/merchant/coupons/{id}
        /// </summary>
        [HttpDelete("coupons/{id}")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<object>>> DeleteCoupon(int id)
        {
            try
            {
                _logger.LogInformation("删除优惠券请求 - ID: {Id}", id);

                var sellerId = GetCurrentSellerId();
                await _couponService.DeleteCouponAsync(sellerId, id);

                return Ok(new ApiResponse<object>
                {
                    code = 200,
                    message = "优惠券删除成功"
                });
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "删除优惠券参数错误 - ID: {Id}", id);
                return NotFound(new ApiResponse<object>
                {
                    code = 404,
                    message = ex.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "删除优惠券失败 - ID: {Id}", id);
                return StatusCode(500, new ApiResponse<object>
                {
                    code = 500,
                    message = "删除优惠券失败，请稍后重试"
                });
            }
        }

        /// <summary>
        /// 批量删除优惠券
        /// DELETE /api/merchant/coupons/batch
        /// </summary>
        [HttpDelete("coupons/batch")]
        [ProducesResponseType(typeof(ApiResponse<BatchDeleteResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<BatchDeleteResponseDto>>> BatchDeleteCoupons([FromBody] BatchDeleteCouponsRequestDto request)
        {
            try
            {
                _logger.LogInformation("批量删除优惠券请求 - 数量: {Count}", request.ids?.Count ?? 0);

                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                    return BadRequest(new ApiResponse<BatchDeleteResponseDto>
                    {
                        code = 400,
                        message = $"请求数据验证失败: {string.Join(", ", errors)}"
                    });
                }

                var sellerId = GetCurrentSellerId();
                var result = await _couponService.BatchDeleteCouponsAsync(sellerId, request);

                return Ok(new ApiResponse<BatchDeleteResponseDto>
                {
                    code = 200,
                    message = $"成功删除 {result.deletedCount} 张优惠券",
                    data = result
                });
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "批量删除优惠券参数错误");
                return BadRequest(new ApiResponse<BatchDeleteResponseDto>
                {
                    code = 400,
                    message = ex.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "批量删除优惠券失败");
                return StatusCode(500, new ApiResponse<BatchDeleteResponseDto>
                {
                    code = 500,
                    message = "批量删除优惠券失败，请稍后重试"
                });
            }
        }
    }

    /// <summary>
    /// 统一API响应格式
    /// </summary>
    public class ApiResponse<T>
    {
        public int code { get; set; }
        public string message { get; set; } = string.Empty;
        public T? data { get; set; }
    }
}
