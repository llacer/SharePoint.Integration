using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SharePoint.Integration.ACS
{
    public interface IAzureAuthProvider
    {
        Task<string> AcquireTokenAsync();
    }

    public class AzureAuthProvider : IAzureAuthProvider
    {
        private readonly AuthenticationConfig _config;

        public AzureAuthProvider(AuthenticationConfig config)
        {
            _config = config;
        }

        public async Task<string> AcquireTokenAsync()
        {
            var body = $"resource={_config.Resource}&" +
                       $"client_id={_config.ClientId}@{_config.TenantId}&" +
                       "grant_type=client_credentials&" +
                       $"client_secret={_config.ClientSecret}";

            using var stringContent = new StringContent(body, Encoding.UTF8, "application/x-www-form-urlencoded");
            var httpClient = new HttpClient();
            var result = await httpClient
                .PostAsync(_config.AuthEndPoint, stringContent)
                .ContinueWith((response) => response.Result.Content.ReadAsStringAsync().Result)
                .ConfigureAwait(false);
            var tokenResult = JsonSerializer.Deserialize<JsonElement>(result);
            var token = tokenResult.GetProperty("access_token").GetString();
            return token;
        }
    }
}