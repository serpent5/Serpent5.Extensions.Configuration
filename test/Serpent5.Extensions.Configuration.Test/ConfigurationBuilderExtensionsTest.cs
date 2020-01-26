using System;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Serpent5.Extensions.Configuration.Test
{
    public class ConfigurationBuilderExtensionsTest
    {
        private const string environmentSettingName = "environmentSetting";

        [Fact]
        public void TestAppSettings()
        {
            var configurationRoot = CreateConfigurationRoot();

            configurationRoot["appSettingsSetting"].Should().Be("appSettingsValue");
        }

        [Fact]
        public void TestAppSettingsProduction()
        {
            var configurationRoot = CreateConfigurationRoot();

            configurationRoot["appSettingsProductionSetting"].Should().Be("appSettingsProductionValue");
        }

        [Fact]
        public void TestEnvironmentVariable()
        {
            const string settingName = "environmentVariableSetting";
            const string value = "environmentVariableValue";

            Environment.SetEnvironmentVariable(settingName, value);

            var configurationRoot = CreateConfigurationRoot();

            configurationRoot[settingName].Should().Be(value);
        }

        [Fact]
        public void TestCommandLine()
        {
            const string settingName = "commandLineSetting";
            const string value = "commandLineValue";

            var configurationRoot = CreateConfigurationRoot(new[] { $"--{settingName}={value}" });

            configurationRoot[settingName].Should().Be(value);
        }

        [Fact]
        public void TestAppSettingsProductionOverride()
        {
            var configurationRoot = CreateConfigurationRoot();

            configurationRoot[environmentSettingName].Should().Be("productionOverrideValue");
        }

        [Fact]
        public void TestEnvironmentVariableOverride()
        {
            const string overrideValue = "environmentVariableOverrideValue";
            Environment.SetEnvironmentVariable(environmentSettingName, overrideValue);

            try
            {
                var configurationRoot = CreateConfigurationRoot();

                configurationRoot[environmentSettingName].Should().Be(overrideValue);
            }
            finally
            {
                Environment.SetEnvironmentVariable(environmentSettingName, null);
            }
        }

        [Fact]
        public void TestCommandLineOverride()
        {
            const string overrideValue = "commandLineOverrideValue";

            var configurationRoot = CreateConfigurationRoot(new[] { $"--{environmentSettingName}={overrideValue}" });

            configurationRoot[environmentSettingName].Should().Be(overrideValue);
        }

        private static IConfigurationRoot CreateConfigurationRoot(string[] args = null) =>
            new ConfigurationBuilder()
                .AddHostDefaults(args)
                .Build();
    }
}
