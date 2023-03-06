using LoginAPI.Models;
using MongoDB.Driver;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.OpenApi.Any;
using System.Net.Http;
using System.Threading;
using MongoDB.Bson.IO;
using Newtonsoft.Json;
using JsonConvert = Newtonsoft.Json.JsonConvert;
using System.Text;
using System.Data;

namespace LoginAPI.Repo
{
    public class UserRepo : IUserRepo
    {
        // CREATE db VARIABLE AND INJECT DEPENDENCY DATACONTEXT
        private readonly DataContext db;
        private readonly Generator gen;
        //private readonly HttpClient httpClient;
        private readonly IConfiguration config;
        private string emailURL;

        public UserRepo(DataContext db, IConfiguration config)
        {
            this.db = db;
            this.config = config;
            //var builder = new ConfigurationBuilder();
            //var config = builder.Build();
            
        }

        //public UserRepo(IGenerator gen)
        //{
        //    this.gen = gen;
        //}

        //public UserRepo(HttpClient httpClient)
        //{
        //    this.httpClient = httpClient;

        //    //this.config = config;
        //    var builder = new ConfigurationBuilder();
        //    var config = builder.Build();
        //    string emailURL = config["EmailURL"];
        //    httpClient.BaseAddress = new Uri(emailURL);
        //    //httpClient.BaseAddress = new Uri("http://localhost:5100"); // docker container port
        //}



        // GET USER BY USERNAME METHOD
        public User GetUserByUserName(string userName)
        {
            return db.Users.Find(x => x.UserName == userName).FirstOrDefault();
        }

        // GET USER BY EMAIL METHOD
        public User GetUserByEmail(string email)
        {
            return db.Users.Find(x => x.Email == email).FirstOrDefault();
        }


        // LOGIN METHOD
        public bool Login(string userName, string password)
        {
            var p = db.Users.Find(x => (x.UserName == userName) && (x.Password == password)).FirstOrDefault();

            if (p != null)
            {
                return true;
            }
            return false;
        }

        // REGISTER USER METHOD
        public User RegisterUser(User user)
        {
            db.Users.InsertOne(user);
            return user;
        }

        // GET USERS METHOD
        public List<User> GetAllUsers()
        {
            return db.Users.Find(x=>true).ToList();
        }

        // DELETE USER BY USERNAME METHOD
        public void DeleteUser(string userName)
        {
            db.Users.DeleteOne(x => x.UserName == userName);
        }

        // UPDATE USER PASSWORD METHOD
        public void UpdateUserPassword(string userName, string old_pw, string new_pw)
        {
            if (GetUserByUserName(userName).Password == old_pw)
            {
                var filter = Builders<User>.Filter.Where(x => x.UserName == userName);
                var update = Builders<User>.Update.Set(x => x.Password, new_pw)
                    .Set(x => x.IsPasswordReset, false);
                db.Users.UpdateOne(filter, update);
            }            
        }


        // UPDATE OTHER USER INFO (FIRSTNAME, LASTNAME, MOBILENO, POSTALCODE) 
        public void UpdateUserInfo(string userName, string firstName, string lastName, string mobileNo, string postalCode)
        {
            var filter = Builders<User>.Filter.Where(x => x.UserName == userName);
            var update = Builders<User>.Update
                .Set(x => x.FirstName, firstName)
                .Set(x => x.LastName, lastName)
                .Set(x => x.MobileNo, mobileNo)
                .Set(x => x.PostalCode, postalCode);
            db.Users.UpdateOne(filter, update);
        }


        // UPDATE USER RIGHTS METHOD FOR ADMIN
        public void UpdateUserRights(string userName, string role, bool isBlocked)
        {
            var filter = Builders<User>.Filter.Where(x => x.UserName == userName);
            var update = Builders<User>.Update
                .Set(x => x.Role, role)
                .Set(x => x.IsBlocked, isBlocked);
            db.Users.UpdateOne(filter, update);
        }

