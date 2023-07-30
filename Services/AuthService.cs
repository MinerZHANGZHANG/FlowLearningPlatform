using AntDesign.Internal;
using Blazored.LocalStorage;
using FlowLearningPlatform.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace FlowLearningPlatform.Services
{
	public interface IAuthService
	{
        Task<ServiceResponse<bool>> ValidateUser(IdentityVal identityVal);
        Task<ServiceResponse<string>> Register(IdentityVal identityVal,RegisterSecondStep baseInfo,ExtraInfo extraInfo);
	    Task<ServiceResponse<string>> Login(UserLogin userLogin);
        Task Logout();
        void RefreshAuthState();
    }

    public class AuthService : IAuthService
	{
        private readonly DataContext _context;
        private readonly ILogger<AuthService> _logger;
        private readonly IConfiguration _configuration;
        private readonly AuthenticationStateProvider _authenticationState;

        public AuthService(DataContext context,ILogger<AuthService> logger, IConfiguration configuration,AuthenticationStateProvider authenticationState)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
            _authenticationState = authenticationState;
        }

        public async Task<ServiceResponse<string>> Register(IdentityVal identityVal, RegisterSecondStep baseInfo, ExtraInfo extraInfo)
        {
            try
            {
                byte[] passwordHash;
                byte[] passwordSalt;
                Guid defaultRoleUid = _context.RoleTypes.FirstOrDefault().RoleTypeId;
                CreatePassword(baseInfo.Password, out passwordHash, out passwordSalt);

                User newUser = new()
                {
                    UserId = Guid.NewGuid(),
                    Name = baseInfo.Name,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    RegTime = DateTime.Now,

                    DepartmentTypeId = Guid.Parse(baseInfo.DepartmentTypeId),
                    RoleTypeId = defaultRoleUid,
                    StudentNumber = identityVal.StudentNumber,

                    PhoneNumber = extraInfo.PhoneNumber,
                    Email = extraInfo.Email,
                    Brithday =identityVal.Brithday,
                    Description = extraInfo.Description,
                };

                await _context.Users.AddAsync(newUser);
                await _context.SaveChangesAsync();

                return new() { Data = newUser.UserId.ToString(), Message = "注册成功" };
            }
            catch (Exception ex)
            {
                _logger.LogError($"用户注册时发生错误：{ex.Message}");
                return new() { Data = string.Empty, Message = $"注册失败", Success = false };
            }
        }

        public async Task<ServiceResponse<bool>> ValidateUser(IdentityVal identityVal)
        {
           
            ServiceResponse<bool> result = new()
            {
                Data = true,
                Message = "验证成功",
            };

            bool isExist = await _context.Users.AsNoTracking().AnyAsync(user => user.StudentNumber.Equals(identityVal.StudentNumber));
            if (isExist)
            {
                result.Data = false;
                result.Message = "这个学号已经注册过了";
                result.Success = false;
            }

            // (如果需要的话)根据学号和生日查询这个用户是否确实为学

            return result;
        }

        private static void CreatePassword(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }



        public async Task<ServiceResponse<string>> Login(UserLogin userLogin)
        {
            ServiceResponse<string> response = new();
            User user = await _context.Users.FirstOrDefaultAsync(u => u.StudentNumber.Equals(userLogin.StudentNumber));

            if (user==null)
            {
                response.Message = "未注册的学号";
                response.Success = false;
            }
            else if(!VerifyPasswordHash(userLogin.Password, user.PasswordHash, user.PasswordSalt))
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

            return response;
        }

       public void RefreshAuthState()
        {
            (_authenticationState as CustomAuthenticationStateProvider).Login();
        }

        private string CreateJWTToken(User user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                new Claim(ClaimTypes.Name,user.Name),
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

        private static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512(passwordSalt);
            var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computeHash.SequenceEqual(passwordHash);
        }

        public async Task Logout()
        {
          await  (_authenticationState as CustomAuthenticationStateProvider).Logout();
        }
    }
}
