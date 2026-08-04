using Application.Chat;
using Infrastructure.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Api.Controllers
{
    public class ChatController : BaseController
    {

        private readonly IChatService _chatService;
        private readonly IOptions<SemanticKernelOptions> _kernelOptions;

        TimeSpan _requestTimeout;

        public ChatController(IChatService chatService, IOptions<SemanticKernelOptions> kernelOptions)
        {
            this._chatService = chatService;
            this._kernelOptions = kernelOptions;
            this._requestTimeout = TimeSpan.FromSeconds(this._kernelOptions.Value.CancellationTokenTimeoutInSeconds);
        }

        [HttpPost("streaming/{conversationId}")]
        public async Task ChatStreaming(
          [FromRoute] string conversationId,
          [FromBody] string prompt)
        {
            CancellationToken cts = new CancellationTokenSource(this._requestTimeout).Token;
            await this._chatService.GetChatStreaming(prompt, conversationId, Response, cts);
        }

        [HttpPost("async/{conversationId}")]
        public async Task<IActionResult> ChatAsync(
            [FromRoute] string conversationId,
            [FromBody] string prompt)
        {
            CancellationToken cts = new CancellationTokenSource(this._requestTimeout).Token;
            var chatMessage = await this._chatService.GetChatMessageAsync(prompt, conversationId, cts);
            return Ok(chatMessage);
        }
    }
}
