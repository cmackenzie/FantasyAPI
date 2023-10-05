using System.Threading.Channels;
using FantasyProcessor.Services.Models;

namespace FantasyProcessor.Services.Channels;

// In a more elaborate system, this internal memory channel would be replaced by some sort of external pub/sub platform 
public class ChannelService<T>
{
    public Channel<ChannelMessage<T>> ServiceChannel { get; }

    public ChannelService()
    {
        ServiceChannel = Channel.CreateBounded<ChannelMessage<T>>(new BoundedChannelOptions(1)
        {
            FullMode = BoundedChannelFullMode.DropOldest
        });
    }
}