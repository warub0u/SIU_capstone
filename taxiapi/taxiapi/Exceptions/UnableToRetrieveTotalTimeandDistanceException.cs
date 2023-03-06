using System.Runtime.Serialization;

namespace taxiapi.Exceptions
{
    public class UnableToRetrieveTotalTimeandDistanceException : Exception
    {
        public UnableToRetrieveTotalTimeandDistanceException()
        {
        }

        public UnableToRetrieveTotalTimeandDistanceException(string? message) : base(message)
        {
        }
    }
}
