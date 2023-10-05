using Microsoft.EntityFrameworkCore;

namespace FantasyCore.Models
{
    [Index(nameof(Name))]
    public class Position
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int SportId { get; set; }
        public int AverageAge { get; set; }
        public Sport Sport { get; set; }
        public List<Player> Players { get; set; }
    }
}