using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.Hosting;
using CaveCore.Services;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace CaveServer.Services
{
    public class TrendingRefreshService : BackgroundService
    {
        private readonly IServiceProvider _provider;
        public TrendingRefreshService(IServiceProvider provider)
        {
            _provider = provider;
        }
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var serviceScope = _provider.CreateScope())
                {
                    var trendingService = serviceScope.ServiceProvider.GetService<ITrendingService>();
                    var isDone = false;
                    do{
                        isDone = await trendingService.ReCalculatePoint();
                    }
                    while (!isDone);       
                    
                    await Task.Delay(AppConstant.schedulerInterval, stoppingToken);
                }
            }
        }
    }
}