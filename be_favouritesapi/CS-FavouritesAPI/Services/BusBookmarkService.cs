using CS_FavouritesAPI.Exceptions;
using CS_FavouritesAPI.Models;
using CS_FavouritesAPI.Repository;

namespace CS_FavouritesAPI.Services
{
    public class BusBookmarkService : IBusBookmarkService
    {
        private readonly IBusBookmarkRepository repo;

        public BusBookmarkService(IBusBookmarkRepository repo)
        {
            this.repo = repo;
        }

        public int AddBookmark(BusBookmark bookmark)
        {
            var b = repo.CheckBookmark(bookmark.UserName, bookmark.BusCode, bookmark.BusStopName);
            if (b.Count != 0)
            {
                throw new BusBookmarkAlreadyExists($"Bus stop: {bookmark.BusStopName} has been saved before!");
            }
            return repo.AddBookmark(bookmark);
        }

        public int DeleteBookmark(int id)
        {
            if(repo.GetBookmark(id) != null)
            {
                return repo.DeleteBookmark(id);
            }

            throw new BusBookmarkDoesNotExists("Opps, we can't find the mentioned bookmark!");
        }

        public List<BusBookmark> GetBusBookmarks(string username)
        {
            if(repo.CheckBookmark(username) != null)
            {
                return repo.GetBusBookmarks(username);
            }
            throw new BusBookmarkDoesNotExists($"You do not have any bookmarks!");
        }

        public int UpdateBookmark(int id, BusBookmark bookmark)
        {
            if(repo.GetBookmark(id) != null)
            {
                return repo.UpdateBookmark(id, bookmark);
            }

            throw new BusBookmarkDoesNotExists($"Opps, this bookmark doesn't exist, please create a new one");
        }
    }
}
