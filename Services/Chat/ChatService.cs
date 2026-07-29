using AISample.AIKernel.AIServices.Interfaces;
using AISample.Services.Interfaces;
using Azure;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

namespace AISample.Services.Chat
{
    public class ChatService : IChatService
    {
        private readonly IChat _chat;
        ILogger<ChatService> _logger;

        public ChatService(
            IChat chat,
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
            Console.WriteLine("-----");
            await response.Body.FlushAsync();
        }

        public async Task<ChatMessageContent> GetChatMessageAsync(string prompt)
        {
            ArgumentNullException.ThrowIfNull(prompt, nameof(prompt));
            _logger.Log(LogLevel.Information, "GetChatMessageAsync called with prompt: {Prompt}", prompt);

            var chatMessage = await _chat.GetChatMessageAsync(prompt);

            return chatMessage;
        }
    }
}
