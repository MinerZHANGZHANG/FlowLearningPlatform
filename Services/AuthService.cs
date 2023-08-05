using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace FlowLearningPlatform.Services
{
    public interface IAuthService
    {
        Task<ServiceResponse<bool>> ValidateUser(RegisterFirstStep identityVal);
        Task<ServiceResponse<string>> Register(RegisterFirstStep identityVal, RegisterSecondStep baseInfo, RegisterThridStep extraInfo);
        Task<ServiceResponse<string>> LoginAsync(UserLogin userLogin);
        Task Logout();
    }

    public class AuthService : IAuthService
    {
        private readonly IDbContextFactory<DataContext> _dbContextFactory;
        private readonly ILogger<AuthService> _logger;
        private readonly IConfiguration _configuration;
        private readonly AuthenticationStateProvider _authenticationState;

        public AuthService(IDbContextFactory<DataContext> dbContextFactory, ILogger<AuthService> logger, IConfiguration configuration, AuthenticationStateProvider authenticationState)
        {
            _dbContextFactory = dbContextFactory;
            _logger = logger;
            _configuration = configuration;
            _authenticationState = authenticationState;
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        public async Task<ServiceResponse<string>> Register(RegisterFirstStep identityInfo, RegisterSecondStep baseInfo, RegisterThridStep extraInfo)
        {
            try
            {
                byte[] passwordHash;
                byte[] passwordSalt;

                CreatePassword(baseInfo.Password, out passwordHash, out passwordSalt);

                User newUser = new()
                {
                    UserId = Guid.NewGuid(),
                    Name = baseInfo.Name,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    RegTime = DateTime.Now,

                    DepartmentTypeId = Guid.Parse(baseInfo.DepartmentTypeId),
                    RoleTypeId = Guid.Parse(identityInfo.RoleId),
                    StudentNumber = identityInfo.StudentNumber,

                    PhoneNumber = extraInfo.PhoneNumber,
                    Email = extraInfo.Email,
                    Brithday = identityInfo.Brithday,
                    Description = extraInfo.Description,
                };

                using (var context = await _dbContextFactory.CreateDbContextAsync())
                {
                    await context.Users.AddAsync(newUser);
                    await context.SaveChangesAsync();
                }

                return new() { Data = newUser.UserId.ToString(), Message = "注册成功" };
            }
            catch (Exception ex)
            {
                _logger.LogError($"用户注册时发生错误：{ex.Message}");
                return new() { Data = string.Empty, Message = $"注册失败", Success = false };
            }
        }

        /// <summary>
        /// 用户验证
        /// </summary>
        public async Task<ServiceResponse<bool>> ValidateUser(RegisterFirstStep identityInfo)
        {
            ServiceResponse<bool> result = new()
            {
                Data = true,
                Message = "验证成功",
            };
            using (var context = await _dbContextFactory.CreateDbContextAsync())
            {
                bool isExist = await context.Users
                    .AsNoTracking()
                    .AnyAsync(user => user.StudentNumber.Equals(identityInfo.StudentNumber));

                if (isExist)
                {
                    result.Data = false;
                    result.Message = "这个学号已经注册过了";
                    result.Success = false;
                }
            }
            // TODO:判断这些信息是否能用于注册新账号

            return result;
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        public async Task<ServiceResponse<string>> LoginAsync(UserLogin userLogin)
        {
            ServiceResponse<string> response = new();
            using (var context = await _dbContextFactory.CreateDbContextAsync())
            {
                User? user = await context.Users
                    .AsNoTracking()
                    .Include(u => u.RoleType)
                    .FirstOrDefaultAsync(u => u.StudentNumber.Equals(userLogin.StudentNumber));

                if (user == null)
                {
                    response.Message = "未注册的学号";
                    response.Success = false;
                }
                else if (!VerifyPasswordHash(userLogin.Password, user.PasswordHash, user.PasswordSalt))
                {
                    response.Message = "错误的密码";
                    response.Success = false;
                }
                else
                {
                    if (userLogin.RememberMe)
                    {
                        response.Data = CreateJWTToken(user);
                    }
                    response.Success = true;
                }
            }

            return response;
        }

        /// <summary>
        /// 创建JWT令牌
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns>令牌</returns>
        private string CreateJWTToken(User user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                new Claim(ClaimTypes.Name,user.Name),
                new Claim(ClaimTypes.Role,user.RoleType.Name)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(int.Parse(_configuration.GetSection("AppSettings:TokenDurationDays").Value)),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        /// <summary>
        /// 随机生成密码哈希
        /// </summary>
        /// <param name="password">原始密码</param>
        /// <param name="passwordHash">密码哈希</param>
        /// <param name="passwordSalt">密码盐</param>
        private static void CreatePassword(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }


        /// <summary>
        /// 验证密码
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordHash"></param>
        /// <param name="passwordSalt"></param>
        /// <returns></returns>
        private static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512(passwordSalt);
            var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computeHash.SequenceEqual(passwordHash);
        }

        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        public async Task Logout()
        {
            await (_authenticationState as CustomAuthenticationStateProvider).Logout();
        }

        /// <summary>
        /// 尝试从用户宣称中获取信息
        /// </summary>
        /// <param name="User"></param>
        /// <param name="userId">用户编号</param>
        /// <param name="userName">用户名称</param>
        /// <param name="userRole">用户权限</param>
        /// <returns></returns>
        public static bool TryGetUserFromClaim(ClaimsPrincipal User, out Guid userId, out string userName, out string userRole)
        {
            userId = Guid.Empty;
            userName = string.Empty;
            userRole = string.Empty;
            if (User != null)
            {
                if (Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out userId))
                {
                    userName = User.FindFirstValue(ClaimTypes.Name) ?? string.Empty;
                    userRole = User.FindFirstValue(ClaimTypes.Role) ?? string.Empty;
                    return true;
                }
            }
            return false;
        }
    }
}
