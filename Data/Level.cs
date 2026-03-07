namespace OpticalServer.Models
{
    public class Level
    {
        public long LevelId { get; set; }
        public string LevelName { get; set; }
        public long? OwnerId { get; set; }
        public DateTime CreationDate { get; set; }
        public string Path { get; set; }
    }
}