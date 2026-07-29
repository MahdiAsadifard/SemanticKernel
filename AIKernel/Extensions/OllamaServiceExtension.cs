namespace AISample.AIKernel.Extensions
{
    using Microsoft.SemanticKernel;
    using Models.Options;
    public static class OllamaServiceExtension
    {
        extension(IServiceCollection services)
        {
            public IServiceCollection AddAppOllamaChatCompletion(IConfiguration configuration)
            {
                var options = configuration.GetSection(OllamaOptions.SectionName).Get<OllamaOptions>();
                if (options == null)
                {
                    throw new InvalidOperationException($"Configuration section '{OllamaOptions.SectionName}' is missing or invalid.");
                }

                services.AddOllamaChatCompletion(
                    modelId: options.Model,
                    endpoint: new Uri(options.Endpoint)
                    );

                services.AddScoped<Kernel>(serviceProvider => {
                    return new Kernel(serviceProvider);
                });

                return services;
            }
        }
    }
}
