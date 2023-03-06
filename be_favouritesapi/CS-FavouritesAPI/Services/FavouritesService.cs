using CS_FavouritesAPI.Exceptions;
using CS_FavouritesAPI.Models;
using CS_FavouritesAPI.Repository;

namespace CS_FavouritesAPI.Services
{
    public class FavouritesService : IFavouritesService
    {
        private readonly IFavouritesRepository repo;

        public FavouritesService(IFavouritesRepository repo)
        {
            this.repo = repo;
        }

        public int AddFavourites(Favourites favourites)
        {
            var f = repo.CheckFavourites(favourites.Source, favourites.Destination, favourites.SourceLang, favourites.SourceLong, favourites.DestinationLang, favourites.DestinationLong, favourites.Mode);
            if (f.Count != 0)
            {
                throw new FavouritesAlreadyExistsException($"You have already added this source : {favourites.Source} to destination {favourites.Destination} before!");
            }
            return repo.AddFavourites(favourites);
        }

        public int DeleteFavourites(int id)
        {
            if (repo.GetFavourites(id) != null)
            {
                return repo.DeleteFavourites(id);
            }

            throw new FavouritesDoesNotExistsException("Opps, we can't delete something that doesn't exists :(");
        }

        public List<Favourites> GetFavourites(string username)
        {
            if (repo.GetFavourites(username) != null)
            {
                return repo.GetFavourites(username);
            }

            throw new FavouritesDoesNotExistsException($"Opps, you seem to have no favourites.. add some!");
        }

        public int UpdateFavourites(int id, Favourites favourites)
        {
            if (repo.GetFavourites(id) != null)
            {
                return repo.UpdateFavourites(id, favourites);
            }
            throw new FavouritesDoesNotExistsException($"Hmm, we can't find the route you wish to update...");
        }
    }
}
