using Microsoft.EntityFrameworkCore;

namespace FantasyCore.Models
{
    [Index(nameof(Age))]
    [Index(nameof(LastName))]
    public class Player
    {
        public int? Id { get; set; }
        public int SportId { get; set; }
        public int TeamId { get; set; }
        public int PositionId { get; set; }
        public Sport Sport { get; set; }
        public Team Team { get; set; }
        public Position Position { get; set; }
        public List<News> News { get; set; }
        public string ExternalId { get; set; }
        public string Checksum { get; set; }
        public int Version { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? PhotoUrl { get; set; }
        public int Age { get; set; }
        public int JerseyNumber { get; set; }

    }
}

