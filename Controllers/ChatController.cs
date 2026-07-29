using AISample.Services.Interfaces;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SemanticKernel.ChatCompletion;

namespace AISample.Controllers
{
    public class ChatController : BaseController
    {

        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            this._chatService = chatService;
        }

        [HttpGet("sample")]
        public IActionResult Index()
        {
            return Ok("AI Sample Response");
        }

        [HttpPost("streaming/{conversationId}")]
        public async Task ChatStreaming(
            [FromRoute] string conversationId,
            [FromBody] string prompt)
        {
            await this._chatService.GetChatStreaming(prompt, conversationId, Response);
        }
        [HttpPost("async")]
        public async Task<IActionResult> ChatAsync([FromBody] string prompt)
        {
            var chatMessage = await this._chatService.GetChatMessageAsync(prompt);
            return Ok(chatMessage);
        }
    }
}
