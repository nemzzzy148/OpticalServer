using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpticalServer.Models
{
    public enum ReactionType
    {
        like,
        dislike
    }
    public class LevelReaction
    {
        [Key]
        [Column("reaction_id")]
        public long ReactionId { get; set; }
        [Column("user_id")]
        public long UserId { get; set; }
        [Column("level_id")]
        public long LevelId { get; set; }
        [Column("reaction")]
        public ReactionType ReactionType { get; set; }

        [Column("created_at")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreationDate { get; set; }
    }
}