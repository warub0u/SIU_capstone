namespace taxiapi.Models
{
    public class FaresInformation
    {
        public double totalTime { get; set; }
        public double totalDistance { get; set; }

        public string routeGeo { get; set; }
        public string startPoint { get; set; }

    }
}
