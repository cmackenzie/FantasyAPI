using FantasyAPI.Models.Request;
using FantasyAPI.Services.Models;
using FantasyCore;

namespace FantasyAPI.Adapters
{
    public class PlayerSearchToSearchCriteria : AdapterBase<PlayerSearch, SearchCriteria>
    {
        public override SearchCriteria Convert(PlayerSearch search)
        {
            return new SearchCriteria()
            {
                Age = search.age,
                Sport = search.sport,
                Position = search.position,
                LastNamePrefix = search.last_name_prefix
            };
        }
    }
}

