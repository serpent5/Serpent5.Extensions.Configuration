# Serpent5.Extensions.Configuration

![Nuget](https://img.shields.io/nuget/v/Serpent5.Extensions.Configuration.svg)

**Serpent5.Extensions.Configuration** provides a single `AddWebHostDefaults` extension method for `IConfigurationBuilder`, which configures the following configuration pipeline:

- appsettings.json (optional, reloadOnChange)
- appsettings.[Environment].json (optional, reloadOnChange)
- User Secrets (Development only)
- Environment Variables
- Command Line

This configuration pipeline is intended to mimic that provided by [`WebHost.CreateDefaultBuilder`](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.webhost.createdefaultbuilder?view=aspnetcore-2.2), allowing for use outside of a `WebHost`-based environment.
