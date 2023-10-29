using Microsoft.Extensions.Configuration;

namespace PassMaui.Infrastructure.Configuration;

public class Configuration
{
    /// <summary>
    /// Creates a new instance of <see cref="IConfiguration"/> and gives access to app settings before configuring host's configuration builder.
    /// <para>Settings are loaded with the following order:</para>
    /// <br>1. appsettings.json</br>
    /// <br>2. environment variables</br>
    /// <br>3. user secrets when running locally</br>
    /// <br>4. appsettings.ENVIRONMENT.json</br>
    /// </summary>
    /// <typeparam name="TEntryPoint">The type to use when resolving the entry assembly.</typeparam>
    /// <returns></returns>
    public static IConfigurationRoot Create<TEntryPoint>()
        where TEntryPoint : class
    {
        var entryAssembly = typeof(TEntryPoint).Assembly;

        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.GetDirectoryName(entryAssembly.Location))
            .AddJsonFile("appsettings.json", optional: false)
            .AddEnvironmentVariables();

        var configuration = builder.Build();
        return configuration;
    }
}