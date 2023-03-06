using CS_FavouritesAPI.Models;

namespace CS_FavouritesAPI.Services
{
    public interface IBusBookmarkService
    {
        List<BusBookmark> GetBusBookmarks(string username);
        int AddBookmark(BusBookmark bookmark);
        int DeleteBookmark(int id);
        int UpdateBookmark(int id, BusBookmark bookmark);
    }
}
