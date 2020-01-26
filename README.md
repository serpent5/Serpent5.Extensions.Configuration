# Serpent5.Extensions.Configuration

[![Nuget](https://img.shields.io/nuget/v/Serpent5.Extensions.Configuration.svg)](https://www.nuget.org/packages/Serpent5.Extensions.Configuration)

**Serpent5.Extensions.Configuration** provides a single `AddHostDefaults` extension method for `IConfigurationBuilder`, which configures the following configuration pipeline:

- appsettings.json (optional, reloadOnChange)
- appsettings.[Environment].json (optional, reloadOnChange)
- User Secrets (Development only)
- Environment Variables
- Command Line

This configuration pipeline is intended to mimic that provided by [`Host.CreateDefaultBuilder`](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.hosting.host.createdefaultbuilder?view=dotnet-plat-ext-3.1), allowing for use outside of a `Host`-based environment.
