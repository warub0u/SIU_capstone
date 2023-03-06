using OneMapAPI.Models;

namespace OneMapAPI.Services
{
    public interface IOneMapService
    {
        Task<Coordinates> GetCoordinates(string postalCode);
        Task<List<LegInformation>> GetRoute(string startLang, string startLong, string endLang, string endLong, string mode);
    }
}
