using System.Data;
using System.Text.Json;
using logistics_api.Extensions;
using logistics_api.Models;
using logistics_api.Repository;
using Newtonsoft.Json;

namespace logistics_api.Services
{
    public interface IDriverService
    {
        DriverDto[] GetDrivers();
        DriverDto[] GetDrivers(string nameFilter);
    }

    public class DriverService : IDriverService
    {
        private readonly IDriverRepository _driverRepository;

        public DriverService(IDriverRepository driverRepository)
        {
            _driverRepository = driverRepository;
        }

        public DriverDto[] GetDrivers()
        {
            return _driverRepository.GetDrivers()
                .data
                .Select(FormatDriverAsDriverDto)
                .ToArray();
        }

        public DriverDto[] GetDrivers(string nameFilter)
        {
            var drivers = _driverRepository.GetDrivers();
            return drivers.data
                .Where(driver => driver.Forename.ContainsIgnoreCase(nameFilter) || driver.Surname.ContainsIgnoreCase(nameFilter))
                .Select(FormatDriverAsDriverDto)
                .ToArray();
        }

        private static DriverDto FormatDriverAsDriverDto(Driver driver)
        {
            var minsWorked = 0;
            var daysWorked = new List<int>();
            var groupedActivityTimes = new Dictionary<string, int>();

            driver.Traces.ToList().ForEach(trace =>
            {
                //get the day as an index using monday as 0 tues as 1 etc.
                //default behaviour is sunday 0, mon 1 etc
                var dayWorked = (((int)trace.Date.DayOfWeek) + 6) % 7;
                daysWorked.Add(dayWorked);
                trace.Activity.ToList().ForEach(activity =>
                {
                    minsWorked += activity.Duration;
                    int groupedActivityVal = 0;
                    groupedActivityTimes.TryGetValue(activity.Type, out groupedActivityVal);

                    groupedActivityVal += activity.Duration;
                    groupedActivityTimes[activity.Type] = groupedActivityVal;
                });
            });

            return new DriverDto
            {
                daysWorked = daysWorked,
                Id = driver.DriverID,
                Name = $"{driver.Forename} {driver.Surname}",
                MinsWorked = minsWorked,
                VehicleRegistration = driver.VehicleRegistration,
                groupedActivityTimes = groupedActivityTimes
            };
        }
    }
}
