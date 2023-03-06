namespace OneMapAPI.Models
{
    //DEFINED ALL POSSIBLE OUTCOME IN JSON FILE INCASE WE REQUIRE SOMETHING ELSE IN THE FUTURE
    public class RouteResponse
    {

            public Requestparameters requestParameters { get; set; }
            public Plan plan { get; set; }
            public Debugoutput debugOutput { get; set; }
            public Elevationmetadata elevationMetadata { get; set; }


        public class Requestparameters
        {
            public string date { get; set; }
            public string preferredRoutes { get; set; }
            public string walkReluctance { get; set; }
            public string fromPlace { get; set; }
            public string transferPenalty { get; set; }
            public string maxWalkDistance { get; set; }
            public string maxTransfers { get; set; }
            public string otherThanPreferredRoutesPenalty { get; set; }
            public string numItineraries { get; set; }
            public string waitAtBeginningFactor { get; set; }
            public string mode { get; set; }
            public string arriveBy { get; set; }
            public string showIntermediateStops { get; set; }
            public string toPlace { get; set; }
            public string time { get; set; }
        }

        public class Plan
        {
            public long date { get; set; }
            public From from { get; set; }
            public To to { get; set; }
            public Itinerary[] itineraries { get; set; }
        }

        public class From
        {
            public string name { get; set; }
            public float lon { get; set; }
            public float lat { get; set; }
            public string orig { get; set; }
            public string vertexType { get; set; }
        }

        public class To
        {
            public string name { get; set; }
            public float lon { get; set; }
            public float lat { get; set; }
            public string orig { get; set; }
            public string vertexType { get; set; }
        }

        public class Itinerary
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
            public From1 from { get; set; }
            public To1 to { get; set; }
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

        public class From1
        {
            public string name { get; set; }
            public float lon { get; set; }
            public float lat { get; set; }
            public long departure { get; set; }
            public string orig { get; set; }
            public string vertexType { get; set; }
            public long arrival { get; set; }
            public string stopId { get; set; }
            public string stopCode { get; set; }
            public int stopIndex { get; set; }
            public int stopSequence { get; set; }
        }

        public class To1
        {
            public string name { get; set; }
            public float lon { get; set; }
            public float lat { get; set; }
            public long arrival { get; set; }
            public long departure { get; set; }
            public string vertexType { get; set; }
            public string stopId { get; set; }
            public string stopCode { get; set; }
            public int stopIndex { get; set; }
            public int stopSequence { get; set; }
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

        public class Debugoutput
        {
            public int precalculationTime { get; set; }
            public int pathCalculationTime { get; set; }
            public int[] pathTimes { get; set; }
            public int renderingTime { get; set; }
            public int totalTime { get; set; }
            public bool timedOut { get; set; }
        }

        public class Elevationmetadata
        {
            public float ellipsoidToGeoidDifference { get; set; }
            public bool geoidElevation { get; set; }
        }

    }
}
