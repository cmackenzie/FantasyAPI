using FantasyProcessor.APIs.CBS;
using FantasyProcessor.Services.Channels;
namespace FantasyProcessor.Services;

// An abstract class to abstract away some of the complexity of scheduling repeating services
// Long term you'd pull the delay and scheduling from configuration or a datastore/database for more finegrained control
public abstract class ScheduledPollingService : ConnectedService<List<Player>>
{
    private const int DELAY_TIME_MS = 30000;

    public ScheduledPollingService(ChannelService<List<Player>> channelService, ILogger<ScheduledPollingService> logger)
        :base(channelService, logger) { }

    protected abstract Task RunAsync(CancellationToken stoppingToken);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Started running at: {time}", DateTimeOffset.Now.ToUnixTimeSeconds());

            await RunAsync(stoppingToken);

            _logger.LogInformation("Finished running at: {time}", DateTimeOffset.Now.ToUnixTimeSeconds());

            await Task.Delay(DELAY_TIME_MS, stoppingToken);
        }
    }
}

