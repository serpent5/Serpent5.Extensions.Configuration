using System;
using System.IO;
using System.Reflection;

namespace Microsoft.Extensions.Configuration;

public static class ConfigurationBuilderExtensions
{
    public static IConfigurationBuilder AddHostDefaults(
      this IConfigurationBuilder configurationBuilder, string[] args = null)
    {
        _ = configurationBuilder ?? throw new ArgumentNullException(nameof(configurationBuilder));

        var aspnetCoreEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
            ?? Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")
            ?? "Production";

        configurationBuilder
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{aspnetCoreEnvironment}.json", optional: true, reloadOnChange: true);

        if (aspnetCoreEnvironment == "Development")
        {
            var entryAssembly = Assembly.GetEntryAssembly();

            if (entryAssembly != null)
                configurationBuilder.AddUserSecrets(entryAssembly, optional: true);
        }

        configurationBuilder.AddEnvironmentVariables();

        if (args != null)
            configurationBuilder.AddCommandLine(args);

        return configurationBuilder;
    }
}
