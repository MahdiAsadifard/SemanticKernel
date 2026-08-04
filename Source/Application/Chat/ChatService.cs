using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.Extensions.Options;

using Infrastructure.SemanticKernel;
using Infrastructure.Options;


namespace Application.Chat
{
    public class ChatService : IChatService
    {
        private readonly ISemanticKernelChatClient _chat;
        ILogger<ChatService> _logger;

        public ChatService(
            ISemanticKernelChatClient chat,
            ILogger<ChatService> logger,
            IOptions<SemanticKernelOptions> kernelOptions)
        {
            this._chat = chat;
            this._logger = logger;
        }

        public async Task GetChatStreaming(string prompt, string conversationId, HttpResponse response, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(prompt, nameof(prompt));
            _logger.Log(LogLevel.Information, "GetChatStreaming called with prompt: {Prompt}", prompt);

            Console.WriteLine("-----");
            var resultStream = this._chat.GetChatStreaming(prompt, conversationId, cancellationToken);
            await foreach (StreamingChatMessageContent message in resultStream.WithCancellation(cancellationToken))
            {
                if (message is null || string.IsNullOrWhiteSpace(message.Content)) continue;

                Console.Write(message.Content);
                await response.WriteAsync(message.Content, cancellationToken);
            }
            Console.WriteLine(Environment.NewLine + "-----");
            await response.Body.FlushAsync(cancellationToken);
        }

        public async Task<ChatMessageContent> GetChatMessageAsync(string prompt, string conversationId, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(prompt, nameof(prompt));
            _logger.Log(LogLevel.Information, "GetChatMessageAsync called with prompt: {Prompt}", prompt);

            var chatMessage = await this._chat.GetChatMessageAsync(prompt, conversationId, cancellationToken);
            Console.WriteLine(chatMessage.Content ?? "NOT_PROVIDED");

            return chatMessage;
        }
    }

}
