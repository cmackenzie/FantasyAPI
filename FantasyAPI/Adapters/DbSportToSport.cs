using FantasyAPI.Services.Models;
using FantasyCore;

namespace FantasyAPI.Adapters
{
    public class DbSportToSport : AdapterBase<FantasyCore.Models.Sport, Sport>
    {
        public override Sport Convert(FantasyCore.Models.Sport dbSport)
        {
            return new Sport()
            {
                Id = (int)dbSport.Id,
                Name = dbSport.Name
            };
        }
    }
}

