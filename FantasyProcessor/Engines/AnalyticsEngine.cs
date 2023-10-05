using FantasyCore;

using Microsoft.EntityFrameworkCore;

namespace FantasyProcessor
{
	/// <summary>
	/// The AnalyticsEngine job is simply to recalculate the average age for a given position
	/// As it is now, it will recalculate all positions any time a sport is updated.
	/// More intelligent versions of this would keep track of dirty positions/sports and only update those as needed. 
	/// </summary>
	public class AnalyticsEngine
	{
        private readonly string _sport;

        public AnalyticsEngine(string sport)
        {
            _sport = sport;
        }

        public void UpdateAgeDifferentials()
		{
			DbRepository repository = new DbRepository();

			var results = repository.Context.Players
				.Where(p => p.Sport.Name == _sport)
				.Where(p => p.Age != 0)
				.GroupBy(player => player.PositionId)
				.Select(p => new { PositionId = p.Key, Average = p.Average(t => t.Age) })
				.ToList();

            for(int i = 0;i < results.Count; ++i)
			{
				var result = results[i];

				repository.Context.Positions
					.Where(p => p.Id == result.PositionId)
					.ExecuteUpdate(position =>
                        position.SetProperty(p => p.AverageAge, result.Average));
            }
		}

    }
}

