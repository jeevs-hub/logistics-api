using logistics_api.Models;
using Newtonsoft.Json;

namespace logistics_api.Repository
{
    public interface IMenuRepository
    {
        Menu GetMenu();
    }

    public class MenuRepository : IMenuRepository
    {
        private readonly string _filePath;
        private readonly ILogger<MenuRepository> _logger;
        private Menu _menuData;

        public MenuRepository(string filePath, ILogger<MenuRepository> logger)
        {
            _filePath = filePath;
            _logger = logger;
            LoadMenu();
        }

        private void LoadMenu()
        {
            try
            {
                var jsonData = File.ReadAllText(_filePath);
                _menuData = JsonConvert.DeserializeObject<Menu>(jsonData);   
            }
            catch (Exception ex)
            {
                _logger.LogError("Error loading menu data: " + ex.Message);
                throw;
            }
        }

        public Menu GetMenu()
        {
            return _menuData;
        }
    }
}
