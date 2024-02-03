using Newtonsoft.Json;
using PassMaui.Domain;
using System.Net.Http.Json;

namespace PassMaui.APIServices
{
    public interface IAccountApiService
    {
        Task<List<Account>> GetAllAccounts();
        Task<Account> Update(Account account);
        Task<Account> GetAccount(int id);
        Task<Account> Create(Account account);
        Task Delete(int id);
    }

    public class AccountApiService : IAccountApiService
    {
        private const string BaseAccountApiPath = "api/accounts";
        private readonly HttpClient _client;

        public AccountApiService(HttpClient client)
        {
            _client = client;
        }

        public async Task<Account> Create(Account account)
        {
            var content = JsonContent.Create(account);
            var result = await _client.PostAsync(BaseAccountApiPath, content);

            if (!result.IsSuccessStatusCode)
            {
                throw new Exception("Failed to create account");
            }

            var response = await result.Content.ReadAsStringAsync();

            var createdAccount = JsonConvert.DeserializeObject<Account>(response);
            return createdAccount;
        }

        public async Task Delete(int id)
        {
            var result = await _client.DeleteAsync($"{BaseAccountApiPath}/{id}");
            if (!result.IsSuccessStatusCode) 
            {
                throw new Exception("Failed to delete account");
            }
        }

        public async Task<Account> GetAccount(int id)
        {
            var result = await _client.GetAsync($"{BaseAccountApiPath}/{id}");

            if (!result.IsSuccessStatusCode)
            {
                throw new Exception("RIP");
            }

            var response = await result.Content.ReadAsStringAsync();

            var account = JsonConvert.DeserializeObject<Account>(response);

            return account;
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

        public async Task<Account> Update(Account account)
        {
            var content = JsonContent.Create(account);
            var result = await _client.PutAsync(BaseAccountApiPath, content);

            if (!result.IsSuccessStatusCode)
            {
                throw new Exception("RIP");
            }

            var response = await result.Content.ReadAsStringAsync();

            var accounts = JsonConvert.DeserializeObject<Account>(response);
            return accounts;
        }
    }
}
