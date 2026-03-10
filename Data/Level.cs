using System.ComponentModel.DataAnnotations.Schema;

namespace OpticalServer.Models
{
    public class Level
    {
        [Column("level_id")]
        public long LevelId { get; set; }
        [Column("name")]
        public string LevelName { get; set; }
        [Column("owner_id")]
        public long? OwnerId { get; set; }
        [Column("created_at")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreationDate { get; set; }
        [Column("path")]
        public string Path { get; set; }
        [Column("views")]
        public int Views { get; set; }
    }
}