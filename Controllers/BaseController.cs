using Microsoft.AspNetCore.Mvc;

namespace SPANTECH.Controllers
{
    // Base Controller with generic type T for logging
    [ApiController]
    public class BaseController<T> : ControllerBase
    {
        private readonly ILogger<T> _logger;

        // Constructor that injects logger
        public BaseController(ILogger<T> logger)
        {
            _logger = logger;
        }

        // Method to log information
        protected void LogInformation(string message, object details = null)
        {
            _logger.LogInformation($"Controller:{GetControllerName()}, Method:{GetMethodNameString()}, Message:{message}, Details:{details}");
        }

        // Method to log warning
        protected void LogWarning(string message, object details = null)
        {
            _logger.LogWarning($"Controller:{GetControllerName()}, Method:{GetMethodNameString()}, Message:{message}, Details:{details}");
        }

        // Helper to get the current controller's name
        protected string GetControllerName()
        {
            return ControllerContext.RouteData.Values["controller"].ToString();
        }

        // Helper to get the current method's name
        protected string GetMethodNameString()
        {
            return ControllerContext.ActionDescriptor.ActionName;
        }
    }
}
