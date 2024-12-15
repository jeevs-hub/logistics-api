namespace logistics_api.Models
{
    public class DriverDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string VehicleRegistration { get; set; }
        public IEnumerable<int> daysWorked { get; set; }
        public int MinsWorked { get; set; }
    }
}
