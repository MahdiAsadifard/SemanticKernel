using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

namespace AISample.Services.Interfaces
{
    public interface IChatService
    {
        Task GetChatStreaming(string prompt, string conversationId, HttpResponse response);

        Task<ChatMessageContent> GetChatMessageAsync(string prompt);
    }
}
