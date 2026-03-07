using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpticalServer.Models
{
    public class User
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreationDate { get; set; } 
    }
}