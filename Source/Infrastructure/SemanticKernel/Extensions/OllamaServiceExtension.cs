namespace Infrastructure.SemanticKernel.Extensions
{

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using Infrastructure.Options;
    using Microsoft.SemanticKernel;

    public static class OllamaServiceExtension
    {
        extension(IServiceCollection services)
        {
            public IServiceCollection AddAppOllamaChatCompletion(IConfiguration configuration)
            {
                var options = configuration.GetSection(OllamaOption.SectionName).Get<OllamaOption>();
                if (options == null)
                {
                    throw new InvalidOperationException($"Configuration section '{OllamaOption.SectionName}' is missing or invalid.");
                }

                services.AddOllamaChatCompletion(
                    modelId: options.Model,
                    endpoint: new Uri(options.Endpoint)
                    );

                services.AddScoped<Kernel>(serviceProvider =>
                {
                    return new Kernel(serviceProvider);
                });

                return services;
            }
        }
    }
}