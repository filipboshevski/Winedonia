using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace WineriesApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        private readonly ILogger<ErrorController> logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            this.logger = logger;
        }

        [Route("home")]
        public IActionResult Index()
        {
            var feature = HttpContext.Features.Get<IExceptionHandlerFeature>();

            if (feature?.Error != null)
            {
                var exception = feature.Error;
                logger.LogError(exception.Message, exception);
            }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}