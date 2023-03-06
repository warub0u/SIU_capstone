using Newtonsoft.Json;

namespace taxiapi.Models
{
    public class DriveResponse
    {
        public string status_message { get; set; }
        public string route_geometry { get; set; }
        public string status { get; set; }

        //public RouteInstructions[][] route_instructions { get; set; }
        //public RouteInstructions[] route_instructions { get; set; }
        public List<string>[] route_instructions { get; set; }
        public string[] route_name { get; set; }
        public RouteSummary route_summary { get; set; }
        public AlternativeRoute[] alternativeroute { get; set; }
        public string viaRoute { get; set; }
        public string subtitle { get; set; }
        public PhyRoute phyroute { get; set; }

       // public class RouteInstructions
       // {
            //public object[] Head { get; set; }
            //public string Name { get; set; }
            //public int Distance { get; set; }
            //public string Location { get; set; }
            //public int Orientation { get; set; }
            //public string Length { get; set; }
            //public string Direction { get; set; }
            //public string Azimuth { get; set; }
            //public string Mode { get; set; }
            //public string Instruction { get; set; }
      //  }


        //public class RouteName
        //{
        //    public string[] Names { get; set; }
        //}

        public class RouteSummary
        {
            public string start_point { get; set; }
            public string end_point { get; set; }
            public int total_time { get; set; }
            public int total_distance { get; set; }
        }

        public class AlternativeRoute
        {
            public string status_message { get; set; }
            public string route_geometry { get; set; }
            public string status { get; set; }
            //public RouteInstructions1[] route_instructions { get; set; }
            public List<string>[] route_instructions { get; set; }
            //public List<RouteInstructions1> route_instructions { get; set; }
            public string[] route_name { get; set; }
            public RouteSummary1 route_summary { get; set; }
            public string viaRoute { get; set; }
            public string subtitle { get; set; }
        }
        //public class RouteInstructions1
        //{
        //    public string Action { get; set; }
        //    public string Name { get; set; }
        //    public int Distance { get; set; }
        //    public string Location { get; set; }
        //    public int Orientation { get; set; }
        //    public string Length { get; set; }
        //    public string Direction { get; set; }
        //    public string Azimuth { get; set; }
        //    public string Mode { get; set; }
        //    public string Instruction { get; set; }
        //}

        //public class RootObject1
        //{
        //    public List<RouteInstructions1> route_instructions { get; set; }
        //}

        //public class RouteName1
        //{
        //    public string[] Names { get; set; }
        //}

        public class RouteSummary1
        {
            public string start_point { get; set; }
            public string end_point { get; set; }
            public int total_time { get; set; }
            public int total_distance { get; set; }
        }

        public class PhyRoute
        {
            public string status_message { get; set; }
            public string route_geometry { get; set; }
            public int status { get; set; }
            // public RouteInstructions2[] route_instructions { get; set; }
            public List<string>[] route_instructions { get; set; }
            //public List<RouteInstructions2> route_instructions { get; set; }
            public string[] route_name { get; set; }
            public RouteSummary2 route_summary { get; set; }
            public string viaRoute { get; set; }
            public string subtitle { get; set; }
        }

        //public class RouteInstructions2
        //{
        //    public string Action { get; set; }
        //    public string Name { get; set; }
        //    public int Distance { get; set; }
        //    public string Location { get; set; }
        //    public int Orientation { get; set; }
        //    public string Length { get; set; }
        //    public string Direction { get; set; }
        //    public string Azimuth { get; set; }
        //    public string Mode { get; set; }
        //    public string Instruction { get; set; }
        //}

        //public class RouteName2
        //{
        //    public string[] Names { get; set; }
        //}

        public class RouteSummary2
        {
            public string start_point { get; set; }
            public string end_point { get; set; }
            public int total_time { get; set; }
            public int total_distance { get; set; }
        }

    }
}
