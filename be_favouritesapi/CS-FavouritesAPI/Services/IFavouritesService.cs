using CS_FavouritesAPI.Models;

namespace CS_FavouritesAPI.Services
{
    public interface IFavouritesService
    {
        List<Favourites> GetFavourites(string username);
        int AddFavourites(Favourites favourites);
        int UpdateFavourites(int id, Favourites favourites);
        int DeleteFavourites(int id);
    }
}
