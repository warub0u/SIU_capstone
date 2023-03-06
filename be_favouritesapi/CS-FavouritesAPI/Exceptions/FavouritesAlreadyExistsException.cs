using System.Runtime.Serialization;

namespace CS_FavouritesAPI.Exceptions
{
    public class FavouritesAlreadyExistsException : Exception
    {
        public FavouritesAlreadyExistsException() { }

        public FavouritesAlreadyExistsException(string? message) : base(message) { }
    }
}
