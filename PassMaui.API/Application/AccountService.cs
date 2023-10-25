using PassMaui.Models;

namespace PassMaui.API.Application;

public interface IAccountService
{
    void Create(Account account);
}

public class AccountService : IAccountService
{
    public void Create(Account account)
    {
        throw new NotImplementedException();
    }
}