        // REGISTER ADMIN METHOD
        public void RegisterAdmin(string passcode)
        {
            User user = new User() { };

            if (passcode == "12345")
            {                
                user.UserName = "DefaultAdmin";
                user.Email = "admin@email.com";
                user.DOB = "2020-01-01";
                user.Password = "123456";
                user.Role = "Admin";
                user.IsBlocked = false;
                user.IsPasswordReset = false;
                user.FirstName = "";
                user.LastName = "";
                user.MobileNo = "";
                user.Gender = "";
                user.PostalCode = "";
                db.Users.InsertOne(user);                
            }            
        }


        // AUTO GENERATE & UPDATE USER PASSWORD METHOD FOR PASSWORD RESET
        public string GenerateUpdateUserPassword(string email)
        {
            Generator gen = new Generator();
            User u = db.Users.Find(x => x.Email == email).FirstOrDefault();
            string genRecoveryPassword = gen.GetRecovery();
            
            var filter = Builders<User>.Filter.Where(x => x.UserName == u.UserName);
            var update = Builders<User>.Update.Set(x => x.Password, genRecoveryPassword)
                .Set(x => x.IsPasswordReset, true);
            db.Users.UpdateOne(filter, update);
            return genRecoveryPassword;
        }          


        //HTTP CLIENT GET METHOD FOR EMAIL API
        public async Task<bool> GetMethodToEmail(string email, string password)
        {
            emailURL = config.GetValue<string>("EmailURL");
            HttpClient httpClient = new HttpClient();            
            httpClient.BaseAddress = new Uri(emailURL);
            //httpClient.BaseAddress = new Uri("https://localhost:7178"); // locally run EmailAPI https port
            //httpClient.BaseAddress = new Uri("http://localhost:5100"); // docker container port
            //Console.WriteLine(httpClient.BaseAddress);

            // Create the request json object
            Request request = new Request();
            request.Email = email;
            request.Password = password;

            // Send the request via the client
            // Serialize the request and convert to json string
            string json = JsonConvert.SerializeObject(request);
            // Convert the json string to HttpContent type
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            // Post async with the json HttpContent
            var response = await httpClient.PostAsync($"api/Email", content);
            
            return true;

        }



        // UPLOAD IMAGE METHOD
        // (CAN BE REMOVED SINCE UNUSED)
        //public async void UploadImage(IFormFile file, string imageName, string userName)
        //{
        //    //MONGODB PROFILE PICS STORAGE
        //    // delete the image in MongoDB for the userName first
        //    var filter = Builders<UserImage>.Filter.Eq(x => x.UserName, userName);
        //    await db.UserImages.DeleteManyAsync(filter);
        //    Console.WriteLine("image deleted");

        //    // add the image from the userName into MongoDB
        //    using (var ms = new MemoryStream())
        //    {
        //        await file.CopyToAsync(ms);
        //        var userImage = new UserImage()
        //        {
        //            UserName = userName,
        //            ImageName = imageName,
        //            ImageData = ms.ToArray(),
        //            ImageContent = file.ContentType
        //        };
        //        await db.UserImages.InsertOneAsync(userImage);
        //        Console.WriteLine("image added");
        //    }
            

        // STORING IMAGES IN FOLDER IN ASSETS/PICTURES FOLDER
        // copy files into the folder
        //string sourcePath = "\\";
        //string targetPath = "\\destLoc\\10_new";

        //using (var ms = new MemoryStream())
        //{
        //    await file.CopyToAsync(ms);
        //}
        //}  


        // GET IMAGE METHOD FOR MONGODB 
        //public async Task<UserImage> GetImage(string userName)
        //{
        //    var image = await db.UserImages.Find(x => x.UserName == userName).FirstOrDefaultAsync();            
        //    return image;
        //}

        // GET IMAGE METHOD 2 FOR MONGODB
        // (CAN BE REMOVED SINCE UNUSED)
        //public async Task<UserImage> GetImage(string userName)
        //{
        //    var filter = Builders<UserImage>.Filter.Eq(x => x.UserName, userName);
        //    var userImage = await db.UserImages.Find(filter).FirstOrDefaultAsync();
        //    return userImage;
        //}

    }
}
