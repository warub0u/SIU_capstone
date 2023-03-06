namespace CS_FavouritesAPI.Exceptions
{
    public class BusBookmarkAlreadyExists : Exception
    {
        public BusBookmarkAlreadyExists() { }
        public BusBookmarkAlreadyExists(string message) : base(message) { }
    }
}
