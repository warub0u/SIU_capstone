using Microsoft.EntityFrameworkCore;

namespace CS_FavouritesAPI.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Favourites> Favourites { get; set; }
        public DbSet<BusBookmark> BusBookmark { get; set; }
    }
}
