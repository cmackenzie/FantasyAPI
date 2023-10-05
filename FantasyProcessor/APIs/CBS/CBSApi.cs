using System.Text.Json;

namespace FantasyProcessor.APIs.CBS
{
	/// <summary>
	/// Class responsible for pulling the player data from CBS
	/// More robust versions of this would have considerable error handling around failed requests/bad data
	/// </summary>
	public class CBSApi
	{
		private static readonly string REQUEST_URL = "https://api.cbssports.com/fantasy/players/list?version=3.0&SPORT={0}&response_format=JSON";
        private static readonly HttpClient HTTP_CLIENT = new HttpClient();
		private readonly string _requestUrl;

        public CBSApi(string sport)
		{
			_requestUrl = String.Format(REQUEST_URL, sport);
		}

		public async Task<List<Player>> GetFullPlayerList()
		{
			var response = await HTTP_CLIENT.GetAsync(_requestUrl);
            var responseString = await response.Content.ReadAsStringAsync();

			CBSResponse<Player> cbsResponse = JsonSerializer.Deserialize<CBSResponse<Player>>(responseString);

			return cbsResponse.body.players;
        }
	}
}

