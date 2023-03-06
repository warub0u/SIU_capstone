using CS_FavouritesAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_FavouritesTest
{
    internal class DatabaseFixture : IDisposable
    {
        public DataContext db;
        public DatabaseFixture()
        {
            var options = new DbContextOptionsBuilder<DataContext>().EnableSensitiveDataLogging(true).UseInMemoryDatabase("FavouritesDB").Options;
            db = new DataContext(options);
            db.ChangeTracker.Clear();
        }
        public void Dispose()
        {
            db = null;
        }
    }
}
