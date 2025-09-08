using Microsoft.Extensions.Configuration;

namespace Dneprokos.Helper.Base.Client.Configuration
{
    public sealed class ConfigManager
    {
        private static readonly Lazy<ConfigManager> _instance =
            new(() => new ConfigManager(), LazyThreadSafetyMode.ExecutionAndPublication);

        private readonly IConfigurationRoot _configuration;

        private ConfigManager()
        {
            // Load config file once - this is expensive I/O operation
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
        }

        public static ConfigManager Instance => _instance.Value;

        public string Get(string key) => _configuration[key]!;
    }
}
