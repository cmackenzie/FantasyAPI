namespace FantasyAPI.Services.Models
{
	public class SearchResponse<T>
	{
        public IEnumerable<T> Items { get; set; }
        public int Count { get; set; }
	}
}

