using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class L5User
    {
        [Key]
        public int UserId { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        
    }
}
