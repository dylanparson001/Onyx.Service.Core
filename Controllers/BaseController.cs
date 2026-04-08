using Microsoft.AspNetCore.Mvc;

namespace onyx_services_core.Controllers
{

    [ApiController]
    public class BaseController : ControllerBase
    {
        protected ILogger _logger;

        public BaseController(ILogger logger)
        {
            _logger = logger;
        }
    }
}
