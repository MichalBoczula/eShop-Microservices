using Microsoft.AspNetCore.Mvc;

namespace Products.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DiagnosticsController : ControllerBase
    {
        private readonly ILogger<DiagnosticsController> _logger;

        public DiagnosticsController(ILogger<DiagnosticsController> logger)
        {
            _logger = logger;
        }

        [HttpGet("HealthCheck")]
        public ActionResult<string> GetHealthCheck()
        {
            return "It's okay Brooo :)";
        }

        [HttpGet("DatabaseName")]
        public ActionResult<string> GetDatabaseName()
        {
            return "Database=eShop.Products;";
        }
    }
}