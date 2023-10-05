using FantasyCore;
using FantasyProcessor.APIs.CBS;
using FantasyProcessor.Services.Models;
using FantasyProcessor.Services.Channels;

namespace FantasyProcessor.Services;

public class FootballPollingService : ScheduledPollingService
{
    public FootballPollingService(FootballChannelService channelService, ILogger<FootballPollingService> logger)
        :base(channelService, logger) { }

    protected override async Task RunAsync(CancellationToken stoppingToken)
    {
        CBSApi cbsApi = new CBSApi(Constants.FOOTBALL);
        var response = await cbsApi.GetFullPlayerList();

        ChannelMessage<List<Player>> message = new ChannelMessage<List<Player>>(Constants.FOOTBALL, response);
        await _channelService.ServiceChannel.Writer.WriteAsync(message);
    }
}

