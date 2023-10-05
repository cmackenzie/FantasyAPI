using FantasyAPI.Services.Models;
using FantasyCore;

namespace FantasyAPI.Adapters
{
    public class DbPositionToPosition : AdapterBase<FantasyCore.Models.Position, Position>
    {
        public override Position Convert(FantasyCore.Models.Position dbPosition)
        {
            return new Position()
            {
                Id = (int)dbPosition.Id,
                Name = dbPosition.Name,
                AverageAge = dbPosition.AverageAge
            };
        }
    }
}

