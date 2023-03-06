using CS_FavouritesAPI.Models;

namespace CS_FavouritesAPI.Repository
{
    public interface IBusBookmarkRepository
    {
        List<BusBookmark> GetBusBookmarks(string username);
        List<BusBookmark> CheckBookmark(string username, string busCode, string busStopName);
        BusBookmark GetBookmark(int id);
        List<BusBookmark> CheckBookmark(string username);
        int AddBookmark(BusBookmark bookmark);
        int DeleteBookmark(int id);
        int UpdateBookmark(int id, BusBookmark bookmark);

    }
}
