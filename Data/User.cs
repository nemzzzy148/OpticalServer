using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpticalServer.Models
{
    public class User
    {
        [Column("user_id")]
        public long UserId { get; set; }
        [Column("username")]
        public string UserName { get; set; }
        [Column("password_hash")]
        public string PasswordHash { get; set; }
        [Column("created_at")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreationDate { get; set; } 
    }
}