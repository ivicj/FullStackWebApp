using FullStackWebApp.Models;
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
        private FundaService _service;
        public FundaAanbodJob(ILogger<FundaAanbodJob> logger, FundaService service)
        {
            _logger = logger;
            _service = service;
        }
        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Hello world!");
            
            //call service to get all data and populate database
            var res = _service.FetchDataAndPopulateDB<bool>();

            if (res == null)
            {
                return null;
            }

            return Task.CompletedTask;
        }

        
    }
}
