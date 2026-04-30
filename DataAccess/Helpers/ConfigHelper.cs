using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace onyx_services_core.DataAccess.Helpers
{
    public class ConfigHelper
    {



        private static string GetSetting(string key)
        {
            var _configuration = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
          .Build();

            return _configuration[key];
        }

        public static string GetDefaultConnection()
        {
            return GetSetting("ConnectionStrings:DefaultConnection");
        }
    }
}
