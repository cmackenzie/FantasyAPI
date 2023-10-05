using FantasyCore;
using FantasyCore.Models;
using FantasyProcessor.Utils;

namespace FantasyProcessor.Adapters
{
	/// <summary>
	/// Converts an api representation of a player to a sport generic database player
	/// </summary>
	public class ApiPlayerToPlayer : AdapterBase<APIs.CBS.Player, Player>
    {
		private readonly Sport _sport;
		private readonly Dictionary<string, Team> _teamLookup;
		private readonly Dictionary<string, Position> _positionLoookup;

		public ApiPlayerToPlayer(Sport sport, Dictionary<string, Team> teamLookup, Dictionary<string, Position> positionLoookup)
		{
			_sport = sport;
			_teamLookup = teamLookup;
            _positionLoookup = positionLoookup;
		}

        /// <summary>
        /// Converts an api representation of an api player to a sport generic database player.
        /// </summary>
        /// <param name="apiPlayer"></param>
        /// <returns></returns>
        public override Player Convert(APIs.CBS.Player apiPlayer)
		{
			Player player = new Player()
			{
				ExternalId = apiPlayer.id,
				FirstName = apiPlayer.firstname,
				LastName = apiPlayer.lastname,
				FullName = apiPlayer.fullname,
				Age = apiPlayer.age,
				SportId = (int)_sport.Id,
                TeamId = (int)_teamLookup[apiPlayer.pro_team].Id,
				PositionId = (int)_positionLoookup[apiPlayer.position].Id
			};

			player.Checksum = PlayerChecksumHelper.GenerateChecksum(player);
			player.Version = 1;

			return player;
		}
	}
}

