using AutoMapper;
using FullStackWebApp.DbContexts;
using FullStackWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore.Query;
using System.Net;

namespace FullStackWebApp
{
    public class FundaService
    {
        private readonly MainSqlServerDbContext _context;
        private readonly IMapper _mapper;
        private readonly HttpClient _client;

        private const string key = "ac1b0b1572524640a0ecc54de453ea9f";
        private const int pageSize = 25;
        private string url = "http://partnerapi.funda.nl/feeds/Aanbod.svc/json/{0}/?type={1}&zo={2}&page={3}&pagesize={4}";
        int maxAttempts = 3;
        public FundaService(HttpClient client, MainSqlServerDbContext context, IMapper mapper)
        {
            _client = client;
            _context = context;
            _mapper = mapper;
        }

        public string ResolveUrl(string type, string zo, int page)
        {
            return String.Format(url, key, type, zo, page, pageSize);
        }

        public async Task<bool> FetchDataAndPopulateDB<ResponseDTO>(MainSqlServerDbContext dbContext)
        {
            try
            {
                // fetch all data using url provided
                List<Aanbod> aanbods = await this.FetchData();
                if(aanbods == null)
                {
                    //@TODO log
                    return false;
                }
                
                // truncate table
                bool successfulDelete = await this.DeleteAllData(dbContext);
                if(!successfulDelete)
                {
                    //@TODO log
                    return false;
                }

                //populate database
                await dbContext.Aanbod.AddRangeAsync(aanbods);
                var numberOfItemsSaved = await dbContext.SaveChangesAsync();
                var success = numberOfItemsSaved > 0 ? true : false;
                return success;
            }
            catch (Exception)
            {
                //@TODO logger
                return false;
            }
        }

        private async Task<List<Aanbod>> FetchData()
        {
            List<Aanbod> allAanbod = await this.FetchAllPages("koop", "/amsterdam/");
            List<Aanbod> allAanbodWithTuin = await this.FetchAllPages("koop", "/amsterdam/tuin/");

            //iteration to set tuin to true where needed
            for (int i = 0; i < allAanbod.Count(); i++)
            {
                for (int j = 0; j < allAanbodWithTuin.Count(); j++)
                {
                    if (allAanbodWithTuin[j].Id.Equals(allAanbod[i].Id))
                    {
                        allAanbod[i].Tuin = true;
                    }
                }
            }
            return allAanbod;
        }

        private async Task<bool> DeleteAllData(MainSqlServerDbContext dbContext)
        {
            try
            {
                foreach (var entity in dbContext.Aanbod)
                    dbContext.Aanbod.Remove(entity);
                await dbContext.SaveChangesAsync();
                return true;
            } catch (Exception e)
            {
                //@TODO log
                return false;
            }
        }

        protected async Task<List<Aanbod>> FetchAllPages(string type, string zo)
        {

            List<AanbodDTO> objects = new List<AanbodDTO>();

            // Fetch total number of pages
            ResponseDTO firstPageData = await FetchFromPage(type, zo, 1);
            if (firstPageData == null || firstPageData.Paging == null)
            {
                //@TODO log
                return _mapper.Map<List<AanbodDTO>, List<Aanbod>>(objects);
            }
            for (int i = 1; i < firstPageData.Paging.AantalPaginas; i++) //total number of pages
            {
                ResponseDTO pageData = await FetchFromPage(type, zo, i);
                if(pageData != null)
                {
                    // @TODO Log
                    objects.AddRange(pageData.Objects);
                }
            }
            return _mapper.Map<List<AanbodDTO>, List<Aanbod>>(objects);
        }

        protected async Task<ResponseDTO> FetchFromPage(string type, string zo, int page)
        {
            string url = this.ResolveUrl(type, zo, page);
            int attemptCount = 1;

            var responseRaw = await _client.GetAsync(url);
            while (!responseRaw.IsSuccessStatusCode)
            {
                if (maxAttempts == attemptCount)
                {
                    // @TODO Log
                    return JsonConvert.DeserializeObject<ResponseDTO>(await responseRaw.Content.ReadAsStringAsync());
                }
                responseRaw = await _client.GetAsync(url);
                attemptCount += 1;
                // Wait 1 second before continuing
                Task.Delay(1000).Wait();
            }
            return JsonConvert.DeserializeObject<ResponseDTO>(await responseRaw.Content.ReadAsStringAsync());
        }

    }
}
