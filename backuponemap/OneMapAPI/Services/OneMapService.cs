using OneMapAPI.Models;
using OneMapAPI.Repository;
using OneMapAPI.Exceptions;
using static OneMapAPI.Models.RouteResponse;

namespace OneMapAPI.Services
{
    public class OneMapService : IOneMapService
    {
        private readonly IOneMapRepository repo;

        public OneMapService(IOneMapRepository repo)
        {
            this.repo = repo;
        }

        public async Task<Coordinates> GetCoordinates(string postalCode)
        {
            var coordinates = await repo.GetCoordinates(postalCode);
            if (coordinates== null)
            {
                throw new UnableToGetCoordinatesException($"Unable to retrieve coordinates for the postal code {postalCode}");
            }
            return coordinates;
        }

        public async Task<List<LegInformation>> GetRoute(string startLang, string startLong, string endLang, string endLong, string mode)
        {
            var route = await repo.GetRoute(startLang, startLong, endLang, endLong, mode);
            if (route == null)
            {
                throw new UnableToRetrieveRouteInfoException("Opps, Unable to retrieve the route information");
            }
            return route;
        }
    }
}
