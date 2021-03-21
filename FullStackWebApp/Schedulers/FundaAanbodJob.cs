using FullStackWebApp.DbContexts;
using FullStackWebApp.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullStackWebApp.Schedulers
{
    public class FundaAanbodJob : IJob
    {
        private readonly ILogger<FundaAanbodJob> _logger;
        private readonly IServiceProvider _provider;
        public FundaAanbodJob(ILogger<FundaAanbodJob> logger, IServiceProvider provider)
        {
            _logger = logger;
            _provider = provider;
        }
        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Hello world!");
            using (var scope = _provider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<MainSqlServerDbContext>();
                var service = scope.ServiceProvider.GetService<FundaService>();
                // fetch makelaars, update DB
                //call service to get all data and populate database
                var res = service.FetchDataAndPopulateDB<bool>(dbContext);

                if (res == null)
                {
                    return null;
                }
            }

            return Task.CompletedTask;
        }

        
    }
}
