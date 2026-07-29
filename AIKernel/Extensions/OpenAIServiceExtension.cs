namespace AISample.AIKernel.Extensions
{
    using Microsoft.SemanticKernel;
    using Models.Options;
    public static class OpenAIServiceExtension
    {
        extension(IServiceCollection services)
        {
            public IServiceCollection AddAppOpenAIChatCompletion(IConfiguration configuration)
            {
                var options = configuration.GetSection(AICafeHCLOptions.SectionName).Get<AICafeHCLOptions>();
                if (options == null)
                {
                    throw new InvalidOperationException($"Configuration section '{AICafeHCLOptions.SectionName}' is missing or invalid.");
                }

                services.AddAzureOpenAIChatCompletion(
                    deploymentName: options.Endpoint,
                    modelId: options.Model,
                    endpoint: options.Endpoint,
                    apiKey: options.ApiKey
                    );

                services.AddScoped<Kernel>(serviceProvider => {
                    return new Kernel(serviceProvider);
                });

                return services;
            }
        }
    }
}
