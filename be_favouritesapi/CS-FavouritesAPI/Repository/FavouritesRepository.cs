using CS_FavouritesAPI.Models;

namespace CS_FavouritesAPI.Repository
{
    public class FavouritesRepository : IFavouritesRepository
    {
        private readonly DataContext db;

        public FavouritesRepository(DataContext db)
        {
            this.db = db;
        }

        public int AddFavourites(Favourites favourites)
        {
            db.Favourites.Add(favourites);
            return db.SaveChanges();
        }

        public List<Favourites> CheckFavourites(string source, string destination, string sourceLang, string sourceLong, string destinationLang, string destinationLong, string mode)
        {
            return db.Favourites.Where(x => x.Source == source && x.Destination == destination && x.SourceLang == sourceLang && x.SourceLong == sourceLong && x.DestinationLang == destinationLang && x.DestinationLong == destinationLong && x.Mode == mode).ToList();
        }

        public int DeleteFavourites(int id)
        {
            var f = db.Favourites.Find(id);
            db.Favourites.Remove(f);
            return db.SaveChanges();
        }

        public List<Favourites> GetFavourites(string username)
        {
            return db.Favourites.Where(x => x.UserName == username).ToList();
        }

        public Favourites GetFavourites(int id)
        {
            return db.Favourites.Find(id);
        }

        public int UpdateFavourites(int id, Favourites favourites)
        {
            var f = db.Favourites.Find(id);
            f.Source = favourites.Source; 
            f.Destination = favourites.Destination;
            f.SourceLang = favourites.SourceLang;
            f.SourceLong = favourites.SourceLong;
            f.DestinationLang = favourites.DestinationLang;
            f.DestinationLong = favourites.DestinationLong;
            f.Mode = favourites.Mode;
            f.MapUrl = favourites.MapUrl;
            db.Entry(f).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return db.SaveChanges();
        }

    }
}
