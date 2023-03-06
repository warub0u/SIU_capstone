using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.VisualBasic;
using taxiapi.Exceptions;
using taxiapi.Models;
using taxiapi.Repository;

namespace taxiapi.Services
{
    public class FareService : IFareService
    {
        double totaldistance = 0;
        double totaltime = 0;
        DateTime nowtime = DateTime.Now;
        DayOfWeek day = DateTime.Today.DayOfWeek;

        private readonly IOneMapRepository repo;

        public FareService(IOneMapRepository repo)
        {
            this.repo = repo;
        }

        public async Task<OneMapCoordinates> GetCoordinates(string postalCode)
        {
            var coordinates = await repo.GetCoordinates(postalCode);
            if (coordinates == null)
            {
                throw new UnableToRetrieveCoordinatesException($"Unable to retrieve coordinates for the postal code {postalCode}");
            }
            return coordinates;
        }
        public async Task<FaresInformation> GetTotalTimeDistance(string startLang, string startLong, string endLang, string endLong)
        {
            var routeResponse = await repo.GetTotalTimeDistance(startLang, startLong, endLang, endLong);
            //this.time = routeResponse.totalTime;
            if (routeResponse == null)
            {
                throw new UnableToRetrieveTotalTimeandDistanceException("Unable to retrieve total time and distance.");
            }
            this.totaltime = routeResponse.totalTime;
            this.totaldistance = routeResponse.totalDistance;
            return routeResponse;
        }
        //string startLang, string startLong, string endLang, string endLong
        public async Task<double> GetFareForCDGApp(string startLang, string startLong, string endLang, string endLong)
        {
            var routeResponse = await repo.GetTotalTimeDistance(startLang, startLong, endLang, endLong);
            double basefare = 2.80;
            double perkm = 0.5;
            double permin = 0.15;
            double km = routeResponse.totalDistance / 1000;
            double time = routeResponse.totalTime / 60;
            Console.WriteLine($"{ routeResponse.totalDistance}, { routeResponse.totalTime}");
            double fare = (basefare + (perkm * km) + (permin * time));
            if(fare < 6)
            {
                return 6;
            }
            return fare;
            
        }

        public async Task<double> GetFareForMetered(string startLang, string startLong, string endLang, string endLong)
        {
            var routeResponse = await repo.GetTotalTimeDistance(startLang, startLong, endLang, endLong);
            double km = routeResponse.totalDistance / 1000;
            double time = routeResponse.totalTime / 60;
            double waitingtimecharge = 0.33 * (time/3);
            double basefare = 3.90;
            double firsttenkm = 0.625;
            double nextfewkm = 0.714;
            Console.WriteLine($"{routeResponse.totalDistance}, {routeResponse.totalTime},{waitingtimecharge}");

            if (day != DayOfWeek.Saturday && day != DayOfWeek.Sunday)
            {
                if ((nowtime.Hour == 6 && nowtime.Minute >= 0) ||
                    (nowtime.Hour >= 7 && nowtime.Hour < 9) ||
                    (nowtime.Hour == 9 && nowtime.Minute <= 29) ||
                    (nowtime.Hour >= 18 && nowtime.Hour < 24))
                {
                    Console.WriteLine("Surcharge of 25%.");
                    double surcharge = 1.25;
                    if (km <= 1)
                    {
                        return 3.90;
                    }
                    else if (km <= 10)
                    {
                        double fare = (waitingtimecharge + basefare + ((km - 1) * firsttenkm)) * surcharge;
                        return fare;
                    }
                    double maxfare = ((waitingtimecharge + basefare + 6.25) + ((km - 11) * nextfewkm)) * surcharge;
                    return maxfare;
                }
                else if (nowtime.Hour < 6)
                {
                    Console.WriteLine("Surcharge of 50%.");
                    double surcharge = 1.50;
                    if (km <= 1)
                    {
                        return 3.90;
                    }
                    else if (km <= 10)
                    {
                        double fare = (waitingtimecharge + basefare + ((km - 1) * firsttenkm)) * surcharge;
                        return fare;
                    }
                    double maxfare = ((waitingtimecharge + basefare + 6.25) + ((km - 11) * nextfewkm)) * surcharge;
                    return maxfare;
                }
            }
            if (nowtime.Hour < 6)
            {
                Console.WriteLine("Surcharge of 50%.");
                double surcharge = 1.50;
                if (km <= 1)
                {
                    return 3.90;
                }
                else if (km <= 10)
                {
                    double fare = (waitingtimecharge + basefare + ((km - 1) * firsttenkm)) * surcharge;
                    return fare;
                }
                double maxfare = ((waitingtimecharge + basefare + 6.25) + ((km - 11) * nextfewkm)) * surcharge;
                return maxfare;
            }
            else
            {
                Console.WriteLine("no Surcharge!");
                if (km <= 1)
                {
                    return 3.90;
                }
                else if (km <= 10)
                {
                    double fare = (waitingtimecharge + basefare + ((km - 1) * firsttenkm));
                    return fare;
                }
                double maxfare = ((waitingtimecharge + basefare + 6.25) + ((km - 11) * nextfewkm));
                return maxfare;
            }
           
        }

    }
}
