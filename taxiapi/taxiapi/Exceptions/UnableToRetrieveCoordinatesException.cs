using System.Runtime.Serialization;

namespace taxiapi.Exceptions
{
    public class UnableToRetrieveCoordinatesException : Exception
    {
        public UnableToRetrieveCoordinatesException()
        {
        }

        public UnableToRetrieveCoordinatesException(string? message) : base(message)
        {
        }
    }
}
