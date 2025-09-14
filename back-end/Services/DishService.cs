using BackEnd.Dtos.Dish;
using BackEnd.Models;
using BackEnd.Models.Enums;
using BackEnd.Repositories.Interfaces;
using BackEnd.Services.Interfaces;

namespace BackEnd.Services
{
    public class DishService : IDishService
    {
        private readonly IDishRepository _dishRepo;

        public DishService(IDishRepository dishRepo)
        {
            _dishRepo = dishRepo;
        }

        public async Task<IEnumerable<DishDto>> GetAllDishesAsync()
        {
            var dishes = await _dishRepo.GetAllAsync();
            return dishes.Select(d => new DishDto
            {
                DishId = d.DishID,
                DishName = d.DishName,
                Price = d.Price,
                Description = d.Description,
                IsSoldOut = (int)d.IsSoldOut,
                SellerID = d.SellerID
            });
        }

        public async Task<IEnumerable<DishDto>> GetDishesBySellerIdAsync(int sellerId)
        {
            var dishes = await _dishRepo.GetBySellerIdAsync(sellerId);
            return dishes.Select(d => new DishDto
            {
                DishId = d.DishID,
                DishName = d.DishName,
                Price = d.Price,
                Description = d.Description,
                IsSoldOut = (int)d.IsSoldOut,
                SellerID = d.SellerID
            });
        }

        public async Task<DishDto> CreateDishAsync(CreateDishDto dto)
        {
            var dish = new Dish
            {
                DishName = dto.DishName,
                Price = dto.Price,
                Description = dto.Description,
                IsSoldOut = (DishIsSoldOut)dto.IsSoldOut,
                SellerID = dto.SellerID
            };

            await _dishRepo.AddAsync(dish);
            return new DishDto
            {
                DishId = dish.DishID,
                DishName = dish.DishName,
                Price = dish.Price,
                Description = dish.Description,
                IsSoldOut = (int)dish.IsSoldOut,
                SellerID = dish.SellerID
            };
        }

        public async Task<DishDto?> UpdateDishAsync(int dishId, UpdateDishDto dto)
        {
            var dish = await _dishRepo.GetByIdAsync(dishId);
            if (dish == null) return null;

            if (dto.DishName != null) dish.DishName = dto.DishName;
            if (dto.Price.HasValue) dish.Price = dto.Price.Value;
            if (dto.Description != null) dish.Description = dto.Description;
            if (dto.IsSoldOut.HasValue) dish.IsSoldOut = (DishIsSoldOut)dto.IsSoldOut.Value;
            if (dto.SellerID.HasValue) dish.SellerID = dto.SellerID.Value;

            await _dishRepo.UpdateAsync(dish);
            return new DishDto
            {
                DishId = dish.DishID,
                DishName = dish.DishName,
                Price = dish.Price,
                Description = dish.Description,
                IsSoldOut = (int)dish.IsSoldOut,
                SellerID = dish.SellerID
            };
        }

        public async Task<(bool Success, string? Message, DishDto? Data)> ToggleSoldOutAsync(int dishId, int isSoldOut, int sellerId)
        {
            if (isSoldOut != 0 && isSoldOut != 2)
                return (false, "售罄状态错误", null);

            var dish = await _dishRepo.GetByIdAsync(dishId);
            if (dish == null)
                return (false, "菜品不存在", null);

            // 验证菜品是否属于该商家
            if (dish.SellerID != sellerId)
                return (false, "无权限操作此菜品", null);

            dish.IsSoldOut = (DishIsSoldOut)isSoldOut;
            await _dishRepo.UpdateAsync(dish);

            return (true, null, new DishDto
            {
                DishId = dish.DishID,
                DishName = dish.DishName,
                Price = dish.Price,
                Description = dish.Description,
                IsSoldOut = (int)dish.IsSoldOut,
                SellerID = dish.SellerID
            });
        }

        public async Task<DishDto?> GetDishByIdAsync(int dishId)
        {
            var dish = await _dishRepo.GetByIdAsync(dishId);
            return dish == null ? null : new DishDto
            {
                DishId = dish.DishID,
                DishName = dish.DishName,
                Price = dish.Price,
                Description = dish.Description,
                IsSoldOut = (int)dish.IsSoldOut,
                SellerID = dish.SellerID
            };
        }
    }
}