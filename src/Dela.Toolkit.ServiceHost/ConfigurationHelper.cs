namespace Dela.Toolkit.ServiceHost;

public static class ConfigurationHelper
{ 
    public static IConfiguration GetConfiguration(this IHostEnvironment host)
    { 
        return new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{host.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();
    }
}