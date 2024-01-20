using PassMaui.Domain;

namespace PassMaui.Application.Common.Interfaces;

public interface IAccountRepository
{
    Task<Account> Add(Account account, CancellationToken cancellationToken = default);
    Task<List<Account>> GetAllAccounts( CancellationToken cancellationToken = default);
    Task<Account> FindById(int id, CancellationToken cancellationToken = default);
    void Delete(Account account);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}