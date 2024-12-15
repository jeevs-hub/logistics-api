namespace logistics_api.Models
{
    public class MenuItem
    {
        public string Title { get; set; }
        public string Url { get; set; }
    }

    public class Menu
    {
        public MenuItem[] data { get; set; }
    }
}
