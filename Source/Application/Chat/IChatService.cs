
namespace Application.Chat
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.SemanticKernel;

    public interface IChatService
    {
        Task GetChatStreaming(string prompt, string conversationId, HttpResponse response, CancellationToken cancellationToken);

        Task<ChatMessageContent> GetChatMessageAsync(string prompt, string conversationId, CancellationToken cancellationToken);

    }
}
