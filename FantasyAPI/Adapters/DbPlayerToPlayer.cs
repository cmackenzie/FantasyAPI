using FantasyAPI.Services.Models;
using FantasyCore;

namespace FantasyAPI.Adapters
{
    public class DbPlayerToPlayer : AdapterBase<FantasyCore.Models.Player, Player>
    {
        private static readonly DbSportToSport DB_SPORT_TO_SPORT = new DbSportToSport();
        private static readonly DbTeamToTeam DB_TEAM_TO_TEAM = new DbTeamToTeam();
        private static readonly DbPositionToPosition DB_POSITION_TO_POSITION = new DbPositionToPosition();

        public override Player? Convert(FantasyCore.Models.Player? dbPlayer)
        {
            if(dbPlayer == null)
            {
                return null;
            }

            return new Player()
            {
                Id = (int)dbPlayer.Id,
                Sport = DB_SPORT_TO_SPORT.Convert(dbPlayer.Sport),
                Team = DB_TEAM_TO_TEAM.Convert(dbPlayer.Team),
                Position = DB_POSITION_TO_POSITION.Convert(dbPlayer.Position),
                FirstName = dbPlayer.FirstName,
                LastName = dbPlayer.LastName,
                Age = dbPlayer.Age,
                JerseyNumber = dbPlayer.JerseyNumber
            };
        }
    }
}

