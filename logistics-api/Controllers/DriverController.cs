using logistics_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace logistics_api.Controllers
{
    [ApiController]
    [Route("Drivers")]
    public class DriverController : ControllerBase
    {
        private readonly IDriverService _driverService;

        public DriverController(ILogger<DriverController> logger, 
            IDriverService driverService)
        {
            _driverService = driverService;
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            return Ok(_driverService.GetDrivers());
        }

        [HttpGet("{nameFilter}")]
        public IActionResult Get(string nameFilter)
        {
            return Ok(_driverService.GetDrivers(nameFilter));
        }
    }
}
