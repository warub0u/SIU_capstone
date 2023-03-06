using LoginAPI.Exceptions;
using LoginAPI.Filters;
using LoginAPI.Models;
using LoginAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System.Net.Http.Headers;
using System.IO;
using Amazon.Auth.AccessControlPolicy;

namespace LoginAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [MyExceptions]
    public class UserController : ControllerBase
    {
        // DECLARE INTERFACE VARIABLES
        private readonly IUserService userService;
        private readonly ITokenGeneratorService tokenGenService;

        // INJECT DEPENDENCIES
        public UserController(IUserService userservice, ITokenGeneratorService service)
        {
            this.userService = userservice;
            this.tokenGenService = service;
        }
            
        // REGISTER USER METHOD VIA HTTP POST
        [HttpPost("register")]
        public IActionResult Register(User user)
        {
            return StatusCode(201, userService.RegisterUser(user));
        }

        // LOGIN USER METHOD VIA HTTP POST
        [HttpPost("login")]
        public IActionResult Login(Cred user)
        {
            userService.Login(user.UserName, user.Password);
            return Ok(tokenGenService.GenerateToken(user.UserName, user.Password));
        }

        //  GET USER BY USERNAME METHOD VIA HTTP GET
        [HttpGet("{userName}")]        
        public IActionResult GetUserByUserName(string userName)
        {
            return Ok(userService.GetUserByUserName(userName));
        }

        //  GET USER BY EMAIL METHOD VIA HTTP GET
        [HttpGet("getemail/{email}")]
        public IActionResult GetUserByEmail(string email)
        {
            return Ok(userService.GetUserByEmail(email));
        }

        //  GET ALL USERS VIA HTTP GET
        [HttpGet]
        //[Authorize(Roles ="Admin")]
        public IActionResult GetAllUsers()
        {
            return Ok(userService.GetAllUsers());
        }

        // DELETE USER BASED ON USERNAME VIA HTTP DELETE
        [HttpDelete("{userName}")]
        //[Authorize(Roles = "Admin")]
        public IActionResult DeleteUser(string userName)
        {
            userService.DeleteUser(userName);
            return Ok("User deleted successfully");
        }

        // UPDATE USER PASSWORD VIA HTTP PUT
        [HttpPut("password")]
        public IActionResult UpdateUserPassword(UpdateUserPW updateuserpw)
        {
            userService.UpdateUserPassword(updateuserpw.UserName, updateuserpw.Old_pw, updateuserpw.New_pw);
            return Ok(new {message = "User password updated successfully" });
        }


        // UPDATE OTHER USER INFO (FIRSTNAME, LASTNAME, MOBILENO, POSTALCODE) VIA HTTP PUT
        [HttpPut("userinfo")]
        public IActionResult UpdateUserInfo(User user)
        {
            userService.UpdateUserInfo(user.UserName, user.FirstName, user.LastName, user.MobileNo, user.PostalCode);
            return GetUserByUserName(user.UserName);
        }


        // UPDATE USER RIGHTS VIA HTTP PUT
        [HttpPut("userrights")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateUserRights(string userName, string role, bool isBlocked)
        {
            userService.UpdateUserRights(userName, role, isBlocked);
            return Ok(new { message = "User rights updated successfully" });
        }

        // REGISTER ADMIN VIA HTTP POST
        [HttpPost]
        public IActionResult RegisterAdmin(string passcode)
        {
            userService.RegisterAdmin(passcode);
            return Ok("Admin added successfully");
        }

        // USER PASSWORD RESET METHOD
        [HttpPost]
        [Route("passwordreset")]
        public async Task<IActionResult> ResetPassword(string email)
        {
            string pw = userService.GenerateUpdateUserPassword(email);
            await userService.GetMethodToEmail(email, pw);
            return GetUserByEmail(email);
        }


        // UPLOAD IMAGE METHOD
        [HttpPost, DisableRequestSizeLimit]
        [Route("profilepic/{userName}")]
        public IActionResult Upload(IFormFile imagefile, string userName)
        {
            try
            {
                //var file = Request.Form.Files[0];
                var file = imagefile;
                var folderName = Path.Combine("Assets", "Pictures");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                // Delete existing picture from user
                DirectoryInfo d = new DirectoryInfo(pathToSave);
                FileInfo[] f = d.GetFiles();
                FileInfo del_filepath = null;
                foreach (FileInfo fi in f)
                {
                    string fpath = Convert.ToString(fi);
                    string[] separators = { "[", "]." };
                    string[] arr = fpath.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    if (arr[arr.Count() - 2] == userName)
                    {
                        del_filepath = fi;
                    }                   
                }
                if (del_filepath != null)
                {
                    del_filepath.Delete();
                }    

                // Save user picture
                if (file.Length > 0)
                {
                    // extract filename from HTTP response and convert the filename to [userName].jpg and save the fileName
                    // in the filepath inside Assets/Pictures, and return the filepath
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"'); 
                                        
                    string[] arr = (Convert.ToString(fileName)).Split(".", StringSplitOptions.RemoveEmptyEntries);
                    fileName = $"[{userName}]." + arr[arr.Count()-1];

                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }     


        // GET IMAGE METHOD
        [HttpGet("profilepic/{userName}")]
        public async Task<IActionResult> GetImage(string userName)
        {
            // Read the image file from the server's file system
            var folderName = Path.Combine("Assets", "Pictures");
            var pathToRead = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            DirectoryInfo d = new DirectoryInfo(pathToRead);
            FileInfo[] f = d.GetFiles();
            string file_extension = "";

            foreach (FileInfo fi in f)
            {
                string fpath = Convert.ToString(fi);
                string[] separators = { "[", "]."};
                string[] arr = fpath.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                if (arr[arr.Count() - 2] == userName)
                {
                    file_extension = arr[arr.Count() - 1];
                }                
            }
            string imageName = $"[{userName}].{file_extension}";
            Console.WriteLine(imageName);
            var imagePath = Path.Combine(pathToRead, imageName);
            var imageFileStream = new FileStream(imagePath, FileMode.Open);

            // Set the content type to tell the browser what type of file it is
            var imageMimeType = $"image/{file_extension}";
            // FileExtensionContentTypeProvider class takes a file path as input and returns a boolean value
            // indicating whether it was able to determine the MIME type of the file.
            // If not able to determine, then will assign imageMimeType as "application/octet-stream".
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(imagePath, out imageMimeType))
            {
                imageMimeType = "application/octet-stream";
            }
            Console.WriteLine(imageMimeType);

            // Send the file contents as the response body
            return File(imageFileStream, imageMimeType);
        }



        // UPLOAD IMAGE METHOD
        //[HttpPost]
        //[Route("profilepic/{userName}")]
        //public IActionResult UploadImage(IFormFile file, string imageName, string userName)
        //{
        //    userService.UploadImage(file, imageName, userName);
        //    return Ok("Profile picture uploaded/updated");
        //}

        // GET IMAGE METHOD
        //[HttpGet("profilepic")]
        //public async Task<IActionResult> GetImage([FromQuery] string userName)
        //{
        //    var image = await userService.GetImage(userName);
        //    if (image == null)
        //    {
        //        return NotFound();
        //    }
        //    return File(image.ImageData, image.ImageContent);
        //}

        // HTTP CLIENT GET METHOD FOR EMAIL API
        //[HttpGet]
        //[Route("emailhttpclient")]
        //public IActionResult GetMethodToEmail(string email, string password)
        //{
        //    userService.GetMethodToEmail(email, password);
        //    return Ok("Email sent successfully");
        //}


    }
}
