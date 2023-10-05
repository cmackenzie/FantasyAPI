using Microsoft.EntityFrameworkCore;

namespace FantasyCore.Models
{
    [Index(nameof(Name))]
    public class Sport
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public List<Player> Players { get; set; }
        public List<Team> Teams { get; set; }
        public List<Position> Positions { get; set; }
    }
}
