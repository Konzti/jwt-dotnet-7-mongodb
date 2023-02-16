namespace JwtDotNet7.Settings
{
    public static class ApplicationSettingsBuilder
    {
        public static IHostBuilder ConfigureApplicationSettings(this IHostBuilder builder)
        {
            return builder.ConfigureAppConfiguration((hostingContext, config) =>
            {

                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                config.AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true);
                config.AddEnvironmentVariables();
            });
        }
    }
}