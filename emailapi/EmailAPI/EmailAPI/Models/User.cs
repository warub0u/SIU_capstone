using System.ComponentModel.DataAnnotations;

namespace EmailAPI.Models
{
    public class User
    {
        // DATA MODEL PROPERTIES 
        
        public string UserName { get; set; } 

        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string Email { get; set; }  
      

    }
}
