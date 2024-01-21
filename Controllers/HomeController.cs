using JsonTree.Services;
using Microsoft.AspNetCore.Mvc;


namespace JsonTree.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ConfigurationKeyService _configurationKeyService;

        public HomeController(ILogger<HomeController> logger, ConfigurationKeyService configurationKeyService)
        {
            _logger = logger;
            _configurationKeyService = configurationKeyService;
        }

        public IActionResult Index()
        {
            string path = HttpContext.Request.Path;

            if (path.Contains("key"))
            {
                return GetUrlConfig(path);
            }
            else
            {
                return RedirectToAction("Index","Object");
            }
        }
        public IActionResult GetUrlConfig(string keys )
        {
            string[] keyArray = keys.Split('/');

            var configuration = _configurationKeyService.GetConfig(keyArray);

            if (configuration == null)
            {
                return NotFound("Configuration not found");
            }

            return View("Index", configuration);
        }
    }
}
