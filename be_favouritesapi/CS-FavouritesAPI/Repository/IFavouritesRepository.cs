using CS_FavouritesAPI.Models;

namespace CS_FavouritesAPI.Repository
{
    public interface IFavouritesRepository
    {
        List<Favourites> GetFavourites(string username);
        List<Favourites> CheckFavourites(string source, string destination, string sourceLang, string sourceLong, string destinationLang, string destinationLong, string mode);
        int AddFavourites (Favourites favourites);
        int UpdateFavourites (int id, Favourites favourites);
        int DeleteFavourites (int id);
        Favourites GetFavourites(int id);
    }
}
