namespace Api.Extensions
{
    using Infrastructure.Options;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class OptionsExtensions
    {
        extension(IServiceCollection services)
        {
            public IServiceCollection AddAppOptions(IConfiguration configuration)
            {
                services.Configure<OllamaOption>(configuration.GetSection(OllamaOption.SectionName));
                services.Configure<AICafeHCLOption>(configuration.GetSection(AICafeHCLOption.SectionName));
                services.Configure<SemanticKernelOptions>(configuration.GetSection(SemanticKernelOptions.SectionName));

                return services;
            }
        }
    }
}
