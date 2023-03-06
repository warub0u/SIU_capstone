using taxiapi.Models;

namespace taxiapi.Services
{
    public interface IFareService
    //this service does the calculation for the fares
    {
        Task<OneMapCoordinates> GetCoordinates(string postalCode);
        Task<FaresInformation> GetTotalTimeDistance(string startLang, string startLong, string endLang, string endLong);
        Task<double> GetFareForCDGApp(string startLang, string startLong, string endLang, string endLong);

        //double GetFareForCDGApp();
        Task<double> GetFareForMetered(string startLang, string startLong, string endLang, string endLong);

    }
}
