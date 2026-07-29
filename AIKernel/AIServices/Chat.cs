using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using AISample.AIKernel.AIServices.Interfaces;
using AISample.Core.AppMemoryCache;

namespace AISample.AIKernel.AIServices
{
    public class Chat : IChat
    {
        private readonly Kernel _kernel;
        private readonly ChatHistory _chatHistory; // TODO: remove
        private readonly IMemoryCacheStore _memoryCacheStore;
        private readonly IChatCompletionService _chatCompletionService;
        private readonly PromptExecutionSettings _promptExecutionSettings;

        public Chat(
            Kernel kernel,
            ChatHistory chatHistory,
            IMemoryCacheStore memoryCacheStore,
            IChatCompletionService chatCompletionService,
            PromptExecutionSettings promptExecutionSettings = null)
        {
            this._kernel = kernel;
            this._chatHistory = chatHistory;
            this._memoryCacheStore = memoryCacheStore;
            this._chatCompletionService = chatCompletionService;

            this._promptExecutionSettings = promptExecutionSettings ??
                new PromptExecutionSettings()
                {
                    FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
                };
        }

        public async IAsyncEnumerable<StreamingChatMessageContent> GetChatStreaming(string prompt, string conversationId)
        {
            ArgumentNullException.ThrowIfNull(prompt, nameof(prompt));

            var conversationHistory = this._memoryCacheStore.GetOrCreate<ChatHistory>(conversationId);
            conversationHistory.AddUserMessage(prompt);

            //var x = this._memoryCacheStore.GetOrCreate<ChatHistory>(conversationId, _ => new ChatHistory());

            var msg = _chatCompletionService.GetStreamingChatMessageContentsAsync(
                chatHistory: conversationHistory,
                kernel: this._kernel,
                executionSettings: this._promptExecutionSettings);

            await foreach (var message in msg)
            {
                yield return message;
            }

        }

        public async Task<ChatMessageContent> GetChatMessageAsync(string prompt)
        { 
            ArgumentNullException.ThrowIfNull(prompt, nameof(prompt));

            this._chatHistory.AddUserMessage(prompt);

            var result = await _chatCompletionService.GetChatMessageContentAsync(
                chatHistory: this._chatHistory,
                kernel: this._kernel,
                executionSettings: this._promptExecutionSettings);
            return result;
        }
    }
}
