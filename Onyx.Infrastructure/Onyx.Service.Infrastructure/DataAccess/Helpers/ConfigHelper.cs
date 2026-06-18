using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;

namespace Onyx.Service.Infrastructure.DataAccess.Helpers
{
    public class ConfigHelper
    {
        private static string GetSetting(string key)
        {
            var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .Build();

            return configuration[key]!;
        }

        public static string GetDefaultConnection()
        {
            return GetSetting("ConnectionStrings:DefaultConnection");
        }
    }
}
