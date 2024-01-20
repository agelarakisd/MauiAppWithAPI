using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PassMaui.API.Configuration;
using PassMaui.Application.Accounts.Commands;
using MediatR;
using PassMaui.Infrastructure.Accounts.Persistence;
using PassMaui.Application.Common.Interfaces;

namespace PassMaui.Infrastructure;

public class InfrastructureCollectionModule : IServiceCollectionModule
{
    public void Configure(IServiceCollection services, IConfiguration configuration)
    {
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        if (configuration is null)
        {
            throw new ArgumentNullException(nameof(configuration));
        }

        services.AddDbContext<ApplicationContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("PassMauiDb");
            options.UseSqlServer(connectionString);
        });

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateAccountCommand>());
        services.AddScoped<IAccountRepository, AccountRepository>();
    }
}