using Microsoft.Extensions.Configuration;

namespace PorkbunDynamicDNS
{
    public static class ConfigurationManager
    {
        private static readonly IConfiguration AppSettings;

        static ConfigurationManager()
        {
            AppSettings = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
        }
        public static string Get(string key)
        {
            return AppSettings[key] ?? string.Empty;
        }
    }
}
