using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Template;

namespace Api.Controllers
{
    [ApiController]
    [Route(RouteTemplate)]
    public class BaseController : Controller
    {
        private const string RouteTemplate = "api/{Controller}";
    }
}
