using AISample.AIKernel.AIServices;
using AISample.AIKernel.AIServices.Interfaces;
using AISample.Core.AppMemoryCache;
using AISample.Services.Chat;
using AISample.Services.Interfaces;
using Microsoft.SemanticKernel.ChatCompletion;

namespace AISample.WebApi.Extensions
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
                services.AddScoped<IChat, Chat>();
                services.AddScoped<IChatService, ChatService>();
                
                // ======== Transients ========

                return services;
            }
        }
    }
}
