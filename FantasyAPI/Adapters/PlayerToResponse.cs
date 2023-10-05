using FantasyAPI.Models.Response;
using FantasyCore;

namespace FantasyAPI.Adapters
{
    public class PlayerToResponse : AdapterBase<Services.Models.Player?, Player?>
    {
        public override Player? Convert(Services.Models.Player? player)
        {
            if(player == null)
            {
                return null;
            }

            return new Player()
            {
                id = player.Id,
                name_brief = GetNameBrief(player),
                first_name = player.FirstName,
                last_name = player.LastName,
                position = player.Position.Name,
                age = player.Age,
                // Some players have an age of 0, in this case, this delta is the average
                // Should I have removed players w/out an age?
                average_position_age_diff = Math.Abs(player.Age - player.Position.AverageAge)
            };
        }

        private string GetNameBrief(Services.Models.Player player)
        {
            try
            {
                switch (player.Sport.Name)
                {
                    case Constants.BASKETBALL:
                        return String.Format("{0} {1}.", player.FirstName, player.LastName.First());
                    case Constants.BASEBALL:
                        return String.Format("{0}. {1}.", player.FirstName.First(), player.LastName.First());
                    case Constants.FOOTBALL:
                        return String.Format("{0}. {1}", player.FirstName.First(), player.LastName);
                    default:
                        return String.Format("{0} {1}", player.FirstName, player.LastName);
                }
            }
            catch(InvalidOperationException)
            {
                return String.Format("{0} {1}", player.FirstName, player.LastName);
            }
        }
    }
}

