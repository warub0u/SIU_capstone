namespace OneMapAPI.Models
{
    public class LegInformation
    {
        public int Duration { get; set; }
        public string Fare { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string MethodOfTransport { get; set; }
        public string RouteType { get; set; }
        public string LegSource { get; set; }
        public string LegDestination { get; set; }
        public string LegDuration { get; set; }
        public string LegGeo { get; set; }
        public List<string> IntermediateStops { get; set; }
    }

    public class LegGeometry
    {
        public string points { get; set; }
        public string length { get; set; }
    }
}
