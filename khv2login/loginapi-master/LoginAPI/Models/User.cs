using Microsoft.EntityFrameworkCore;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace LoginAPI.Models
{
    public class User
    {
        // DATA MODEL PROPERTIES 

        [BsonId]
        [Required]
        public string? UserName { get; set; }  

        [Required]
        public string? Email { get; set; }        

        [Required]
        public string? Password { get; set; }

        [Required]
        public string? Role { get; set; }

        [Required]
        public bool? IsBlocked { get; set; }        

        [Required]
        public bool? IsPasswordReset { get; set; }


        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? MobileNo { get; set; }
        [Required]
        public string? DOB { get; set; }
        [Required]
        public string? Gender { get; set; }
        [Required]
        public string? PostalCode { get; set; }


    }
}
