using FantasyCore;
using FantasyProcessor.APIs.CBS;
using FantasyProcessor.Services.Models;
using FantasyProcessor.Services.Channels;

namespace FantasyProcessor.Services;

public class BasketabllPollingService : ScheduledPollingService
{
    public BasketabllPollingService(BasketballChannelService channelService, ILogger<BasketabllPollingService> logger)
        :base(channelService, logger) { }

    protected override async Task RunAsync(CancellationToken stoppingToken)
    {   
        CBSApi cbsApi = new CBSApi(Constants.BASKETBALL);
        var response = await cbsApi.GetFullPlayerList();

        ChannelMessage<List<Player>> message = new ChannelMessage<List<Player>>(Constants.BASKETBALL, response);
        await _channelService.ServiceChannel.Writer.WriteAsync(message);
    }
}

