namespace Api.Extensions
{
    public static class LoggingExtension
    {
        extension(IServiceCollection services)
        {
            public IServiceCollection AddAppLogging()
            {
                services.AddLogging(logging => {
                    logging.AddConsole(c => {
                        //c.DisableColors = false;
                        //c.Format = Microsoft.Extensions.Logging.Console.ConsoleLoggerFormat.Default;
                        c.FormatterName = Microsoft.Extensions.Logging.Console.ConsoleFormatterNames.Simple;
                    });
                    logging.SetMinimumLevel(LogLevel.Information);
                });
                return services;
            }
        }
    }
}
