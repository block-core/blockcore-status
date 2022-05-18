namespace blockcore.status.HostedServices;

public class UpdateGithubDataHostedService : BackgroundService
{
    private readonly ILogger<UpdateGithubDataHostedService> _logger;

    public UpdateGithubDataHostedService(ILogger<UpdateGithubDataHostedService> logger)
    {
        _logger = logger;

    }
    protected  override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        { 
            //TODO

            await Task.Delay(TimeSpan.FromHours(4), stoppingToken);
        }
    }
}
