namespace logistics_api.Models
{
    public class Activity
    {
        public string StartTime {  get; set; }
        public string Type {  get; set; }
        public int Duration {  get; set; }
    }

    public class Trace
    {
        public DateTime Date { get; set; }
        public Activity[] Activity { get; set; }
    }

    public class Driver
    {
        public long DriverID { get; set; }
        public string Surname { get; set; }
        public string Forename { get; set; }
        public string VehicleRegistration { get; set; }
        public Trace[] Traces { get; set; }
    }

    public class Drivers
    {
        public Driver[] data { get; set; }
    }
}
