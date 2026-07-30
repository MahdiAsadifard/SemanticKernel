using Application.Chat;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class ChatController : BaseController
    {

        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            this._chatService = chatService;
        }

        [HttpPost("streaming/{conversationId}")]
        public async Task ChatStreaming(
          [FromRoute] string conversationId,
          [FromBody] string prompt)
        {
            await this._chatService.GetChatStreaming(prompt, conversationId, Response);
        }

        [HttpPost("async/{conversationId}")]
        public async Task<IActionResult> ChatAsync(
            [FromRoute] string conversationId,
            [FromBody] string prompt)
        {
            var chatMessage = await this._chatService.GetChatMessageAsync(prompt, conversationId);
            return Ok(chatMessage);
        }
    }
}
