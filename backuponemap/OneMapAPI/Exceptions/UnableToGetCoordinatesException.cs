namespace OneMapAPI.Exceptions
{
    public class UnableToGetCoordinatesException : Exception
    {
        public UnableToGetCoordinatesException() { }
        public UnableToGetCoordinatesException(string message) : base(message) { }
    }
}
