using MediatR;
using PassMaui.Application.Common.Interfaces;
namespace PassMaui.Application.Accounts.Commands
{
    public record DeleteAccountCommand(int Id) : IRequest
    {}

    public class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand>
    { 
        private readonly IAccountRepository _accountRepository;
        public DeleteAccountCommandHandler(IAccountRepository repository)
        {
            _accountRepository = repository;
        }

        public async Task Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.FindById(request.Id, cancellationToken);
            _accountRepository.Delete(account);
            await _accountRepository.SaveChangesAsync(cancellationToken);
        }
    }

}
