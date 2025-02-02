using Serilog;

namespace ProjectEverythingForHomeOnlineShop.Infrastructure
{
    public class LoggingConfig
    {
        public static void ConfigureLogger()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()  
                .WriteTo.Console()            
                .WriteTo.Debug()
                .WriteTo.File("logs/myapp.log",   
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 7,     
                    fileSizeLimitBytes: 10_000_000, 
                    rollOnFileSizeLimit: true,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level}] {Message}{NewLine}{Exception}"
                )
                .CreateLogger();
        }
    }
}
