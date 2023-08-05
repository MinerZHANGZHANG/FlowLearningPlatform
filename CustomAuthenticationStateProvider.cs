using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Text.Json;

namespace FlowLearningPlatform
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILogger<CustomAuthenticationStateProvider> _logger;
        private readonly ILocalStorageService _localStorageService;
        private readonly IConfiguration _configuration;

        private string _authTokenName=string.Empty;

        public CustomAuthenticationStateProvider(
            ILogger<CustomAuthenticationStateProvider> logger
            ,ILocalStorageService localStorageService
            ,IConfiguration configuration)
        {
            _logger = logger;
            _localStorageService = localStorageService;
            _configuration = configuration;
            _authTokenName = _configuration.GetSection("AppSettings:LocalTokenName").Value;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            //Blazor 服务器将呈现网页 2 次，第一次在服务器中，
            //第二次在浏览器中。由于浏览器存储在服务器中不可用，
            //因此当 Blazor 首次尝试呈现页面时，需要将浏览器存储访问代码包装在 try catch 中。
            //如果不这样做，则会收到错误。

            // Todo:验证数据库是否存在这个人
            try
            {
                string authToken = await _localStorageService.GetItemAsStringAsync(_authTokenName);

                var identity = new ClaimsIdentity();

                if (!string.IsNullOrEmpty(authToken))
                {
                    try
                    {
                        identity = new ClaimsIdentity(ParseClaimFromJwt(authToken), "jwt");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(message: ex.Message);
                        await _localStorageService.RemoveItemAsync(_authTokenName);
                        identity = new ClaimsIdentity();
                    }
                }
                var user = new ClaimsPrincipal(identity);
                var state = new AuthenticationState(user);

                return state;
            }
            catch
            {
                var identity = new ClaimsIdentity();

                var user = new ClaimsPrincipal(identity);
                var state = new AuthenticationState(user);

                return state;
            }

        }

        public void AuthStateChange()
        {
            NotifyAuthenticationStateChanged( GetAuthenticationStateAsync());
        }

        public async Task Logout()
        {
            await _localStorageService.RemoveItemAsync(_authTokenName);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }

        private IEnumerable<Claim> ParseClaimFromJwt(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer
                .Deserialize<Dictionary<string, object>>(jsonBytes);

            var claims = keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));

            return claims;
        }
    }
}
