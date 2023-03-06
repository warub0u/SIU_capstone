using System.Data.Common;
using MongoDB.Driver;


namespace LoginAPI.Models
{
    public class DataContext
    {
        // DECLARE VARIABLES FOR MONGOCLIENT AND MONGODB
        //MongoClient client { get; set; }
        //IMongoDatabase db { get; set; }        

        //public MongoClient client;
        //public IMongoDatabase db;

        MongoClient client; //private
        IMongoDatabase db;  //private

        // DEFINE DATACONTEXT CONSTRUCTOR TO CREATE MONGOCLIENT AND DB
        public DataContext()
        {
            var environment = Environment.GetEnvironmentVariable("MONGODB_CONNECTIONSTRING");
            if (environment != null)
            {
                client = new MongoClient(environment);
                //client = new MongoClient("mongodb://mongo_auth:27017"); // to try with hardcoding the environment client string for troublshooting
                db = client.GetDatabase("Auth");
            }
            else
            {
                client = new MongoClient("mongodb://localhost:27017");
                //client = new MongoClient("mongodb://0.0.0.0:27017");
                db = client.GetDatabase("Auth");
            }
        }

        //public MongoClient client2; 
        //public IMongoDatabase db2;  

        // DEFINE DATACONTEXT CONSTRUCTOR TO CREATE MONGOCLIENT AND DB (FOR TESTING)
        public DataContext(string connectionstring, string database)
        {            
            client = new MongoClient("mongodb://localhost:27017");
            //client = new MongoClient("mongodb://0.0.0.0:27017"); 
            db = client.GetDatabase("testdb");           

        }

        // GET COLLECTION FROM THE MONGODB "Users" COLLECTION
        public IMongoCollection<User> Users => db.GetCollection<User>("Users");

        // GET COLLECTION FROM THE MONGODB "UserImages" COLLECTION
        public IMongoCollection<UserImage> UserImages => db.GetCollection<UserImage>("UserImages");


    }
}
