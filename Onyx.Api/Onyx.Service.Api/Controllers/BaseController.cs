using Microsoft.AspNetCore.Mvc;
namespace Onyx.Service.Api.Controllers

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
