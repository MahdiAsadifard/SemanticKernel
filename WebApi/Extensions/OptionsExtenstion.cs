namespace AISample.WebApi.Extensions
{

    using Models.Options;

    public static class OptionsExtensions
    {
        extension(IServiceCollection services)
        {
            public IServiceCollection AddAppOptions(IConfiguration configuration)
            {
                services.Configure<OllamaOptions>(configuration.GetSection(OllamaOptions.SectionName));
                services.Configure<AICafeHCLOptions>(configuration.GetSection(AICafeHCLOptions.SectionName));
                return services;
            }
        }
    }
}
