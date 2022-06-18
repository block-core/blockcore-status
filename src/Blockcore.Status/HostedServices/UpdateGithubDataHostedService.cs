using BlockcoreStatus.Services.Contracts;
using Common.Web.Core;

namespace BlockcoreStatus.HostedServices;

public class UpdateGithubDataHostedService : BackgroundService
{
    private readonly ILogger<UpdateGithubDataHostedService> _logger;
    private readonly IServiceProvider _serviceProvider;
    public UpdateGithubDataHostedService(IServiceProvider serviceProvider, ILogger<UpdateGithubDataHostedService> logger)
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
                    var _githubService = scope.ServiceProvider.GetRequiredService<IGithubService>();

                    var orgs = await _githubService.GetAllOrganizationFromDB();
                    foreach (var org in orgs)
                    {
                        await _githubService.UpdateOrganizationInDB(org.Login);
                        await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
                       
                        await _githubService.UpdateRepositoriesInDB(org.GithubOrganizationId);
                        await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
                       
                        await _githubService.UpdateLatestRepositoriesReleaseInDB(org.Login);
                        await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);

                    }

                    await Task.Delay(TimeSpan.FromHours(4), stoppingToken);
                }
            }
            catch (Exception ex)
            {
                _logger.LogErrorMessage(ex.Message);
            }

        }
    }
}
