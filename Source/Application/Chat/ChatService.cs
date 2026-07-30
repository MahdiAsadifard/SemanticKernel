using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Infrastructure.SemanticKernel;


namespace Application.Chat
{
    public class ChatService : IChatService
    {
        private readonly ISemanticKernelChatClient _chat;
        ILogger<ChatService> _logger;

        public ChatService(
            ISemanticKernelChatClient chat,
            ILogger<ChatService> logger)
        {
            this._chat = chat;
            this._logger = logger;
        }

        public async Task GetChatStreaming(string prompt, string conversationId, HttpResponse response)
        {
            ArgumentNullException.ThrowIfNull(prompt, nameof(prompt));
            _logger.Log(LogLevel.Information, "GetChatStreaming called with prompt: {Prompt}", prompt);

            Console.WriteLine("-----");
            var resultStream = this._chat.GetChatStreaming(prompt, conversationId);
            await foreach (StreamingChatMessageContent message in resultStream)
            {
                Console.Write(message.Content);
                await response.WriteAsync(message.Content);
            }
            Console.WriteLine(Environment.NewLine + "-----");
            await response.Body.FlushAsync();
        }

        public async Task<ChatMessageContent> GetChatMessageAsync(string prompt, string conversationId)
        {
            ArgumentNullException.ThrowIfNull(prompt, nameof(prompt));
            _logger.Log(LogLevel.Information, "GetChatMessageAsync called with prompt: {Prompt}", prompt);

            var chatMessage = await this._chat.GetChatMessageAsync(prompt, conversationId);
            Console.WriteLine(chatMessage.Content);

            return chatMessage;
        }
    }

}
