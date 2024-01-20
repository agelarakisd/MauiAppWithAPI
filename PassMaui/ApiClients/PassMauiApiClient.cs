
using System.Net.Http.Headers;
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

        //public async Task<IAtozApiClientV1> CreateClient(int timeoutInSeconds = 5)
        //{
        //    var httpClient = _clientFactory.CreateClient(nameof(IAtozApiClientV1));
        //    httpClient.BaseAddress = new Uri(_settings.BaseUrl);
        //    httpClient.Timeout = TimeSpan.FromSeconds(timeoutInSeconds);
        //    var accessToken = await _accessTokenProvider.GetAccessToken(_settings.Scopes);
        //    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        //    httpClient.DefaultRequestHeaders.Add("X-Correlation-Id", CorrelationScope.CurrentCorrelationIdOrNull);

        //    return new AtozApiClientV1(httpClient);
        //}
    }
}
