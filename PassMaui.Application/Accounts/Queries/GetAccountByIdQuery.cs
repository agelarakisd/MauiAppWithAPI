using MediatR;
using PassMaui.Application.Common.Interfaces;
using PassMaui.Domain;

namespace PassMaui.Application.Accounts.Queries
{
    public record GetAccountByIdQuery(int Id) : IRequest<Account>
    { }

    public class GetAccountByIdQueryHandler : IRequestHandler<GetAccountByIdQuery, Account>
    {
        private readonly IAccountRepository _accountRepository;
        public GetAccountByIdQueryHandler(IAccountRepository repository)
        {
            _accountRepository = repository;   
        }
        public async Task<Account> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
        {
            return await _accountRepository.FindById(request.Id, cancellationToken);

        }
    }
}
