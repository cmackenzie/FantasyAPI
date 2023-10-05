namespace FantasyAPI.Services.Models
{
    public class Player
    {
        public int Id { get; set; }
        public Sport Sport { get; set; }
        public Team Team { get; set; }
        public Position Position { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? PhotoUrl { get; set; }
        public int Age { get; set; }
        public int JerseyNumber { get; set; }
    }
}

