namespace FantasyAPI.Services.Models
{
	public class SearchCriteria
    {
        public int Take { get; set; }
        public int Offset { get; set; }
        public string? Sport { get; set; }
        public string? LastNamePrefix { get; set; }
        public int[]? Age { get; set; }
        public string? Position { get; set; }
	}
}