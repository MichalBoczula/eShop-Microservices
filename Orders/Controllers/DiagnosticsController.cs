using Microsoft.AspNetCore.Mvc;

namespace Orders.Controllers
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
            return "Orders works Brooo :)";
        }

        [HttpGet("DatabaseName")]
        public ActionResult<string> GetDatabaseName()
        {
            return "Database=eShop.Orders;";
        }
    }
}