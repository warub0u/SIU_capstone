using Microsoft.AspNetCore.Mvc;
using OneMapAPI.Models;

namespace OneMapAPI.Repository
{
    public interface IOneMapRepository
    {
        Task<Coordinates> GetCoordinates(string postalCode);
        Task<List<LegInformation>> GetRoute(string startLang, string startLong, string endLang, string endLong, string mode);
    }
}
