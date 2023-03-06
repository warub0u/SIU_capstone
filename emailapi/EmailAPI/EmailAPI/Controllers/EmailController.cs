using EmailAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EmailAPI.Services;
using System.Net.Mail;


namespace EmailAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        // DECLARE emailService VARIABLE
        private readonly IEmailService emailService;

        // INJECT DEPENDENCY OF emailService VIA CONSTRUCTOR
        public EmailController(IEmailService emailService)
        {
            this.emailService = emailService;
        }


        //[HttpGet]
        [HttpPost]
        public IActionResult Email(Request request)
        //public IActionResult Email([FromBody] Request request)
        //public IActionResult Email(string email, string password)
        {
            
            // EMAIL MESSAGE
            var message =
            new EmailMessage(new string[] { request.Email },
            "Notifications: You have requested a password reset.",
            $"This is your generated password: {request.Password}. \nPlease login with this password and change your password thereafter.");

            // SEND THE EMAIL
            emailService.SendEmail(message);
            Console.WriteLine(message);
            return Ok();

        }


        [HttpPost]
        [Route("Welcome")]

        public IActionResult WelcomeEmail(User user)
        //public IActionResult Email([FromBody] Request request)
        //public IActionResult Email(string email, string password)
        {            
            // EMAIL MESSAGE
            var message =
            new EmailMessage(new string[] { user.Email },
            "Welcome to MovEase!",
            $"Hello {user.FirstName},\n\nWe're glad to have you onboard MovEase! \n\nThank you for signing up!\n\n We are excited to make your travelling that much easier and we hope you'll enjoy using our app!\n\n Best Travels,\n\nMovEase Team");

            // SEND THE EMAIL
            emailService.SendEmail(message);
            Console.WriteLine(message);
            return Ok();

        }
    }
}
