using FantasyProcessor.Services.Channels;

namespace FantasyProcessor.Services.Persisting;

public class BaseballPersistingService : ConnectedService<List<APIs.CBS.Player>>
{
    public BaseballPersistingService(BaseballChannelService channelService, ILogger<BaseballPersistingService> logger)
        :base(channelService, logger) { }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var channelMessage = await _channelService.ServiceChannel.Reader.ReadAsync(stoppingToken);

            ImportEngine importEngine = new ImportEngine(channelMessage.Topic, channelMessage.Payload);
            importEngine.ImportPlayerData();

            // This could be limited to dirty flags only, etc.
            AnalyticsEngine engine = new AnalyticsEngine(channelMessage.Topic);
            engine.UpdateAgeDifferentials();
        }
    }
}