using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoginAPI.Models
{
    public class UserImage
    {
        [ForeignKey("User")]
        public string? UserName { get; set; }  
        public string? ImageName { get; set; }
        public byte[]? ImageData { get; set; }
        public string? ImageContent { get; set; }

    }
}
