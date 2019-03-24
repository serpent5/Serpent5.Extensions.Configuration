using System;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace Serpent5.Extensions.Configuration.Tests
{
    [TestFixture]
    public class ConfigurationBuilderExtensionsTests
    {
        private const string environmentSettingName = "environmentSetting";

        [Test]
        public void TestAppSettings()
        {
            var configurationRoot = CreateConfigurationRoot();

            Assert.That(configurationRoot["appSettingsSetting"], Is.EqualTo("appSettingsValue"));
        }

        [Test]
        public void TestAppSettingsProduction()
        {
            var configurationRoot = CreateConfigurationRoot();

            Assert.That(configurationRoot["appSettingsProductionSetting"], Is.EqualTo("appSettingsProductionValue"));
        }

        [Test]
        public void TestEnvironmentVariable()
        {
            const string settingName = "environmentVariableSetting";
            const string value = "environmentVariableValue";

            Environment.SetEnvironmentVariable(settingName, value);

            var configurationRoot = CreateConfigurationRoot();

            Assert.That(configurationRoot[settingName], Is.EqualTo(value));
        }

        [Test]
        public void TestCommandLine()
        {
            const string settingName = "commandLineSetting";
            const string value = "commandLineValue";
            var configurationRoot = CreateConfigurationRoot(new[] { $"--{settingName}={value}" });

            Assert.That(configurationRoot[settingName], Is.EqualTo(value));
        }

        [Test]
        public void TestAppSettingsProductionOverride()
        {
            var configurationRoot = CreateConfigurationRoot();

            Assert.That(configurationRoot[environmentSettingName], Is.EqualTo("productionOverrideValue"));
        }

        [Test]
        public void TestEnvironmentVariableOverride()
        {
            const string overrideValue = "environmentVariableOverrideValue";
            Environment.SetEnvironmentVariable(environmentSettingName, overrideValue);
            var configurationRoot = CreateConfigurationRoot();

            Assert.That(configurationRoot[environmentSettingName], Is.EqualTo(overrideValue));
        }

        [Test]
        public void TestCommandLineOverride()
        {
            const string overrideValue = "commandLineOverrideValue";
            var configurationRoot = CreateConfigurationRoot(new[] { $"--{environmentSettingName}={overrideValue}" });

            Assert.That(configurationRoot[environmentSettingName], Is.EqualTo(overrideValue));
        }

        private static IConfigurationRoot CreateConfigurationRoot(string[] args = null) =>
            new ConfigurationBuilder()
                .AddDefaultProviders(args)
                .Build();
    }
}
