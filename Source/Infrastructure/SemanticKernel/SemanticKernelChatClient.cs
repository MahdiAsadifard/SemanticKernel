using Infrastructure.Caching;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.SemanticKernel
{
    public class SemanticKernelChatClient : ISemanticKernelChatClient
    {

        private readonly Kernel _kernel;
        private readonly IMemoryCacheStore _memoryCacheStore;
        private readonly IChatCompletionService _chatCompletionService;
        private readonly PromptExecutionSettings _promptExecutionSettings;

        public SemanticKernelChatClient(
            Kernel kernel,
            IMemoryCacheStore memoryCacheStore,
            IChatCompletionService chatCompletionService,
            PromptExecutionSettings promptExecutionSettings = null)
        {
            this._kernel = kernel;
            this._memoryCacheStore = memoryCacheStore;
            this._chatCompletionService = chatCompletionService;

            this._promptExecutionSettings = promptExecutionSettings ??
                new PromptExecutionSettings()
                {
                    FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
                };
        }

        public async IAsyncEnumerable<StreamingChatMessageContent> GetChatStreaming(
            string prompt,
            string conversationId, 
            [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(prompt, nameof(prompt));

            var conversationHistory = this._memoryCacheStore.GetOrCreate<ChatHistory>(conversationId, _ => new ChatHistory());
            conversationHistory.AddUserMessage(prompt);

            var messageStream = _chatCompletionService.GetStreamingChatMessageContentsAsync(
                chatHistory: conversationHistory,
                kernel: this._kernel,
                executionSettings: this._promptExecutionSettings,
                cancellationToken: cancellationToken);

            await foreach (var message in messageStream.WithCancellation(cancellationToken))
            {
                yield return message;
            }

        }

        public async Task<ChatMessageContent> GetChatMessageAsync(string prompt, string conversationId, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(prompt, nameof(prompt));

            var conversationHistory = this._memoryCacheStore.GetOrCreate<ChatHistory>(conversationId, _ => new ChatHistory());
            conversationHistory.AddUserMessage(prompt);

            var result = await _chatCompletionService.GetChatMessageContentAsync(
                chatHistory: conversationHistory,
                kernel: this._kernel,
                executionSettings: this._promptExecutionSettings, 
                cancellationToken: cancellationToken);
            return result;
        }
    }
}

