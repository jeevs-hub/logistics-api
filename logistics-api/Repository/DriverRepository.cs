using logistics_api.Models;
using Newtonsoft.Json;

namespace logistics_api.Repository
{
    public interface IDriverRepository
    {
        Drivers GetDrivers();
    }

    public class DriverRepository : IDriverRepository
    {
        private readonly string _filePath;
        private readonly ILogger<DriverRepository> _logger;
        private Drivers _driverData;

        public DriverRepository(string filePath, ILogger<DriverRepository> logger)
        {
            _filePath = filePath;
            _logger = logger;
            LoadDrivers();
        }

        private void LoadDrivers()
        {
            try
            {
                var jsonData = File.ReadAllText(_filePath);
                _driverData = JsonConvert.DeserializeObject<Drivers>(jsonData);         
            }
            catch (Exception ex)
            {
                _logger.LogError("Error loading driver data: " + ex.Message);
                throw;
            }
        }

        public Drivers GetDrivers()
        {
            return _driverData;
        }
    }
}
