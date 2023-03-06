using CS_FavouritesAPI.Models;
using CS_FavouritesAPI.Repository;

namespace CS_FavouritesTest
{
    public class Tests
    {
        private readonly IFavouritesRepository repo;
        DatabaseFixture fixture;

        public Tests()
        {
            fixture = new DatabaseFixture();
            repo = new FavouritesRepository(fixture.db);
        }


        [OneTimeSetUp]
        public void SeedData()
        {
            repo.AddFavourites(new Favourites { Id = 1, UserName = "kh@gmail.com", Source = "Harbourfront MRT", Destination = "Labrador Park MRT", SourceLang = "1.26538938374901", SourceLong = "103.821530157095", DestinationLang = "1.2722270505362", DestinationLong = "103.802744505566", Mode = "TRANSIT", MapUrl = "onemap.com" });
            repo.AddFavourites(new Favourites { Id = 2, UserName = "zy@gmail.com", Source = "80 PASIR PANJANG ROAD MAPLETREE BUSINESS CITY", Destination = "ORTO", SourceLang = "1.27587544821221", SourceLong = "103.800651847647", DestinationLang = "1.40454653668991", DestinationLong = "103.902072622229", Mode = "TRANSIT", MapUrl = "onemap.com" });
            repo.AddFavourites(new Favourites { Id = 3, UserName = "abby@gmail.com", Source = "80 PASIR PANJANG ROAD MAPLETREE BUSINESS CITY", Destination = "PUNGGOL MRT", SourceLang = "1.27587544821221", SourceLong = "103.800651847647", DestinationLang = "1.40454653668991", DestinationLong = "103.902072622229", Mode = "TRANSIT", MapUrl = "onemap.com" });
        }
   

        [Test, Order(1)]
        public void GetFavouritesTest()
        {
            var favourites = repo.GetFavourites("kh@gmail.com");
            Assert.Greater(favourites.Count, 0);
        }

        [Test, Order(2)]
        public void GetFavouritesByIDTest()
        {
            var favourites = repo.GetFavourites(1);
            Assert.AreEqual("Harbourfront MRT", favourites.Source);
        }

        [Test, Order(3)]
        public void AddFavouritesTest()
        {
            int res = repo.AddFavourites(new Favourites { Id = 4, UserName = "abby@gmail.com", Source = "80 PASIR PANJANG ROAD MAPLETREE BUSINESS CITY", Destination = "PUNGGOL MRT", SourceLang = "1.27587544821221", SourceLong = "103.800651847647", DestinationLang = "1.40454653668991", DestinationLong = "103.902072622229", Mode = "TRANSIT", MapUrl = "onemap.com" });
            Assert.AreEqual(1, res);
        }

        [Test, Order(4)]
        public void RemoveFavouritesTest()
        {
            var favourites = repo.DeleteFavourites(1);
            Assert.AreEqual(1, favourites);
        }

        [Test,Order(5)]
        public void UpdatesFavouritesTest()
        {
            var originalFavourites = repo.GetFavourites(1);
            var updatedFavourites = new Favourites
            {
                UserName = "kh@gmail.com",
                Source = "Harbourfront MRT",
                Destination = "VivoCity",
                SourceLang = "1.26538938374901",
                SourceLong = "103.821530157095",
                DestinationLang = "1.26479154947531",
                DestinationLong = "103.82343425854",
                Mode = "WALK",
                MapUrl = "onemap.com"
            };
            var result = repo.UpdateFavourites(1, updatedFavourites);
            var favourites = repo.GetFavourites("kh@gmail.com").First();
            Assert.AreEqual(updatedFavourites.Source, favourites.Source);
        }

    }




}