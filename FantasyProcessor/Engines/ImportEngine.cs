using FantasyCore;
using FantasyProcessor.Adapters;
using FantasyCore.Models;

namespace FantasyProcessor
{
    /// <summary>
    /// Class primarily responsible for populating the fantasy db
    /// 
    /// Note: There are some tradeoffs made here for readability over performance
    /// I choose to iterate over the player list several times so that I may separate syncing of teams,positions,players
    /// Larger datasets would require rethinking that decisions
    /// Syncing to the database is limited to 100 records at a time though.
    /// </summary>
	public class ImportEngine
    {
        private const int MAX_UPDATE_CHUNK = 100;
        // Remove all team related positions
        private static readonly HashSet<string> _ignoredPositions =
            new HashSet<string>() { "DST", "ST", "D", "PS", "TQB" };

        private readonly string _sport;
        private readonly List<APIs.CBS.Player> _players;

        public ImportEngine(string sport, List<APIs.CBS.Player> players)
        {
            _sport = sport;
            _players = players
                .Where(p => !_ignoredPositions.Contains(p.position))
                .ToList();
        }

        /// <summary>
        /// Imports all data associated to a player
        /// The player itself, it's position and team
        /// Keeps track of the current state of the player with an md5 checksum
        /// </summary>
        public async void ImportPlayerData()
        {
            Sport sport = await SyncSport();
            Dictionary<string, Team> teams = await SyncTeams(sport);
            Dictionary<string, Position> positions = await SyncPositions(sport);
            await SyncPlayers(sport, teams, positions);
        }

        /// <summary>
        /// Save the sport to the db
        /// </summary>
        /// <returns></returns>
        private async Task<Sport> SyncSport()
        {
            DbRepository repository = new DbRepository();
            return await repository.GetOrCreateByAsync(new Sport() { Name = _sport });
        }

        /// <summary>
        /// Saves all teams associated to any player in the payload to the database
        /// Practically speaking this probably syncs all teams the first time this is run and never again.
        /// Also in more robust systems this is it's own service with it's own source of truth (different api endpoint)
        /// </summary>
        /// <param name="sport">The sport the team belongs to</param>
        /// <returns>A dictionary lookup the player sync can leverage</returns>
        private async Task<Dictionary<string, Team>> SyncTeams(Sport sport)
        {
            DbRepository repository = new DbRepository();

            Dictionary<string, Team> teamLookup = repository.Context.Teams
                .Where(t => t.SportId == sport.Id)
                .ToDictionary(t => t.Name, t => t, StringComparer.OrdinalIgnoreCase);

            List<string> teams = _players.Select(p => p.pro_team).ToHashSet().ToList();

            for (int i = 0; i < teams.Count; i++)
            {
                string teamString = teams[i];
                if (!teamLookup.ContainsKey(teamString))
                {
                    Team team = await repository.GetOrCreateByAsync(new Team()
                    {
                        Name = teamString,
                        SportId = (int)sport.Id
                    });

                    teamLookup.Add(team.Name, team);
                }
            }

            return teamLookup;
        }

        /// <summary>
        /// Saves all positions associated to any player in the payload to the database
        /// Practically speaking this probably syncs all positions the first time this is run and never again.
        /// Also in more robust systems this is it's own service with it's own source of truth (different api endpoint)
        /// </summary>
        /// <param name="sport">The sport the positions belongs to</param>
        /// <returns>A dictionary lookup the player sync can leverage</returns>
        private async Task<Dictionary<string, Position>> SyncPositions(Sport sport)
        {
            DbRepository repository = new DbRepository();

            Dictionary<string, Position> positionsLookup = repository.Context.Positions
                .Where(p => p.SportId == sport.Id)
                .ToDictionary(p => p.Name, p => p, StringComparer.OrdinalIgnoreCase);

            List<string> positions = _players.Select(p => p.position).ToHashSet().ToList();

            for (int i = 0; i < positions.Count; i++)
            {
                string positionString = positions[i];
                if (!positionsLookup.ContainsKey(positionString))
                {
                    Position position = await repository.GetOrCreateByAsync(new Position()
                    {
                        Name = positionString,
                        SportId = (int)sport.Id
                    });

                    positionsLookup.Add(position.Name, position);
                }
            }

            return positionsLookup;
        }

        /// <summary>
        /// Syncs players to the database. Uses a checksum to check whether a player is dirty
        /// and in need of updating. Updates are in done chunks of 100 entities.
        /// </summary>
        /// <param name="sport"></param>
        /// <param name="teams"></param>
        /// <param name="positions"></param>
        /// <returns></returns>
        private async Task SyncPlayers(Sport sport, Dictionary<string, Team> teams, Dictionary<string, Position> positions)
        {
            DbRepository repository = new DbRepository();
            ApiPlayerToPlayer adapter = new ApiPlayerToPlayer(sport, teams, positions);
            var playerChunks = _players
                .Select(adapter.Convert)
                .Chunk(MAX_UPDATE_CHUNK)
                .ToList();

            for(int i = 0; i < playerChunks.Count; ++i)
            {
                var playerChunk = playerChunks[i].ToList();
                var checksumLookup = repository.Context.Players
                    .Where(p => p.SportId == sport.Id)
                    .Select(p => new { Id = p.Id, ExternalId = p.ExternalId, Version = p.Version, Checksum = p.Checksum })
                    .ToDictionary(
                        p => p.ExternalId,
                        p => (p.Checksum, p.Id, p.Version)
                    );

                using (FantasyContext context = repository.Context)
                {
                    for (int j = 0; j < playerChunk.Count; j++)
                    {
                        Player dbPlayer = playerChunk[j];
                        // If we aren't tracking the player yet
                        // then we simply insert the players current data
                        if (!checksumLookup.ContainsKey(dbPlayer.ExternalId))
                        {
                            context.Players.Add(dbPlayer);
                        }
                        else
                        {
                            var existing = checksumLookup[dbPlayer.ExternalId];
                            // When the checksums don't match
                            // We need to flag this player for an update
                            if (existing.Checksum != dbPlayer.Checksum)
                            {
                                dbPlayer.Id = existing.Id;
                                dbPlayer.Version = existing.Version + 1;
                                context.Players.Update(dbPlayer);
                            }
                        }
                    }

                    context.SaveChanges();
                }
            }
        }
    }
}

