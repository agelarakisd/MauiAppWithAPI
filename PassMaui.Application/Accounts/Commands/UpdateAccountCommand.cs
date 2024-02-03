using MediatR;
using PassMaui.Application.Common.Interfaces;
using PassMaui.Domain;

namespace PassMaui.Application.Accounts.Commands
{
    public record UpdateAccountCommand(int Id, string Site, string Description , string Username, string Password) : IRequest<Account>
    {}

    public class UpdateAccountCommandHandler : IRequestHandler<UpdateAccountCommand,Account>
    {
        private readonly IAccountRepository _accountRepository;
        public UpdateAccountCommandHandler(IAccountRepository repository)
        {
            _accountRepository = repository;
        }

        public async Task<Account> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.FindById(request.Id, cancellationToken);
            account.ChangeAccountDetails(request.Site,request.Description,request.Username,request.Password);
            await _accountRepository.Update(account,cancellationToken);
            return account;
        }
    }
}
