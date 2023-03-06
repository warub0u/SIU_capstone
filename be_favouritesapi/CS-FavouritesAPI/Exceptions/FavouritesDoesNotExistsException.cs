using System.Runtime.Serialization;

namespace CS_FavouritesAPI.Exceptions
{
    public class FavouritesDoesNotExistsException : Exception
    {
        public FavouritesDoesNotExistsException()
        {
        }

        public FavouritesDoesNotExistsException(string? message) : base(message)
        {
        }

       
    }
}
