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
        public FundaAanbodJob(ILogger<FundaAanbodJob> logger)
        {
            _logger = logger;
        }
        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Hello world!");
            ///TODO call service to geat all data and populate database
            return Task.CompletedTask;
        }

        
    }
}
