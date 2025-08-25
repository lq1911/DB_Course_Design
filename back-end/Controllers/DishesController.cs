using Microsoft.AspNetCore.Mvc;
using BackEnd.Dtos.Dish;
using BackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("api/dishes")]
    [Authorize(Roles = "Seller")]
    public class DishesController : ControllerBase
    {
        private readonly IDishService _dishService;

        public DishesController(IDishService dishService)
        {
            _dishService = dishService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDishes()
        {
            try
            {
                var dishes = await _dishService.GetAllDishesAsync();
                return Ok(dishes);
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