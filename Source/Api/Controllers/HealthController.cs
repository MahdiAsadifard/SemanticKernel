using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class HealthController : BaseController
    {
        [HttpGet("sync")]
        public IActionResult Index()
        {
            while (true)
            {
                Task.Delay(TimeSpan.FromSeconds(1));
                var now = DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss UTC");
                Console.WriteLine($"Base controller running at {now}  ...");
                return Ok($"Running at {now}  ...");
            }
        }

        [HttpGet("stream")]
        public async Task HealthStream(CancellationToken cancellationToken)
        {
            Response.ContentType = "text/event-stream";
            while (!cancellationToken.IsCancellationRequested)
            {

                var now = DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss UTC");
                var message = $"data: Base controller running at {now}  ...\n\n";

                await Response.WriteAsync(message, cancellationToken);
                await Response.Body.FlushAsync(cancellationToken);

                Console.WriteLine(message);
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
        }
    }
}
