namespace Infrastructure.SemanticKernel.Extensions
{

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.SemanticKernel;

    using Infrastructure.Options;
    using Infrastructure.SemanticKernel.Helpers;
    using Infrastructure.SemanticKernel.Filters;

    public static class OpenAIServiceExtension
    {
        extension(IServiceCollection services)
        {
            public IServiceCollection AddAppOpenAIChatCompletion(IConfiguration configuration)
            {
                var options = configuration.GetSection(AICafeHCLOption.SectionName).Get<AICafeHCLOption>();
                if (options == null)
                {
                    throw new InvalidOperationException($"Configuration section '{AICafeHCLOption.SectionName}' is missing or invalid.");
                }

                services.AddAzureOpenAIChatCompletion(
                    deploymentName: options.Endpoint,
                    modelId: options.Model,
                    endpoint: options.Endpoint,
                    apiKey: options.ApiKey
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
