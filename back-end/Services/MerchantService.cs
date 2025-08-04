using BackEnd.Dtos.Merchant;
using BackEnd.Models;
using BackEnd.Repositories.Interfaces;
using BackEnd.Services.Interfaces;

namespace BackEnd.Services
{
    public class MerchantService : IMerchantService
    {
        private readonly IMerchantRepository _merchantRepository;

        public MerchantService(IMerchantRepository merchantRepository)
        {
            _merchantRepository = merchantRepository;
        }

        public async Task<ShopOverviewResponseDto> GetShopOverviewAsync(int sellerId)
        {
            Console.WriteLine($"=== Service层: 获取店铺概览，商家ID: {sellerId} ===");
            
            var store = await _merchantRepository.GetStoreBySellerIdAsync(sellerId);
            Console.WriteLine($"店铺信息: {(store == null ? "null" : $"StoreID={store.StoreID}, Name={store.StoreName}")}");
            
            if (store == null)
            {
                Console.WriteLine("店铺不存在，返回默认数据");
                return new ShopOverviewResponseDto
                {
                    Rating = 0,
                    MonthlySales = 0,
                    IsOpen = false
                };
            }

            var rating = await _merchantRepository.GetStoreRatingAsync(store.StoreID);
            Console.WriteLine($"店铺评分: {rating}");
            
            var monthlySales = await _merchantRepository.GetStoreMonthlySalesAsync(store.StoreID);
            Console.WriteLine($"月销量: {monthlySales}");
            
            // TODO: 需要在Store模型中添加StoreStatus字段来判断营业状态
            var isOpen = true; // 暂时默认为营业中
            Console.WriteLine($"营业状态: {isOpen}");

            var result = new ShopOverviewResponseDto
            {
                Rating = rating,
                MonthlySales = monthlySales,
                IsOpen = isOpen
            };
            
            Console.WriteLine($"返回结果: {System.Text.Json.JsonSerializer.Serialize(result)}");
            return result;
        }

        public async Task<ShopInfoResponseDto?> GetShopInfoAsync(int sellerId)
        {
            var store = await _merchantRepository.GetStoreBySellerIdAsync(sellerId);
            if (store == null) return null;

            var seller = await _merchantRepository.GetSellerByIdAsync(sellerId);

            return new ShopInfoResponseDto
            {
                Id = store.StoreID.ToString(),
                Name = store.StoreName,
                CreateTime = store.StoreCreationTime.ToString("yyyy-MM-dd HH:mm:ss"),
                Address = store.StoreAddress,
                BusinessHours = store.BusinessHours.ToString("HH:mm"),
                Feature = store.StoreFeatures,
                CreditScore = seller?.ReputationPoints
            };
        }

        public async Task<MerchantInfoResponseDto?> GetMerchantInfoAsync(int sellerId)
        {
            var user = await _merchantRepository.GetUserBySellerIdAsync(sellerId);
            if (user == null) return null;

            return new MerchantInfoResponseDto
            {
                Username = user.Username
            };
        }

        public async Task<CommonResponseDto> ToggleBusinessStatusAsync(int sellerId, ToggleBusinessStatusRequestDto request)
        {
            try
            {
                var store = await _merchantRepository.GetStoreBySellerIdAsync(sellerId);
                if (store == null) return new CommonResponseDto { Success = false };

                var seller = await _merchantRepository.GetSellerByIdAsync(sellerId);
                if (seller?.BanStatus == "Banned") return new CommonResponseDto { Success = false };

                // TODO: 需要在Store模型中添加StoreStatus字段来实现营业状态切换
                // 暂时返回失败，因为还没有实现这个功能
                return new CommonResponseDto { Success = false };
            }
            catch
            {
                return new CommonResponseDto { Success = false };
            }
        }

        public async Task<CommonResponseDto> UpdateShopFieldAsync(int sellerId, UpdateShopFieldRequestDto request)
        {
            try
            {
                var store = await _merchantRepository.GetStoreBySellerIdAsync(sellerId);
                if (store == null) return new CommonResponseDto { Success = false };

                var seller = await _merchantRepository.GetSellerByIdAsync(sellerId);
                if (seller?.BanStatus == "Banned") return new CommonResponseDto { Success = false };

                switch (request.Field.ToLower())
                {
                    case "address":
                        store.StoreAddress = request.Value;
                        break;
                    case "businesshours":
                        store.BusinessHours = DateTime.Parse(request.Value);
                        break;
                    case "feature":
                        store.StoreFeatures = request.Value;
                        break;
                    default:
                        return new CommonResponseDto { Success = false };
                }

                var success = await _merchantRepository.UpdateStoreAsync(store);
                return new CommonResponseDto { Success = success };
            }
            catch
            {
                return new CommonResponseDto { Success = false };
            }
        }
    }
} 