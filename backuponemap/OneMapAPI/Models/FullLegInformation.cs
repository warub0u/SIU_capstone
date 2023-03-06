namespace OneMapAPI.Models
{
    public class FullLegInformation
    {

        public class Rootobject
        {
            public int duration { get; set; }
            public long startTime { get; set; }
            public long endTime { get; set; }
            public int walkTime { get; set; }
            public int transitTime { get; set; }
            public int waitingTime { get; set; }
            public float walkDistance { get; set; }
            public bool walkLimitExceeded { get; set; }
            public int elevationLost { get; set; }
            public int elevationGained { get; set; }
            public int transfers { get; set; }
            public Leg[] legs { get; set; }
            public bool tooSloped { get; set; }
            public string fare { get; set; }
        }

        public class Leg
        {
            public long startTime { get; set; }
            public long endTime { get; set; }
            public int departureDelay { get; set; }
            public int arrivalDelay { get; set; }
            public bool realTime { get; set; }
            public float distance { get; set; }
            public bool pathway { get; set; }
            public string mode { get; set; }
            public string route { get; set; }
            public int agencyTimeZoneOffset { get; set; }
            public bool interlineWithPreviousLeg { get; set; }
            public From from { get; set; }
            public To to { get; set; }
            public Leggeometry legGeometry { get; set; }
            public bool rentedBike { get; set; }
            public bool transitLeg { get; set; }
            public int duration { get; set; }
            public Intermediatestop[] intermediateStops { get; set; }
            public Step[] steps { get; set; }
            public int numIntermediateStops { get; set; }
            public string agencyName { get; set; }
            public string agencyUrl { get; set; }
            public int routeType { get; set; }
            public string routeId { get; set; }
            public string agencyId { get; set; }
            public string tripId { get; set; }
            public string serviceDate { get; set; }
            public string routeShortName { get; set; }
            public string routeLongName { get; set; }
        }

        public class From
        {
            public string name { get; set; }
            public float lon { get; set; }
            public float lat { get; set; }
            public long departure { get; set; }
            public string orig { get; set; }
            public string vertexType { get; set; }
            public string stopId { get; set; }
            public string stopCode { get; set; }
            public long arrival { get; set; }
            public int stopIndex { get; set; }
            public int stopSequence { get; set; }
        }

        public class To
        {
            public string name { get; set; }
            public string stopId { get; set; }
            public string stopCode { get; set; }
            public float lon { get; set; }
            public float lat { get; set; }
            public long arrival { get; set; }
            public long departure { get; set; }
            public int stopIndex { get; set; }
            public int stopSequence { get; set; }
            public string vertexType { get; set; }
            public string orig { get; set; }
        }

        public class Leggeometry
        {
            public string points { get; set; }
            public int length { get; set; }
        }

        public class Intermediatestop
        {
            public string name { get; set; }
            public string stopId { get; set; }
            public string stopCode { get; set; }
            public float lon { get; set; }
            public float lat { get; set; }
            public long arrival { get; set; }
            public long departure { get; set; }
            public int stopIndex { get; set; }
            public int stopSequence { get; set; }
            public string vertexType { get; set; }
        }

        public class Step
        {
            public float distance { get; set; }
            public string relativeDirection { get; set; }
            public string streetName { get; set; }
            public string absoluteDirection { get; set; }
            public bool stayOn { get; set; }
            public bool area { get; set; }
            public bool bogusName { get; set; }
            public float lon { get; set; }
            public float lat { get; set; }
            public object[] elevation { get; set; }
        }

    }
}
