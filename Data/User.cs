using System.Data;

namespace OpticalServer.Models
{
    public class User
    {
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public DateTime CreationDate { get; set; } 
    }
}