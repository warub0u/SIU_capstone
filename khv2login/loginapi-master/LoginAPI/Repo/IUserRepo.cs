using LoginAPI.Models;
using Microsoft.AspNetCore.Mvc;


namespace LoginAPI.Repo
{
    public interface IUserRepo
    {
        // METHODS TO BE IMPLEMENTED FOR THE INTERFACE         
        User GetUserByUserName(string userName);
        User GetUserByEmail(string email);


        // METHODS TO BE IMPLEMENTED FOR THE INTERFACE FOR USERS  
        User RegisterUser(User user);
        bool Login(string userName, string password);
        void UpdateUserPassword(string userName, string old_pw, string new_pw);
        void UpdateUserInfo(string userName, string firstName, string lastName, string mobileNo, string postalCode);
        string GenerateUpdateUserPassword(string email);
        //void UploadImage(IFormFile file, string imageName, string userName);
        //Task<UserImage> GetImage(string userName);


        // METHODS TO BE IMPLEMENTED FOR THE INTERFACE FOR ADMIN
        List<User> GetAllUsers();        
        void DeleteUser(string userName);
        void UpdateUserRights(string userName, string role, bool isBlocked);
        void RegisterAdmin(string passcode);


        // METHODS TO BE IMPLEMENTED FOR THE INTERFACE FOR HTTPCLIENT
        Task<bool> GetMethodToEmail(string email, string password);


    }
}
