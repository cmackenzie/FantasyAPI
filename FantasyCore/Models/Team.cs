namespace FantasyCore.Models
{
    public class Team
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int SportId { get; set; }
        public Sport Sport { get; set; }
        public List<Player> Players { get; set; }
    }
}
