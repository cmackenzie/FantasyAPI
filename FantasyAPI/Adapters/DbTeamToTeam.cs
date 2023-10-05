using FantasyAPI.Services.Models;
using FantasyCore;

namespace FantasyAPI.Adapters
{
    public class DbTeamToTeam : AdapterBase<FantasyCore.Models.Team, Team>
    {
        public override Team Convert(FantasyCore.Models.Team dbTeam)
        {
            return new Team()
            {
                Id = (int)dbTeam.Id,
                Name = dbTeam.Name
            };
        }
    }
}

