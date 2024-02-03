using MediatR;
using PassMaui.Application.Common.Interfaces;
using PassMaui.Domain;

namespace PassMaui.Application.Accounts.Commands;

public class CreateAccountCommand : IRequest<Account>
{
    public string Site { get; set; }

    public string Description { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }
}

public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, Account>
{
    private readonly IAccountRepository _repository;

    public CreateAccountCommandHandler(IAccountRepository repository)
    {
        _repository = repository;
    }

    public async Task<Account> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        var account = Account.Create(
            request.Site, 
            request.Description,
            request.Username, 
            request.Password);

        var createdAccount = await _repository.Add(account, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        return createdAccount; 
    }
}
