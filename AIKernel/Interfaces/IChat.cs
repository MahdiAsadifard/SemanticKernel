using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

namespace AISample.AIKernel.AIServices.Interfaces
{
    public interface IChat
    {
        IAsyncEnumerable<StreamingChatMessageContent> GetChatStreaming(string prompt, string conversationId);

        Task<ChatMessageContent> GetChatMessageAsync(string prompt);
    }
}
