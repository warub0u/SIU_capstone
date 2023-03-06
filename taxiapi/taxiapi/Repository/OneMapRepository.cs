using Newtonsoft.Json;
using taxiapi.Models;

namespace taxiapi.Repository
{
    public class OneMapRepository : IOneMapRepository
    {
        private readonly HttpClient _client;
        public OneMapRepository(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri("https://developers.onemap.sg");
        }

        public async Task<OneMapCoordinates> GetCoordinates(string postalCode)
        {
            {
                var response = await _client.GetAsync($"/commonapi/search?searchVal={postalCode}&returnGeom=Y&getAddrDetails=Y&pageNum=1");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<OneMapCoordinates>(json);

                    var location = result.results[0].ADDRESS;
                    var latitude = result.results[0].LATITUDE;
                    var longitude = result.results[0].LONGTITUDE;

                    Console.WriteLine($"{location}, {latitude}, {longitude}");

                    return result;
                }
                return null;
            }
        }


        public async Task<FaresInformation> GetTotalTimeDistance(string startLang, string startLong, string endLang, string endLong)
        {

            string date = DateTime.Now.ToString("yyyy-MM-dd");
            string time = DateTime.Now.ToString("HH:mm:ss");

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://developers.onemap.sg");

                HttpResponseMessage response = await client.GetAsync($"/privateapi/routingsvc/route?start={startLang},{startLong}&end={endLang},{endLong}&routeType=drive&token=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOjk4NzMsInVzZXJfaWQiOjk4NzMsImVtYWlsIjoia2FpaGVuZy50ZW9AdGVtdXMuY29tIiwiZm9yZXZlciI6ZmFsc2UsImlzcyI6Imh0dHA6XC9cL29tMi5kZmUub25lbWFwLnNnXC9hcGlcL3YyXC91c2VyXC9zZXNzaW9uIiwiaWF0IjoxNjc3OTkxNjU3LCJleHAiOjE2Nzg0MjM2NTcsIm5iZiI6MTY3Nzk5MTY1NywianRpIjoiOWEyYWI0ODE3NGRhMmVkZTE0NzFiN2RhZGMwZWMzNzcifQ.4Jq_4l0v81dtK2z0nx7Hsm9xq3On8uwYELWQcSGH3qM&date={date}&time={time}&mode=&maxWalkDistance=&numItineraries=");

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();

                    var responseData = JsonConvert.DeserializeObject<DriveResponse>(jsonString);
                    string startPoint = responseData.route_summary.start_point;
                    double totalTime = responseData.route_summary.total_time;
                    double totalDistance = responseData.route_summary.total_distance;
                    string routeGeo = responseData.route_geometry;
                    
                    // Create a new instance of the FaresInformation class
                    FaresInformation faresInformation = new FaresInformation();
                    // Set the values of the TotalDistance and TotalTime properties
                    faresInformation.routeGeo = routeGeo;
                    faresInformation.totalDistance = totalDistance;
                    faresInformation.totalTime = totalTime;
                    faresInformation.startPoint = startPoint;
                    
                    return faresInformation;
                }
                else
                {
                    return null;
                }
            }
        }



    }
}
