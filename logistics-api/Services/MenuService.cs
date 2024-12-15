using logistics_api.Models;
using logistics_api.Repository;

namespace logistics_api.Services
{
    public interface IMenuService
    {
        MenuItem[] GetMenu();
    }

    public class MenuService : IMenuService
    {
        private readonly IMenuRepository _menuRepository;

        public MenuService(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }

        public MenuItem[] GetMenu()
        {
            return _menuRepository.GetMenu().data;
        }
    }
}
