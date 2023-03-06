namespace LoginAPI.Services
{
    public interface ITokenGeneratorService
    {
        // METHODS TO BE IMPLEMENTED FOR THE INTERFACE
        string GenerateToken(string userName, string password);
    }
}
