using Microsoft.SemanticKernel;

namespace Infrastructure.SemanticKernel
{
    public interface ISemanticKernelChatClient
    {
        IAsyncEnumerable<StreamingChatMessageContent> GetChatStreaming(string prompt, string conversationId);

        Task<ChatMessageContent> GetChatMessageAsync(string prompt, string conversationId);
    }
}
