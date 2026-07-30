using Application.Chat;
using Infrastructure.Caching;
using Infrastructure.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

namespace Api.Extensions
{
    public static class ServicesExtension
    {
        extension(IServiceCollection services)
        {
            public IServiceCollection AddAppServices()
            {
                services.AddMemoryCache();

                // ======== Singeltons ========
                services.AddSingleton<IMemoryCacheStore, MemoryCacheStore>();

                // ======== Scoped ========
                services.AddScoped<ChatHistory>();
                services.AddScoped<ISemanticKernelChatClient, SemanticKernelChatClient>();
                services.AddScoped<IChatService, ChatService>();

                // ======== Transients ========

                return services;
            }
        }
    }
}
