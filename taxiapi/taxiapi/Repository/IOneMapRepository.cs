using taxiapi.Models;

namespace taxiapi.Repository
{
    public interface IOneMapRepository
    {
        Task<OneMapCoordinates> GetCoordinates(string postalCode);
        Task<FaresInformation> GetTotalTimeDistance(string startLang, string startLong, string endLang, string endLong);

    }
}
