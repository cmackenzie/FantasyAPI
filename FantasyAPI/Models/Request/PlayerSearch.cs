namespace FantasyAPI.Models.Request
{
    public class PlayerSearch
	{
        public string? sport { get; set; }
        public string? last_name_prefix { get; set; }
        public int[]? age { get; set; }
        public string? position { get; set; }
    }
}

