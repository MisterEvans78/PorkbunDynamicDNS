using Microsoft.Extensions.Configuration;

namespace PorkbunDynamicDNS
{
    public class ConfigurationManager
    {
        private static readonly IConfiguration AppSettings;

        static ConfigurationManager()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            AppSettings = builder.Build();
        }
        public static string Get(string key)
        {
            return AppSettings[key] ?? string.Empty;
        }
    }
}
