namespace Infrastructure.SemanticKernel.Extensions
{

    using Infrastructure.Options;
    using Infrastructure.SemanticKernel.Filters;
    using Infrastructure.SemanticKernel.Helpers;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
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

                services.AddSingleton<IFunctionInvocationFilter, LoggingFunctionFilter>();
                services.AddSingleton<IPromptRenderFilter, PromptRenderFilter>();

                services.AddScoped<Kernel>(serviceProvider =>
                {
                    var kernel = new Kernel(serviceProvider);
                    AIHelpers.AddPlugins(kernel);
                    return kernel;
                });

                return services;
            }
        }
    }
}