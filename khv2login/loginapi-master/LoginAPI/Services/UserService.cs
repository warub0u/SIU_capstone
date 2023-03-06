using LoginAPI.Exceptions;
using LoginAPI.Models;
using LoginAPI.Repo;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace LoginAPI.Services
{
    public class UserService : IUserService
    {
        // CREATE repo VARIABLE AND INJECT DEPENDENCY IUserRepo
        private readonly IUserRepo repo;
        public UserService(IUserRepo repo)
        {
            this.repo = repo;
        }

        // GET USER BY USERNAME METHOD
        public User GetUserByUserName(string userName)
        {
            if (repo.GetUserByUserName(userName) == null)
            {
                throw new UserNotFoundException($"No such username: {userName}");
            }
            return repo.GetUserByUserName(userName);
        }

        // GET USER BY EMAIL METHOD
        public User GetUserByEmail(string email)
        {
            if (repo.GetUserByEmail(email) == null)
            {
                throw new UserNotFoundException($"No such email: {email}");
            }
            return repo.GetUserByEmail(email);
        }


        // LOGIN METHOD
        public bool Login(string userName, string password)
        {
            if (repo.Login(userName, password))
            {
                User u = repo.GetUserByUserName(userName);

                if (u.IsBlocked == true)
                {
                    throw new UserIsBlockedException("You have been blocked and unauthorized from access");
                }
                else
                {
                    return true;
                }
            }
            throw new UserNotFoundException($"Your username/password is wrong!");
        }

        // REGISTER USER METHOD
        public User RegisterUser(User user)
        {
            if (user.UserName == null)
            {
                throw new UsernameNotProvidedException("Username not provided");
            }
            else 
            {                
                if (repo.GetUserByUserName(user.UserName) != null)
                {
                    throw new UserAlreadyExistsException($"Username {user.UserName} already exists!");
                }
                else if (repo.GetUserByEmail(user.Email) != null)
                {
                    throw new UserAlreadyExistsException($"Email {user.Email} already exists!");
                }
                else
                {
                    return repo.RegisterUser(user);
                }
            }            
        }

        // GET ALL USERS METHOD
        public List<User> GetAllUsers()
        {
            return repo.GetAllUsers();            
        }


        // DELETE USER BASED ON USERNAME
        public void DeleteUser(string userName)
        {
            if (repo.GetUserByUserName(userName) == null)
            {
                throw new UserNotFoundException($"No such username");
            }
            else
            {
                repo.DeleteUser(userName);
            }
        }


        // UPDATE USER PASSWORD METHOD
        public void UpdateUserPassword(string userName, string old_pw, string new_pw)
        {
            if (repo.GetUserByUserName(userName) == null)
            {
                throw new UserNotFoundException($"No such username");
            }
            else if(repo.GetUserByUserName(userName).Password != old_pw)
            {
                throw new OldPasswordIncorrectException($"Your old password is incorrect. Password not updated.");
            }
            else
            {
                repo.UpdateUserPassword(userName, old_pw, new_pw);
            }
        }


        // UPDATE OTHER USER INFO (FIRSTNAME, LASTNAME, MOBILENO, POSTALCODE) 
        public void UpdateUserInfo(string userName, string firstName, string lastName, string mobileNo, string postalCode)
        {
            if (repo.GetUserByUserName(userName) == null)
            {
                throw new UserNotFoundException($"No such username");
            }
            else
            {
                repo.UpdateUserInfo(userName, firstName, lastName, mobileNo, postalCode);
            }
        }


        // UPDATE USER RIGHTS METHOD
        public void UpdateUserRights(string userName, string role, bool isBlocked)
        {
            if (repo.GetUserByUserName(userName) == null)
            {
                throw new UserNotFoundException($"No such username");
            }
            else
            {
                repo.UpdateUserRights(userName, role, isBlocked);
            }
        }


        // REGISTER ADMIN METHOD
        public void RegisterAdmin(string passcode)
        {
            if(repo.GetUserByUserName("DefaultAdmin") != null)
            {
                throw new UserAlreadyExistsException("DefaultAdmin already exists!");
            }
            repo.RegisterAdmin(passcode);
        }


        // AUTO GENERATE & UPDATE USER PASSWORD METHOD FOR PASSWORD RESET
        public string GenerateUpdateUserPassword(string email)
        {
            if (repo.GetUserByEmail(email) == null)
            {
                throw new UserNotFoundException($"No such email: {email}");
            }
            return repo.GenerateUpdateUserPassword(email);
        }
        

        // HTTP CLIENT GET METHOD FOR EMAIL API
        public async Task<bool> GetMethodToEmail(string email, string password)
        {
            bool response = await repo.GetMethodToEmail(email, password);
            if (response == true)
            {
                return response;
            }
            else
            {
                throw new UnableToSendEmailException("email is not sent successfully");
            }
            //return true;
        }



        // UPLOAD IMAGE METHOD
        // (CAN BE REMOVED SINCE UNUSED)
        //public void UploadImage(IFormFile file, string imageName, string userName)
        //{
        //    if (repo.GetUserByUserName(userName) == null)
        //    {
        //        throw new UserNotFoundException($"No such username: {userName}");
        //    }
        //    else
        //    {
        //        repo.UploadImage(file, imageName, userName);
        //    }
        //}

        // GET IMAGE METHOD
        // (CAN BE REMOVED SINCE UNUSED)
        //public async Task<UserImage> GetImage(string userName) 
        //{
        //    if (repo.GetUserByUserName(userName) == null)
        //    {
        //        throw new UserNotFoundException($"No such username: {userName}");
        //    }
        //    else
        //    {
        //        return await repo.GetImage(userName);
        //    }
        //}

    }
}
