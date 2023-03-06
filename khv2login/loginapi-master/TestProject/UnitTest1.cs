using LoginAPI.Models;
using LoginAPI.Repo;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Reflection;

namespace TestProject
{
    [TestFixture]
    public class Tests
    {        
        private UserRepo repo;
        private IGenerator gen;
        private HttpClient httpclient;
        private DataContext datacontext;
        private IMongoCollection<User> collection;
        

        public Tests()
        {
            // create new instance of MongoClient via connection string and database "testdb"
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("testdb");

            // Drop the test database and Collection if it already exists            
            database.DropCollection("Test_users");
            client.DropDatabase("testdb");

            // create an instance of DataContext via constructor method DataContext(string connection, string database)
            // with connection string "mongodb://localhost:27017" and db "testdb"
            // this DataContext constructor is defined in DataContext.cs with its private variables
            // datacontext is used to interact with db via connection string
            datacontext = new DataContext("mongodb://localhost:27017", "testdb");

            // define collection variable as "Test_users" collection in the MongoDB
            collection = database.GetCollection<User>("Test_users"); 

            // add user to create the "Test_users" collection in MongoDB
            collection.InsertMany(new[]
            {
                new User { UserName = "MJ", Email = "mj@google.com", Password="airjordan", Role = "MJ", IsBlocked = false,
                            IsPasswordReset = false, FirstName = "michael", LastName = "jordan", MobileNo = "65305575", DOB="2000-1-12", Gender="Male", PostalCode="500000"}
            });

            // other parameters to be instantantiated to feed into UserRepo() for instantiating repo only
            httpclient = new HttpClient();
            gen = new Generator();

            // instantiate repo
            repo = new UserRepo(datacontext, null);
        }


        [SetUp]
        public void Setup()
        { }

        [TearDown]
        public void TearDown()
        {
            //database.DropCollection("Test_users");
        }

        [Test, Order(1)]
        public void AddUserShouldSuccess()
        {
            User user_test = new User();
            user_test.UserName = "John";
            user_test.Email = "ab@email.com";
            user_test.Password = "Temus@123456";
            user_test.Role = "Admin";
            user_test.IsBlocked = false;
            user_test.IsPasswordReset = false;
            user_test.FirstName = "John";
            user_test.LastName = "Peters";
            user_test.MobileNo = "65304475";
            user_test.DOB = "2000-12-12";
            user_test.Gender = "Male";
            user_test.PostalCode = "570177";

            var actual = repo.RegisterUser(user_test);
            Assert.IsAssignableFrom<User>(actual);
            Assert.That(actual.UserName, Is.EqualTo("John"));
        }


        [Test, Order(2)]
        public void LoginUserShouldFail()
        {
            Cred user_test = new Cred() { UserName = "John", Password = "Temus@1" };

            var actual = repo.Login(user_test.UserName, user_test.Password);
            Assert.False(actual);
        }


        [Test, Order(3)]
        public void LoginUserShouldSuccess()
        {
            Cred user_test = new Cred() { UserName = "John", Password = "Temus@123456" };

            var actual = repo.Login(user_test.UserName, user_test.Password);
            Assert.True(actual);
        }


        [Test, Order(4)]
        public void GetUserByUserNameShouldSuccess()
        {
            string userName = "John";
            var actual = repo.GetUserByUserName(userName);
            Assert.IsAssignableFrom<User>(actual);
            Assert.That(actual.Email, Is.EqualTo("ab@email.com"));
            Assert.That(actual.Password, Is.EqualTo("Temus@123456"));
        }


        [Test, Order(5)]
        public void UpdateUserInfoShouldSuccess()
        {
            string userName = "John";
            string firstName = "Jonathan";
            string lastName = "Peters";
            string mobileNo = "91223823";
            string postalCode = "500093";
            repo.UpdateUserInfo(userName, firstName, lastName, mobileNo, postalCode);
            var actual = repo.GetUserByUserName(userName);
            Assert.IsAssignableFrom<User>(actual);
            Assert.That(actual.FirstName, Is.EqualTo("Jonathan"));
            Assert.That(actual.MobileNo, Is.EqualTo("91223823"));
        }


        [Order(6)]
        public void DeleteUserShouldSuccess()
        {
            var filter = Builders<User>.Filter.Eq("Email", "ab@email.com");
            collection.DeleteOne(filter);            
            repo.DeleteUser("John");           

            //var actual = repo.GetUserByUserName("John");
            //Assert.IsAssignableFrom<User>(actual);
            //Assert.That(actual.UserName, Is.EqualTo(null));
        }

    }
}