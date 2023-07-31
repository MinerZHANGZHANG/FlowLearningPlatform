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
	    Task<ServiceResponse<string>> LoginAsync(UserLogin userLogin);
        Task Logout();
        void RefreshAuthState();
    }

    public class AuthService : IAuthService
	{
        private readonly DataContext _context;
        private readonly ILogger<AuthService> _logger;
        private readonly IConfiguration _configuration;
        private readonly AuthenticationStateProvider _authenticationState;

        private bool _isRun = false;

        public AuthService(DataContext context,ILogger<AuthService> logger, IConfiguration configuration,AuthenticationStateProvider authenticationState)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
            _authenticationState = authenticationState;
        }

        public async Task<ServiceResponse<string>> Register(IdentityVal identityVal, RegisterSecondStep baseInfo, ExtraInfo extraInfo)
        {
            if (_isRun)
            {
                return new() { Success=false};
            }

            try
            {
                _isRun = true;
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
                    RoleTypeId = Guid.Parse(identityVal.RoleId),
                    StudentNumber = identityVal.StudentNumber,

                    PhoneNumber = extraInfo.PhoneNumber,
                    Email = extraInfo.Email,
                    Brithday =identityVal.Brithday,
                    Description = extraInfo.Description,
                };

                await _context.Users.AddAsync(newUser);
                await _context.SaveChangesAsync();

				_isRun =false;
				return new() { Data = newUser.UserId.ToString(), Message = "注册成功" };
            }
            catch (Exception ex)
            {
                _logger.LogError($"用户注册时发生错误：{ex.Message}");
				_isRun = false;
				return new() { Data = string.Empty, Message = $"注册失败", Success = false };
            }
        }

        public async Task<ServiceResponse<bool>> ValidateUser(IdentityVal identityVal)
        {
			if (_isRun)
			{
				return new() { Success = false };
			}

			ServiceResponse<bool> result = new()
            {
                Data = true,
                Message = "验证成功",
            };
			_isRun =true;
			bool isExist = await _context.Users.AsNoTracking().AnyAsync(user => user.StudentNumber.Equals(identityVal.StudentNumber));
			_isRun = false;
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



        public async Task<ServiceResponse<string>> LoginAsync(UserLogin userLogin)
        {
			if (_isRun)
			{
				return new() { Success = false };
			}
			ServiceResponse<string> response = new();
			_isRun = true;
			User user = await _context.Users.Include(u=>u.RoleType).FirstOrDefaultAsync(u => u.StudentNumber.Equals(userLogin.StudentNumber));
			_isRun = false;
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
