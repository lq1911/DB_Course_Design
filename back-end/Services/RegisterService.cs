using BackEnd.Data;
using BackEnd.Dtos.AuthRequest;
using BackEnd.Models;
using BackEnd.Models.Enums;
using BackEnd.Repositories.Interfaces;
using BackEnd.Services.Interfaces;
using System;
using System.Linq; // 引入 LINQ 以使用 .All()
using System.Threading.Tasks;

namespace BackEnd.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly IUserRepository _userRepo;
        private readonly IStoreRepository _storeRepo;
        private readonly AppDbContext _context;

        public RegisterService(IUserRepository userRepo, IStoreRepository storeRepo, AppDbContext context)
        {
            _userRepo = userRepo;
            _storeRepo = storeRepo;
            _context = context;
        }

        // 根据请求创建用户
        public async Task<RegisterResult> RegisterAsync(RegisterRequest req)
        {
            // 密码验证
            if (req.Password != req.ConfirmPassword)
                return new RegisterResult { Success = false, Code = 400, Message = "两次密码不一致" };

            // 不能注册重复的手机号
            if (await _userRepo.ExistsByPhoneAsync(req.Phone))
                return new RegisterResult { Success = false, Code = 409, Message = "手机号已被注册" };

            // 隐私设置验证
            ProfilePrivacyLevel privacyLevel;
            if (Enum.IsDefined(typeof(ProfilePrivacyLevel), req.IsPublic))
                privacyLevel = (ProfilePrivacyLevel)req.IsPublic;
            else
                return Fail("无效的隐私设置", 400);

            // 【新增】验证手机号格式是否为纯数字
            if (string.IsNullOrWhiteSpace(req.Phone) || !req.Phone.All(char.IsDigit))
            {
                return Fail("手机号格式错误，必须为纯数字", 400);
            }

            // 构建用户实体
            var user = new User
            {
                Username = req.Nickname,
                Password = HashPassword(req.Password),
                // 【已修正】直接将 string 类型的 req.Phone 赋值给 string 类型的 PhoneNumber
                PhoneNumber = req.Phone,
                Email = req.Email,
                Gender = req.Gender,
                Birthday = !string.IsNullOrEmpty(req.Birthday) ? DateTime.Parse(req.Birthday) : null,
                Avatar = req.AvatarUrl,
                IsProfilePublic = privacyLevel,
                AccountCreationTime = DateTime.UtcNow,
                Role = MapStringToRole(req.Role)
            };

            // 根据角色附加子实体
            switch (req.Role.ToLower())
            {
                case "rider":
                    if (req.RiderInfo == null)
                        return Fail("缺少 RiderInfo");
                    if (req.RiderInfo.Name.Trim().Length > 6)
                        return Fail("真实姓名长度不能超过6个字符", 400);
                    user.FullName = req.RiderInfo.Name.Trim();
                    user.Courier = new Courier
                    {
                        VehicleType = req.RiderInfo.VehicleType,
                        CourierRegistrationTime = DateTime.Now
                    };
                    break;
                case "admin":
                    if (req.AdminInfo == null)
                        return Fail("缺少 AdminInfo");
                    if (req.AdminInfo.Name.Trim().Length > 6)
                        return Fail("真实姓名长度不能超过6个字符", 400);
                    user.FullName = req.AdminInfo.Name.Trim();
                    user.Administrator = new Administrator
                    {
                        ManagedEntities = req.AdminInfo.ManagementObject,
                        AdminRegistrationTime = DateTime.Now
                    };
                    break;
                case "merchant":
                    if (req.StoreInfo == null)
                        return Fail("缺少 StoreInfo");
                    if (req.StoreInfo.SellerName.Trim().Length > 6)
                        return Fail("真实姓名长度不能超过6个字符", 400);
                    user.FullName = req.StoreInfo.SellerName.Trim();
                    user.Seller = new Seller
                    {
                        SellerRegistrationTime = DateTime.Now
                    };
                    break;
                case "customer":
                    user.Customer = new Customer
                    {
                        DefaultAddress = ""
                    };
                    break;
                default:
                    return Fail("不支持的角色类型");
            }

            // 使用数据库事务确保数据一致性（商家和店铺同时注册）
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // 保存用户（包含相关联的角色实体）
                await _userRepo.AddAsync(user);

                // 如果是商家，创建店铺信息
                if (req.Role.ToLower() == "merchant" && req.StoreInfo != null && user.Seller != null)
                {
                    // AddAsync 内部调用了 SaveAsync，所以此时 user.UserID 应该已经被数据库生成并填充
                    await CreateStoreAsync(req.StoreInfo, user.UserID);
                }

                // 提交事务
                await transaction.CommitAsync();

                return new RegisterResult
                {
                    Success = true,
                    Code = 201,
                    Message = "注册成功！"
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return Fail($"注册失败: {ex.Message}", 500);
            }
        }

        // 字符串角色映射方法
        private UserIdentity MapStringToRole(string roleStr)
        {
            switch (roleStr.ToLower())
            {
                case "customer": return UserIdentity.Customer;
                case "rider": return UserIdentity.Courier;
                case "admin": return UserIdentity.Administrator;
                case "merchant": return UserIdentity.Seller;
                default: throw new ArgumentException($"不支持的角色类型: {roleStr}");
            }
        }

        // 密码加密
        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, 12);
        }

        // 注册店铺
        private async Task CreateStoreAsync(StoreInfoDto storeInfo, int sellerId)
        {
            if (!TimeSpan.TryParse(storeInfo.OpenTime, out var openTime))
                throw new ArgumentException("营业开始时间格式错误，请使用 HH:mm 格式");

            if (!TimeSpan.TryParse(storeInfo.CloseTime, out var closeTime))
                throw new ArgumentException("营业结束时间格式错误，请使用 HH:mm 格式");

            if (!Enum.TryParse<StoreCategory>(storeInfo.Category, true, out var category))
                throw new ArgumentException($"店铺类别无效: {storeInfo.Category}");

            if (!DateTime.TryParse(storeInfo.EstablishmentDate, out var establishmentDate))
                establishmentDate = DateTime.Now;

            var store = new Store
            {
                StoreName = storeInfo.StoreName.Trim(),
                StoreAddress = storeInfo.Address.Trim(),
                OpenTime = openTime,
                CloseTime = closeTime,
                StoreCreationTime = establishmentDate,
                StoreCategory = category,
                SellerID = sellerId
            };

            await _storeRepo.AddAsync(store);
        }

        // 表示注册失败
        private RegisterResult Fail(string message, int code = 400)
        {
            return new RegisterResult
            {
                Success = false,
                Code = code,
                Message = message
            };
        }
    }
}