using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;


namespace LoginAPI.Models
{
    public class Request
    {
        // DATA MODEL FOR REQUEST
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
