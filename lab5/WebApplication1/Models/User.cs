namespace WebApplication1.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        
    }
}
