
using FantasyCore;
using FantasyAPI.Adapters;
using FantasyAPI.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace FantasyAPI.Services
{
	public class PlayerService
	{
		private static readonly DbPlayerToPlayer DB_PLAYER_TO_PLAYER = new DbPlayerToPlayer();

		private readonly DbRepository _dbRepository;
		
		public PlayerService()
		{
			_dbRepository = new DbRepository();
        }

		public Player? GetPlayerById(int id)
		{
            FantasyCore.Models.Player? player = _dbRepository.Context.Players
                .Include(p => p.Sport)
                .Include(p => p.Position)
                .Include(p => p.Team)
                .Where(p => p.Id == id)
				.FirstOrDefault();

            return DB_PLAYER_TO_PLAYER.Convert(player);
		}

		public SearchResponse<Player> FindPlayersByCriteria(SearchCriteria searchCriteria)
		{
			IQueryable<FantasyCore.Models.Player> query = _dbRepository.Context.Players
				.Include(p => p.Sport)
				.Include(p => p.Position)
				.Include(p => p.Team);

			if (searchCriteria.Sport != null)
            {
                query = query.Where(p => p.Sport.Name == searchCriteria.Sport.ToLower());
            }

            if (searchCriteria.Position != null)
            {
                query = query.Where(p => p.Position.Name == searchCriteria.Position.ToUpper());
            }

            if (searchCriteria.LastNamePrefix != null)
            {
                query = query.Where(p => p.LastName.ToLower().StartsWith(searchCriteria.LastNamePrefix.ToLower()));
            }

            if (searchCriteria.Age != null)
			{
				if(searchCriteria.Age.Length == 1)
				{
                    query = query.Where(p => p.Age == searchCriteria.Age.First());
				}
				else
				{
					query = query
						.Where(p => p.Age >= searchCriteria.Age[0])
						.Where(p => p.Age <= searchCriteria.Age[1]);
				}
            }

			int count = query.Count();

			query = query
				.Skip(searchCriteria.Offset)
				.Take(searchCriteria.Take);

			return new SearchResponse<Player>()
			{
				Items = DB_PLAYER_TO_PLAYER.Convert(query),
				Count = count
			};
        }
	}
}

