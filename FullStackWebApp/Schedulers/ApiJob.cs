using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullStackWebApp.Schedulers
{
    public class ApiJob : IJob
    {
        private readonly ILogger<ApiJob> _logger;
        public ApiJob(ILogger<ApiJob> logger)
        {
            _logger = logger;
        }
        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Hello world!");
            return Task.CompletedTask;
        }

        
    }
}
