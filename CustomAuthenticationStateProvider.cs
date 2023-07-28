using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Net.Http;
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
        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {

            var identity = new ClaimsIdentity();

            var user = new ClaimsPrincipal(identity);
            var state = new AuthenticationState(user);

            return Task.FromResult(state);
        }

        public async Task UpdateAuthStateAsync()
        {
            string authToken = await _localStorageService.GetItemAsStringAsync(_authTokenName);

            var identity = new ClaimsIdentity();

            if (!string.IsNullOrEmpty(authToken))
            {
                try
                {
                    _logger.LogInformation("哈哈哈");
                    identity = new ClaimsIdentity(ParseClaimFromJwt(authToken), "jwt");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    await _localStorageService.RemoveItemAsync(_authTokenName);
                    identity = new ClaimsIdentity();
                }
            }
            var user = new ClaimsPrincipal(identity);
            var state = new AuthenticationState(user);

            NotifyAuthenticationStateChanged(Task.FromResult(state));

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
