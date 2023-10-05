using FantasyCore;
using FantasyProcessor.APIs.CBS;
using FantasyProcessor.Services.Channels;
using FantasyProcessor.Services.Models;

namespace FantasyProcessor.Services;

public class BaseballPollingService : ScheduledPollingService
{
    public BaseballPollingService(BaseballChannelService channelService, ILogger<BaseballPollingService> logger)
        :base(channelService, logger) { }

    protected override async Task RunAsync(CancellationToken stoppingToken)
    {
        CBSApi cbsApi = new CBSApi(Constants.BASEBALL);
        var response = await cbsApi.GetFullPlayerList();

        ChannelMessage<List<Player>> message = new ChannelMessage<List<Player>>(Constants.BASEBALL, response);
        await _channelService.ServiceChannel.Writer.WriteAsync(message);
    }
}