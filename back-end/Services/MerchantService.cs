using BackEnd.Dtos.Merchant;
using BackEnd.Models;
using BackEnd.Models.Enums;
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

            // 1. 获取店铺信息
            var store = await _merchantRepository.GetStoreBySellerIdAsync(sellerId);

            // 检查店铺是否存在
            if (store == null)
            {
                Console.WriteLine($"未找到商家ID为 {sellerId} 的店铺信息");
                // 返回默认值或者抛出异常，这里我选择返回默认值
                return new ShopOverviewResponseDto
                {
                    Rating = 0.0m,
                    MonthlySales = 0,
                    IsOpen = false,
                    CreditScore = 0
                };
            }

            Console.WriteLine($"店铺信息: StoreID={store.StoreID}, Name={store.StoreName}, Rating={store.AverageRating}, Sales={store.MonthlySales}");

            // 2. 获取商家信息（用于信誉积分）
            var seller = await _merchantRepository.GetSellerByIdAsync(sellerId);
            if (seller == null)
            {
                Console.WriteLine($"未找到商家ID为 {sellerId} 的商家信息");
                return new ShopOverviewResponseDto
                {
                    Rating = store.AverageRating,
                    MonthlySales = store.MonthlySales,
                    IsOpen = store.StoreState == StoreState.IsOperation,
                    CreditScore = 0
                };
            }

            Console.WriteLine($"商家信息: UserID={seller.UserID}, ReputationPoints={seller.ReputationPoints}");

            // 3. 组装数据
            var rating = store.AverageRating;
            Console.WriteLine($"店铺评分(从数据库): {rating}");

            var monthlySales = store.MonthlySales;
            Console.WriteLine($"月销量(从数据库): {monthlySales}");

            // 从数据库的StoreState字段获取营业状态
            var isOpen = store.StoreState == StoreState.IsOperation; // IsOperation=营业中, Closing=休息中
            Console.WriteLine($"营业状态(从数据库): {isOpen} (StoreState={store.StoreState})");

            var creditScore = seller.ReputationPoints;
            Console.WriteLine($"信誉积分(从数据库): {creditScore}");

            var result = new ShopOverviewResponseDto
            {
                Rating = rating,
                MonthlySales = monthlySales,
                IsOpen = isOpen,
                CreditScore = creditScore
            };

            Console.WriteLine($"返回结果: {System.Text.Json.JsonSerializer.Serialize(result)}");
            return result;
        }

        public async Task<ShopInfoResponseDto?> GetShopInfoAsync(int sellerId)
        {
            Console.WriteLine($"=== Service层: 获取店铺信息，商家ID: {sellerId} ===");

            var store = await _merchantRepository.GetStoreBySellerIdAsync(sellerId);
            var seller = await _merchantRepository.GetSellerByIdAsync(sellerId);

            Console.WriteLine($"店铺信息: StoreID={store.StoreID}, Name={store.StoreName}, Address={store.StoreAddress}");
            Console.WriteLine($"商家信息: ReputationPoints={seller.ReputationPoints}");

            var result = new ShopInfoResponseDto
            {
                Id = store.StoreID.ToString(),
                Name = store.StoreName,
                CreateTime = store.StoreCreationTime.ToString("yyyy-MM-dd HH:mm:ss"),
                Address = store.StoreAddress,
                OpenTime = store.OpenTime.ToString(@"hh\:mm"),
                CloseTime = store.CloseTime.ToString(@"hh\:mm"),
                Feature = store.StoreFeatures,
                CreditScore = seller.ReputationPoints
            };

            Console.WriteLine($"返回结果: {System.Text.Json.JsonSerializer.Serialize(result)}");
            return result;
        }

        public async Task<MerchantInfoResponseDto?> GetMerchantInfoAsync(int sellerId)
        {
            Console.WriteLine($"=== Service层: 获取商家信息，商家ID: {sellerId} ===");

            var user = await _merchantRepository.GetUserBySellerIdAsync(sellerId);
            Console.WriteLine($"用户信息: Username={user!.Username}");

            var result = new MerchantInfoResponseDto
            {
                Username = user.Username,
                SellerId = sellerId
            };

            Console.WriteLine($"返回结果: {System.Text.Json.JsonSerializer.Serialize(result)}");
            return result;
        }

        public async Task<CommonResponseDto> ToggleBusinessStatusAsync(int sellerId, ToggleBusinessStatusRequestDto request)
        {
            try
            {
                Console.WriteLine($"=== Service层: 切换营业状态，商家ID: {sellerId}, 新状态: {request.IsOpen} ===");

                // 1. 根据商家ID查询店铺信息
                var store = await _merchantRepository.GetStoreBySellerIdAsync(sellerId);
                if (store == null)
                {
                    Console.WriteLine("店铺不存在，切换营业状态失败");
                    return new CommonResponseDto { Success = false };
                }

                Console.WriteLine($"找到店铺: StoreID={store.StoreID}, Name={store.StoreName}, 当前状态: {store.StoreState}");

                // 2. 根据商家ID查询商家信息
                var seller = await _merchantRepository.GetSellerByIdAsync(sellerId);
                if (seller?.BanStatus == SellerState.Banned)
                {
                    Console.WriteLine($"商家被禁用，BanStatus={seller.BanStatus}，切换营业状态失败");
                    return new CommonResponseDto { Success = false };
                }

                Console.WriteLine($"商家状态正常，BanStatus={seller?.BanStatus}");

                // 3. 更新营业状态
                var oldStatus = store.StoreState;
                store.StoreState = request.IsOpen ? StoreState.IsOperation : StoreState.Closing;  // IsOperation=营业中, Closing=休息中

                Console.WriteLine($"营业状态从 '{oldStatus}' 更新为 '{store.StoreState}' ({(request.IsOpen ? "营业中" : "休息中")})");

                // 4. 保存到数据库
                var success = await _merchantRepository.UpdateStoreAsync(store);
                Console.WriteLine($"数据库更新结果: {success}");

                if (success)
                {
                    Console.WriteLine("=== Service层: 营业状态切换成功 ===");
                }
                else
                {
                    Console.WriteLine("=== Service层: 营业状态切换失败 ===");
                }

                return new CommonResponseDto { Success = success };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"切换营业状态异常: {ex.Message}");
                Console.WriteLine($"异常堆栈: {ex.StackTrace}");
                return new CommonResponseDto { Success = false };
            }
        }

        public async Task<CommonResponseDto> UpdateShopFieldAsync(int sellerId, UpdateShopFieldRequestDto request)
        {
            try
            {
                Console.WriteLine($"=== Service层: 更新店铺字段，商家ID: {sellerId}, 字段: {request.Field}, 值: {request.Value} ===");

                var store = await _merchantRepository.GetStoreBySellerIdAsync(sellerId);
                if (store == null)
                {
                    Console.WriteLine("店铺不存在，更新失败");
                    return new CommonResponseDto { Success = false };
                }

                Console.WriteLine($"找到店铺: StoreID={store.StoreID}, Name={store.StoreName}");

                var seller = await _merchantRepository.GetSellerByIdAsync(sellerId);
                if (seller?.BanStatus == SellerState.Banned)
                {
                    Console.WriteLine($"商家被禁用，BanStatus={seller.BanStatus}，更新失败");
                    return new CommonResponseDto { Success = false };
                }

                Console.WriteLine($"商家状态正常，BanStatus={seller?.BanStatus}");

                switch (request.Field)
                {
                    case "Address":
                    case "address":
                        store.StoreAddress = request.Value;
                        Console.WriteLine($"更新地址为: {request.Value}");
                        break;
                    case "OpenTime":
                    case "openTime":
                    case "startTime":
                        if (TimeSpan.TryParse(request.Value, out var openTime))
                        {
                            store.OpenTime = openTime;
                            Console.WriteLine($"更新营业开始时间为: {openTime}");
                        }
                        else
                        {
                            Console.WriteLine($"时间格式解析失败: {request.Value}");
                            return new CommonResponseDto { Success = false };
                        }
                        break;
                    case "CloseTime":
                    case "closeTime":
                    case "endTime":
                        if (TimeSpan.TryParse(request.Value, out var closeTime))
                        {
                            store.CloseTime = closeTime;
                            Console.WriteLine($"更新营业结束时间为: {closeTime}");
                        }
                        else
                        {
                            Console.WriteLine($"时间格式解析失败: {request.Value}");
                            return new CommonResponseDto { Success = false };
                        }
                        break;
                    case "Feature":
                    case "feature":
                        store.StoreFeatures = request.Value;
                        Console.WriteLine($"更新特色为: {request.Value}");
                        break;
                    default:
                        Console.WriteLine($"不支持的字段: {request.Field}");
                        return new CommonResponseDto { Success = false };
                }

                var success = await _merchantRepository.UpdateStoreAsync(store);
                Console.WriteLine($"数据库更新结果: {success}");

                return new CommonResponseDto { Success = success };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"更新店铺字段异常: {ex.Message}");
                Console.WriteLine($"异常堆栈: {ex.StackTrace}");
                return new CommonResponseDto { Success = false };
            }
        }
    }
}
