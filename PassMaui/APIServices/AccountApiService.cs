using Newtonsoft.Json;
using PassMaui.Domain;

namespace PassMaui.APIServices
{
    interface IAccountApiService
    {
        Task<List<Account>> GetAllAccounts();
    }

    public class AccountApiService : IAccountApiService
    {
        private const string BaseAccountApiPath = "api/accounts";
        private readonly HttpClient _client;

        public AccountApiService(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<Account>> GetAllAccounts()
        {
            var result = await _client.GetAsync(BaseAccountApiPath);

            if (!result.IsSuccessStatusCode)
            {
                throw new Exception("RIP");
            }

            var content = await result.Content.ReadAsStringAsync();

            var accounts = JsonConvert.DeserializeObject<List<Account>>(content);
            return accounts;
        }
    }
}
