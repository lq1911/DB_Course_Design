using Microsoft.AspNetCore.Mvc;
using BackEnd.Dtos.Dish;
using BackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("api/dishes")]
    public class DishesController : ControllerBase
    {
        private readonly IDishService _dishService;

        public DishesController(IDishService dishService)
        {
            _dishService = dishService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDishes([FromQuery] int? sellerId)
        {
            try
            {
                if (sellerId.HasValue && sellerId == 3)
                {
                    var dishes = await _dishService.GetAllDishesAsync();

                    // 确保返回的是数组
                    var dishDtos = dishes?.Select(d => new DishDto
                    {
                        DishId = d.DishId,
                        DishName = d.DishName,
                        Price = d.Price,
                        Description = d.Description,
                        IsSoldOut = (int)d.IsSoldOut,
                    }).ToList() ?? new List<DishDto>();

                    return Ok(dishDtos);
                }
                else
                {
                    // 返回空数组而不是单个对象
                    return Ok(new List<DishDto>());
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { code = 400, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateDish([FromBody] CreateDishDto dto)
        {
            try
            {
                var dish = await _dishService.CreateDishAsync(dto);
                return CreatedAtAction(nameof(GetDishById), new { dishId = dish.DishId }, dish);
            }
            catch (Exception ex)
            {
                return BadRequest(new { code = 400, message = ex.Message });
            }
        }

        [HttpGet("{dishId}")]
        public async Task<IActionResult> GetDishById(int dishId)
        {
            try
            {
                var dish = await _dishService.GetDishByIdAsync(dishId);
                return dish == null ? NotFound(new { code = 404, message = "菜品不存在" }) : Ok(dish);
            }
            catch (Exception ex)
            {
                return BadRequest(new { code = 400, message = ex.Message });
            }
        }

        [HttpPatch("{dishId}")]
        public async Task<IActionResult> UpdateDish(int dishId, [FromBody] UpdateDishDto dto)
        {
            try
            {
                var updated = await _dishService.UpdateDishAsync(dishId, dto);
                return updated == null ? NotFound(new { code = 404, message = "菜品不存在" }) : Ok(updated);
            }
            catch (Exception ex)
            {
                return BadRequest(new { code = 400, message = ex.Message });
            }
        }

        [HttpPatch("{dishId}/soldout")]
        public async Task<IActionResult> ToggleSoldOut(int dishId, [FromBody] ToggleSoldOutDto dto)
        {
            try
            {
                var result = await _dishService.ToggleSoldOutAsync(dishId, dto.IsSoldOut);
                return !result.Success ? BadRequest(new { code = 400, message = result.Message }) : Ok(result.Data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { code = 400, message = ex.Message });
            }
        }
    }
}