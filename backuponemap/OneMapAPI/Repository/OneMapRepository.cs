using Newtonsoft.Json;
using OneMapAPI.Models;

namespace OneMapAPI.Repository
{
    public class OneMapRepository : IOneMapRepository
    {
        private readonly HttpClient _client;
        public OneMapRepository(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri("https://developers.onemap.sg");
        }
        public async Task<Coordinates> GetCoordinates(string postalCode)
        {
            {
                var response = await _client.GetAsync($"/commonapi/search?searchVal={postalCode}&returnGeom=Y&getAddrDetails=Y&pageNum=1");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<Coordinates>(json);

                    var location = result.results[0].ADDRESS;
                    var latitude = result.results[0].LATITUDE;
                    var longitude = result.results[0].LONGTITUDE;

                    Console.WriteLine($"{location}, {latitude}, {longitude}");

                    return result;
                }
                return null;
            }
        }

        public async Task<List<LegInformation>> GetRoute(string startLang, string startLong, string endLang, string endLong, string mode)
        {

            string date = DateTime.Now.ToString("yyyy-MM-dd");
            string time = DateTime.Now.ToString("HH:mm:ss");
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://developers.onemap.sg");

                HttpResponseMessage response = await client.GetAsync($"/privateapi/routingsvc/route?start={startLang},{startLong}&end={endLang},{endLong}&routeType=PT&token=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOjk4NzMsInVzZXJfaWQiOjk4NzMsImVtYWlsIjoia2FpaGVuZy50ZW9AdGVtdXMuY29tIiwiZm9yZXZlciI6ZmFsc2UsImlzcyI6Imh0dHA6XC9cL29tMi5kZmUub25lbWFwLnNnXC9hcGlcL3YyXC91c2VyXC9zZXNzaW9uIiwiaWF0IjoxNjc3OTkxNjU3LCJleHAiOjE2Nzg0MjM2NTcsIm5iZiI6MTY3Nzk5MTY1NywianRpIjoiOWEyYWI0ODE3NGRhMmVkZTE0NzFiN2RhZGMwZWMzNzcifQ.4Jq_4l0v81dtK2z0nx7Hsm9xq3On8uwYELWQcSGH3qM&date={date}&time={time}&mode={mode}&maxWalkDistance=&numItineraries=");

                if (response.IsSuccessStatusCode)
                {
                    string jsonString = await response.Content.ReadAsStringAsync();

                    var responseData = JsonConvert.DeserializeObject<RouteResponse>(jsonString);
                    var legInformationList = new List<LegInformation>();
                    //var source = responseData.plan.from.name;
                    //var destination = responseData.plan.to.name;
                    var itinerary = responseData.plan.itineraries[0];

                        foreach (var leg in itinerary.legs)
                        {
                            var legInformation = new LegInformation
                            {
                                Duration = itinerary.duration,
                                Fare = itinerary.fare,
                                IntermediateStops = new List<string>()
                            };

                            //convert time to singapore time = +28800000
                            legInformation.StartTime = DateTimeOffset.FromUnixTimeMilliseconds(leg.from.arrival + 28800000).ToString("hh:mm:ss tt");
                            legInformation.EndTime = DateTimeOffset.FromUnixTimeMilliseconds(leg.to.departure + 28800000).ToString("hh:mm:ss tt");
                            legInformation.MethodOfTransport = leg.mode.ToString();
                            string m = leg.from.name.ToString();
                            if (leg.mode == "BUS")
                            {
                                legInformation.RouteType = leg.routeId;
                            }
                            legInformation.LegSource = leg.from.name.ToString();
                            legInformation.LegDestination = leg.to.name.ToString();
                            legInformation.LegDuration = leg.duration.ToString();
                            legInformation.LegGeo = leg.legGeometry.points.ToString();
                            if (leg.intermediateStops.Count() != 0)
                            {
                                foreach (var stop in leg.intermediateStops)
                                {
                                    legInformation.IntermediateStops.Add(stop.name);
                                }
                            }
                            legInformationList.Add(legInformation);
                        
                    }
                    return legInformationList;
                }
                else
                {
                    return null;
                }
            }
        }


    }
}
