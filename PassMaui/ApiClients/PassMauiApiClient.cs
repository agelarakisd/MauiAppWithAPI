using PassMaui.APIServices;
using System;
using System.Net.Http;
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
            httpClient.Timeout = TimeSpan.FromSeconds(timeoutInSeconds);

            string baseApiUrl;
            if (Device.RuntimePlatform == Device.Android)
            {
                baseApiUrl = "http://10.0.2.2:5119";
            }
            else if (Device.RuntimePlatform == Device.WinUI)
            {
                baseApiUrl = "http://localhost:5119";
            }
            else
            {
                baseApiUrl = "http://localhost:5119";
            }

            httpClient.BaseAddress = new Uri(baseApiUrl);

            return new AccountApiService(httpClient);
        }
    }
}
