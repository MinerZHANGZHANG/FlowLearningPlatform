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

        private bool _isPrerendering = true;

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
            if (_isPrerendering)
            {
                var identity = new ClaimsIdentity();

                var user = new ClaimsPrincipal(identity);
                var state = new AuthenticationState(user);

                return state;
            }
            else
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

                // NotifyAuthenticationStateChanged(Task.FromResult(state));
                return state;
            }
        }

        // Method to be called when the client-side rendering is complete
        public async Task MarkAsClientRendered()
        {
            _isPrerendering = false;
            var state= await GetAuthenticationStateAsync();
            NotifyAuthenticationStateChanged(Task.FromResult(state));
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

        [Obsolete]
        public void MarkUserAsAuthenticated(string account)
        {
            var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, account) }, "jwt"));
            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
            NotifyAuthenticationStateChanged(authState);
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
