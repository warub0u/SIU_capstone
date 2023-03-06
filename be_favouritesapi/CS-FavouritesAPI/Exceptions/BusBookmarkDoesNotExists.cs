namespace CS_FavouritesAPI.Exceptions
{
    public class BusBookmarkDoesNotExists : Exception
    {
        public BusBookmarkDoesNotExists() { }
        public BusBookmarkDoesNotExists(string message) : base(message) { }
    }
}
