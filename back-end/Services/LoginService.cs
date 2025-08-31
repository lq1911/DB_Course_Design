using BackEnd.Dtos.AuthRequest;
using BackEnd.Dtos.User;
using BackEnd.Models;
using BackEnd.Models.Enums;
using BackEnd.Repositories.Interfaces;
using BackEnd.Services.Interfaces;
using Microsoft.Extensions.Configuration; // 确保 using 了这个
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic; // 确保 using 了这个
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks; // 确保 using 了这个

namespace BackEnd.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public LoginService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<LoginResult> LoginAsync(LoginRequest request)
        {
            // --- 这是核心修改部分 ---
            // 1. 根据手机号查找用户
            // 【已修正】直接使用 string 类型的 request.PhoneNum 调用仓储层
            var user = await _userRepository.GetByPhoneAsync(request.PhoneNum);
            // --- 修改结束 ---

            if (user == null)
                return Fail("手机号或密码错误", 401);

            // 2. 验证密码
            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                return Fail("手机号或密码错误", 401);

            // 3. 验证角色
            if (!IsRoleMatch(user.Role, request.Role))
                return Fail("角色选择错误，请选择正确的登录身份", 403);

            // 4. 生成Token
            var token = GenerateJwtToken(user);

            // 5. 返回登录结果
            return new LoginResult
            {
                Success = true,
                Token = token,
                User = new UserInfo
                {
                    UserId = user.UserID,
                    Username = user.Username,
                    Role = user.Role.ToString().ToLower(),
                    Avatar = user.Avatar
                }
            };
        }

        private bool IsRoleMatch(UserIdentity userRole, string requestRole)
        {
            var roleMapping = new Dictionary<string, UserIdentity>
            {
                { "customer", UserIdentity.Customer },
                { "rider", UserIdentity.Courier },
                { "merchant", UserIdentity.Seller },
                { "admin", UserIdentity.Administrator }
            };

            return roleMapping.TryGetValue(requestRole.ToLower(), out var expectedRole)
                   && userRole == expectedRole;
        }

        // 生成 JWT Token
        private string GenerateJwtToken(Models.User user)
        {
            var jwtKey = _configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT密钥未配置");

            var key = Encoding.ASCII.GetBytes(jwtKey);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                // 【已修正】user.PhoneNumber 现在是 string，无需再 .ToString()
                new Claim("phone", user.PhoneNumber)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        // 表示登录失败（和注册服务保持一致的命名）
        private LoginResult Fail(string message, int code = 400)
        {
            return new LoginResult
            {
                Success = false,
                Code = code,
                Message = message,
                Token = null,
                User = null
            };
        }
    }
}