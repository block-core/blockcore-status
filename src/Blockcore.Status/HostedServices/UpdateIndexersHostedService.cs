using BlockcoreStatus.Services.Contracts;
using Common.Web.Core;

namespace BlockcoreStatus.HostedServices;

public class UpdateIndexersHostedService : BackgroundService
{
    private readonly ILogger<UpdateGithubDataHostedService> _logger;
    private readonly IServiceProvider _serviceProvider;
    public UpdateIndexersHostedService(IServiceProvider serviceProvider, ILogger<UpdateGithubDataHostedService> logger)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var indexersService = scope.ServiceProvider.GetRequiredService<IBlockcoreIndexersService>();

                    //Update status and Ip location

                    await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken);
                }
            }
            catch (Exception ex)
            {
                _logger.LogErrorMessage(ex.Message);
            }

        }
    }
}
