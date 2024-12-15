using Moq;
using logistics_api.Models;
using logistics_api.Services;
using logistics_api.Repository;

namespace logistics_api.Tests
{
    [TestClass]
    public class DriverServiceTests
    {
        private readonly Mock<IDriverRepository> _driverRepositoryMock;
        private readonly DriverService _driverService;

        public DriverServiceTests()
        {
            _driverRepositoryMock = new Mock<IDriverRepository>();
            _driverService = new DriverService(_driverRepositoryMock.Object);
        }

        [TestMethod]
        public void GetDrivers_Returns_All_Drivers()
        {
            var mockDrivers = new[]
            {
                new Driver
                {
                    DriverID = 1,
                    Forename = "John",
                    Surname = "Doe",
                    VehicleRegistration = "AB123CD",
                    Traces = new[]
                    {
                        new Trace { Date = new System.DateTime(2021, 02, 01), Activity = new[] { new Activity { Duration = 30 } } },
                        new Trace { Date = new System.DateTime(2021, 02, 02), Activity = new[] { new Activity { Duration = 45 } } }
                    }
                },
                new Driver
                {
                    DriverID = 2,
                    Forename = "Jane",
                    Surname = "Doe",
                    VehicleRegistration = "EF456GH",
                    Traces = new[]
                    {
                        new Trace { Date = new System.DateTime(2021, 02, 03), Activity = new[] { new Activity { Duration = 60 } } }
                    }
                }
            };

            _driverRepositoryMock.Setup(repo => repo.GetDrivers()).Returns(new Drivers { data = mockDrivers });

            var result = _driverService.GetDrivers();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Length);
            Assert.AreEqual("John Doe", result[0].Name);
            Assert.AreEqual(75, result[0].MinsWorked);
            Assert.AreEqual("Jane Doe", result[1].Name);
            Assert.AreEqual(60, result[1].MinsWorked);
        }

        [TestMethod]
        public void GetDrivers_With_NameFilter_Returns_Filtered_Drivers()
        {
            var mockDrivers = new[]
            {
                new Driver
                {
                    DriverID = 1,
                    Forename = "John",
                    Surname = "Doe",
                    VehicleRegistration = "AB123CD",
                    Traces = new[]
                    {
                        new Trace { Date = new System.DateTime(2021, 02, 01), Activity = new[] { new Activity { Duration = 30 } } },
                        new Trace { Date = new System.DateTime(2021, 02, 02), Activity = new[] { new Activity { Duration = 45 } } }
                    }
                },
                new Driver
                {
                    DriverID = 2,
                    Forename = "Jane",
                    Surname = "Doe",
                    VehicleRegistration = "EF456GH",
                    Traces = new[]
                    {
                        new Trace { Date = new System.DateTime(2021, 02, 03), Activity = new[] { new Activity { Duration = 60 } } }
                    }
                },
                new Driver 
                { 
                    DriverID = 3,
                    Forename = "Johnny",
                    Surname = "Appleseed",
                    VehicleRegistration = "EF456GH",
                    Traces = new[]
                    {
                        new Trace { Date = new System.DateTime(2021, 02, 03), Activity = new[] { new Activity { Duration = 60 } } }
                    }
                }
            };

            _driverRepositoryMock.Setup(repo => repo.GetDrivers()).Returns(new Drivers { data = mockDrivers });

            var result = _driverService.GetDrivers("Johnny");

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Length == 1);
            Assert.AreEqual("Johnny Appleseed", result[0].Name);
        }

        [TestMethod]
        public void GetDrivers_With_CaseInsensitive_NameFilter_Returns_Filtered_Drivers()
        {
            var mockDrivers = new[]
            {
                new Driver
                {
                    DriverID = 1,
                    Forename = "John",
                    Surname = "Doe",
                    VehicleRegistration = "AB123CD",
                    Traces = new[]
                    {
                        new Trace { Date = new System.DateTime(2021, 02, 01), Activity = new[] { new Activity { Duration = 30 } } },
                        new Trace { Date = new System.DateTime(2021, 02, 02), Activity = new[] { new Activity { Duration = 45 } } }
                    }
                },
                new Driver
                {
                    DriverID = 2,
                    Forename = "Jane",
                    Surname = "Doe",
                    VehicleRegistration = "EF456GH",
                    Traces = new[]
                    {
                        new Trace { Date = new System.DateTime(2021, 02, 03), Activity = new[] { new Activity { Duration = 60 } } }
                    }
                },
                new Driver
                {
                    DriverID = 3,
                    Forename = "Johnny",
                    Surname = "Appleseed",
                    VehicleRegistration = "EF456GH",
                    Traces = new[]
                    {
                        new Trace { Date = new System.DateTime(2021, 02, 03), Activity = new[] { new Activity { Duration = 60 } } }
                    }
                }
            };

            _driverRepositoryMock.Setup(repo => repo.GetDrivers()).Returns(new Drivers { data = mockDrivers });

            var result = _driverService.GetDrivers("johnny");

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Length == 1);
            Assert.AreEqual("Johnny Appleseed", result[0].Name);
        }
    }
}
