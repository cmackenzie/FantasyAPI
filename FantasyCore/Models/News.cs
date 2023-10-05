namespace FantasyCore.Models
{
    public class News
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int PlayerId { get; set; }
        public Player Player { get; set; }
    }
}