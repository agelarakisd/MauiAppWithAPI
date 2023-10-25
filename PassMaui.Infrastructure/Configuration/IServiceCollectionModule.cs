using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PassMaui.API.Configuration;

public interface IServiceCollectionModule
{
    void Configure(IServiceCollection services, IConfiguration configuration);
}