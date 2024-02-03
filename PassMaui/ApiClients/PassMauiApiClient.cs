
using PassMaui.APIServices;
using System.Text.Json;

namespace PassMaui.ApiClients
{
    public class PassMauiApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly JsonSerializerOptions _options;

        public PassMauiApiClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;

            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }
        
        public AccountApiService CreateApiService(int timeoutInSeconds = 5)
        {
            var httpClient = _httpClientFactory.CreateClient(nameof(PassMauiApiClient));
            httpClient.BaseAddress = new Uri("http://localhost:5119/");
            httpClient.Timeout = TimeSpan.FromSeconds(timeoutInSeconds);

            return new AccountApiService(httpClient);
        }
    }
}
