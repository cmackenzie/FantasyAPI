using FantasyProcessor.Services;
using FantasyProcessor.Services.Channels;
using FantasyProcessor.Services.Persisting;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddSingleton<FootballChannelService>();
        services.AddSingleton<BasketballChannelService>();
        services.AddSingleton<BaseballChannelService>();

        services.AddHostedService<BaseballPollingService>();
        services.AddHostedService<FootballPollingService>();
        services.AddHostedService<BasketabllPollingService>();

        services.AddHostedService<FootballPersistingService>();
        services.AddHostedService<BaseballPersistingService>();
        services.AddHostedService<BasketballPersistingService>();
    })
    .Build();

host.Run();