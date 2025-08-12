using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Dtos.UserHomepage;
using BackEnd.Repositories.Interfaces;
using BackEnd.Services.Interfaces;

namespace BackEnd.Services
{
    public class UserHomepageService : IUserHomepageService
    {
        private readonly IStoreRepository _storeRepository;
        private readonly IDishRepository _dishRepository;
        private readonly IMenuRepository _menuRepository;
        private readonly IMenu_DishRepository _menuDishRepository;

        public UserHomepageService(
            IStoreRepository storeRepository,
            IDishRepository dishRepository,
            IMenuRepository menuRepository,
            IMenu_DishRepository menuDishRepository)
        {
            _storeRepository = storeRepository;
            _dishRepository = dishRepository;
            _menuRepository = menuRepository;
            _menuDishRepository = menuDishRepository;
        }

        public async Task<IEnumerable<HomeRecmDto>> GetRecommendedStoresAsync()
        {
            var stores = await _storeRepository.GetAllAsync();

            // 按评分排序，取前10个，然后随机选4个
            var topStores = stores
                .OrderByDescending(s => s.AverageRating)
                .Take(10)
                .ToList();

            var random = new Random();
            var recommended = topStores
                .OrderBy(s => random.Next())
                .Take(4)
                .Select(s => new HomeRecmDto
                {
                    StoreID = s.StoreID,
                    StoreName = s.StoreName,
                    AverageRating = s.AverageRating,
                    MonthlySales = s.MonthlySales
                });

            return recommended;
        }

        public async Task<(IEnumerable<HomeSearchGetDto> Stores, IEnumerable<HomeSearchGetDto> Dishes)>
            SearchAsync(HomeSearchDto searchDto)
        {
            // 店铺搜索
            var stores = await _storeRepository.GetAllAsync();
            var storeResults = stores
                .Where(s => s.StoreName.Contains(searchDto.SearchName))
                .Select(s => new HomeSearchGetDto
                {
                    AverageRating = s.AverageRating,
                    StoreName = s.StoreName,
                    MonthlySales = s.MonthlySales,
                    StoreAddress = s.StoreAddress
                })
                .ToList();

            // 菜品搜索
            var dishes = await _dishRepository.GetAllAsync();
            var menus = await _menuRepository.GetAllAsync();
            var menuDishes = await _menuDishRepository.GetAllAsync();

            var dishResults = dishes
                .Where(d => d.DishName.Contains(searchDto.SearchName))
                .Join(menuDishes,
                      dish => dish.DishID,
                      md => md.DishID,
                      (dish, md) => new { dish, md })
                .Join(menus,
                      dm => dm.md.MenuID,
                      menu => menu.MenuID,
                      (dm, menu) => new { dm.dish, menu })
                .Join(stores,
                      dmMenu => dmMenu.menu.StoreID,
                      store => store.StoreID,
                      (dmMenu, store) => new HomeSearchGetDto
                      {
                          AverageRating = store.AverageRating,
                          StoreName = store.StoreName,
                          MonthlySales = store.MonthlySales,
                          StoreAddress = store.StoreAddress
                      })
                .Distinct()
                .ToList();

            return (storeResults, dishResults);
        }
    }
}
