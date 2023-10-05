using FantasyProcessor.Services.Channels;

namespace FantasyProcessor.Services;

public abstract class ConnectedService<T> : BackgroundService
{
    protected readonly ILogger<ConnectedService<T>> _logger;
    protected readonly ChannelService<T> _channelService;

    protected ConnectedService(ChannelService<T> channelService, ILogger<ConnectedService<T>> logger)
    {
        _logger = logger;
        _channelService = channelService;
    }
}