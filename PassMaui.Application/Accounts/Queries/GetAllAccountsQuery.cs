using MediatR;
using PassMaui.Application.Common.Interfaces;
using PassMaui.Domain;

namespace PassMaui.Application.Accounts.Queries
{
    public class GetAllAccountsQuery : IRequest<List<Account>>
    {

    }

    public class GetAllAccountsQueryHandler : IRequestHandler<GetAllAccountsQuery, List<Account>>
    {
        private readonly IAccountRepository _accountRepository;
        public GetAllAccountsQueryHandler(IAccountRepository repository)
        {
            _accountRepository = repository;
        }

        public async Task<List<Account>> Handle(GetAllAccountsQuery request, CancellationToken cancellationToken)
        {
            return await _accountRepository.GetAllAccounts(cancellationToken);
        }
    }
}
