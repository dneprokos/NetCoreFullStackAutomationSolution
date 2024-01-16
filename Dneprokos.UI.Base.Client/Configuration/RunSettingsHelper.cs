using FluentAssertions;
using NUnit.Framework;

namespace Dneprokos.UI.Base.Client.Configuration
{
    public class RunSettingsHelper
    {
        /// <summary>
        /// Reads a setting from the runsettings file. If the setting is not defined, the test will fail.
        /// </summary>
        /// <param name="settingName">Name of the setting in '<...>.runsettings' file</param>
        /// <returns>String setting value or fails if setting was not found</returns>
        public static string GetNotNullStringSetting(string settingName)
        {
            string? settingValue = TestContext.Parameters[settingName];
            settingValue.Should().NotBeNullOrEmpty($"Setting '{settingName}' is not defined in runsettings file");
            return settingValue!;
        }

        /// <summary>
        /// Gets a setting from the runsettings file. If the setting is not defined, then null is returned.
        /// </summary>
        /// <param name="settingName">Name of the setting in '<...>.runsettings' file</param>
        /// <returns>String setting value or null if value was not found</returns>
        public static string? GetNullAbleStringSetting(string settingName)
            => TestContext.Parameters[settingName];

        /// <summary>
        /// Reads a setting from the runsettings file. If the setting is not defined, the test will fail.
        /// </summary>
        /// <param name="settingName">Name of the setting in '<...>.runsettings' file</param>
        /// <returns>Boolean setting value or fails if value is null</returns>
        public static bool GetNotNullBooleanSetting(string settingName)
        {
            string stringValue = GetNotNullStringSetting(settingName);
            bool isParsed = bool.TryParse(stringValue, out bool settingValue);
            isParsed.Should().BeTrue($"Setting '{settingName}' is not a boolean value");
            return settingValue;
        }

        /// <summary>
        /// Reads a setting from the runsettings file. If the setting is not defined, then false is returned.
        /// </summary>
        /// <param name="settingName">Name of the setting in '<...>.runsettings' file</param>
        /// <returns>Boolean setting value. False in case if setting was not found</returns>
        public static bool GetNullableBooleanSetting(string settingName)
        {
            string? stringValue = GetNullAbleStringSetting(settingName);
            if (stringValue == null)
            {
                //TODO: Log that the setting is not defined
                return false;
            }

            bool isParsed = bool.TryParse(stringValue, out bool settingValue);
            isParsed.Should().BeTrue($"Setting '{settingName}' is not a boolean value");
            return settingValue;
        }

        /// <summary>
        /// Reads a setting from the runsettings file. If the setting is not defined, the test will fail.
        /// </summary>
        /// <param name="settingName">Name of the setting in '<...>.runsettings' file</param>
        /// <returns>Int setting value or fails if setting was not found</returns>
        public static int GetNotNullIntSetting(string settingName)
        {
            string stringValue = GetNotNullStringSetting(settingName);
            bool isParsed = int.TryParse(stringValue, out int settingValue);
            isParsed.Should().BeTrue($"Setting '{settingName}' is not an integer value");
            return settingValue;
        }

        /// <summary>
        /// Reads a setting from the runsettings file and converts it to integer. If the setting is not defined, then null is returned.
        /// </summary>
        /// <param name="settingName">Name of the setting in '<...>.runsettings' file</param>
        /// <returns>Int setting value or null if setting was not found</returns>
        public static int? GetNullAbleIntSetting(string settingName)
        {
            string? stringValue = TestContext.Parameters[settingName];
            return stringValue == null ? null : int.Parse(stringValue);
        }
    }
}
