using Dneprokos.Helper.Base.Client.Configuration;
using Dneprokos.Helper.Base.Client.Loggers.Constants;
using NLog;
using NLog.Config;
using NLog.Extensions.Logging;
using NLog.Targets;
using LogLevel = NLog.LogLevel;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Dneprokos.Helper.Base.Client.Loggers.Managers
{
    public class NLogLogger
    {
        private NLogLogger()
        {
            var config = new LoggingConfiguration();

            if (File.Exists(LoggerConstants.NlogConfigFile))
            {
                // Read and load configuration from the file
                var xmlConfigurator = new XmlLoggingConfiguration(LoggerConstants.NlogConfigFile);
                config = xmlConfigurator;

                // Read runsettings and add additional configurations
                ReconfigureLoggerFileFromRunSettings(config);
            }
            else
            {
                //Create a target
                var consoleTarget = new ColoredConsoleTarget
                {
                    Layout = "${longdata} ${level} ${message}"
                };

                config.AddTarget(consoleTarget);

                // Define logging rules
                var consoleRule = new LoggingRule("*", LogLevel.Info, consoleTarget);

                // Add configuration rules
                config.LoggingRules.Add(consoleRule);
            }

            LogManager.Configuration = config;
            LoggerFactory = new NLogLoggerFactory();
            Logger = LoggerFactory.CreateLogger(LoggerConstants.LogDefaultName);
        }

        private static readonly Lazy<NLogLogger> lazyInit 
            = new(() => new NLogLogger(), LazyThreadSafetyMode.ExecutionAndPublication);

        public static NLogLogger Instance => lazyInit.Value;

        public NLogLoggerFactory LoggerFactory { get; set; }

        public ILogger Logger { get; set; }


        private void ReconfigureLoggerFileFromRunSettings(LoggingConfiguration configuration)
        {
            string? logLevel = RunSettingsHelper.GetNullAbleStringSetting(LoggerConstants.LogLevel);
            if (!string.IsNullOrEmpty(logLevel)) 
            {
                configuration.Variables[LoggerConstants.NlogMyLevelKey] = logLevel;
            }

            string? logPath = RunSettingsHelper.GetNullAbleStringSetting(LoggerConstants.LogPath);
            if (!string.IsNullOrEmpty(logPath))
            {
                var target = (FileTarget)configuration.FindTargetByName("logfile");
                target.FileName = logPath + "\\logs\\Debug.${cached:${date:format=yyyy-MM-dd}}.log";
            }
        }
    }
}
