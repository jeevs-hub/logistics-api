using logistics_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace logistics_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MenuController : ControllerBase
    {
        private readonly ILogger<MenuController> _logger;
        private readonly IMenuService _menuService;

        public MenuController(ILogger<MenuController> logger, 
            IMenuService menuService)
        {
            _logger = logger;
            _menuService = menuService;
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            return Ok(_menuService.GetMenu());
        }
    }
}
