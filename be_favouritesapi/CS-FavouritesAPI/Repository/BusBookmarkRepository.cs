using CS_FavouritesAPI.Models;

namespace CS_FavouritesAPI.Repository
{
    public class BusBookmarkRepository : IBusBookmarkRepository
    {
        private readonly DataContext db;

        public BusBookmarkRepository(DataContext db)
        {
            this.db = db;
        }

        public int AddBookmark(BusBookmark bookmark)
        {
            db.BusBookmark.Add(bookmark);
            return db.SaveChanges();
        }

        public List<BusBookmark> CheckBookmark(string username, string busCode, string busStopName)
        {

            return db.BusBookmark.Where(b => b.UserName == username && (b.BusCode == busCode || b.BusStopName == busStopName)).ToList();
          
            
        }

        public List<BusBookmark> CheckBookmark(string username)
        {
            return db.BusBookmark.Where(b => b.UserName == username).ToList();
        }

        public int DeleteBookmark(int id)
        {
            var b = db.BusBookmark.Find(id);
            db.BusBookmark.Remove(b);
            return db.SaveChanges();
        }

        public BusBookmark GetBookmark(int id)
        {
            return db.BusBookmark.Find(id);
        }

        public List<BusBookmark> GetBusBookmarks(string username)
        {
            return db.BusBookmark.Where(b => b.UserName == username).ToList();
        }

        public int UpdateBookmark(int id, BusBookmark bookmark)
        {
            var b = db.BusBookmark.Find(id);
            b.BusCode = bookmark.BusCode;
            b.BusStopName = bookmark.BusStopName;
            db.Entry(b).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return db.SaveChanges();
        }
    }
}
