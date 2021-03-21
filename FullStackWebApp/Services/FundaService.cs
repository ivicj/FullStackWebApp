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
                // pokupi sve objekte iz amsterdama bez obzira dal imaju bastu ili ne
                List<Aanbod> allAanbod = await this.FetchAllPages("koop", "/amsterdam/");
                // pokupi sve objekte iz amsterdama sa bastom
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

                // Truncate table
                await this.DeleteAllData(dbContext);

                //populate database
                await dbContext.Aanbod.AddRangeAsync(allAanbod);
                var numberOfItemsSaved = await dbContext.SaveChangesAsync();
                var success = numberOfItemsSaved > 0 ? true : false;
                return success;
            }
            catch (Exception)
            {
                //@TODO logger
                throw;
            }
        }

        private async Task DeleteAllData(MainSqlServerDbContext dbContext)
        {
            foreach (var entity in dbContext.Aanbod)
                dbContext.Aanbod.Remove(entity);
            await dbContext.SaveChangesAsync();
        }

        protected async Task<List<Aanbod>> FetchAllPages(string type, string zo)
        {

            List<AanbodDTO> objects = new List<AanbodDTO>();

            // Fetch total number of pages
            // @TODO Handle errors such as page not found (probaj da stavis da dohvati stranu 100)
            ResponseDTO firstPageData = await FetchFromPage(type, zo, 1);
            if (firstPageData == null)
            {
                //@TODO log error Null ref ex retry
                return _mapper.Map<List<AanbodDTO>, List<Aanbod>>(objects);
            }
            for (int i = 1; i < firstPageData.Paging.AantalPaginas; i++) //total number of pages
            {
                ResponseDTO pageData = await FetchFromPage(type, zo, i);
                if(pageData != null)
                {
                    objects.AddRange(pageData.Objects);
                }
            }
            return _mapper.Map<List<AanbodDTO>, List<Aanbod>>(objects);
        }


        protected async Task<ResponseDTO> FetchFromPage(string type, string zo, int page)
        {
            string url = this.ResolveUrl(type, zo, page);

            var responseRaw = await _client.GetAsync(url);
            if (!responseRaw.IsSuccessStatusCode)
            {
                // @TODO
                // Pause 1 second and retry
                // Log info
                if (responseRaw.StatusCode.Equals(HttpStatusCode.Unauthorized))
                {
                        return JsonConvert.DeserializeObject<ResponseDTO>(await responseRaw.Content.ReadAsStringAsync());
                }
                return JsonConvert.DeserializeObject<ResponseDTO>(await responseRaw.Content.ReadAsStringAsync());
            }

            return JsonConvert.DeserializeObject<ResponseDTO>(await responseRaw.Content.ReadAsStringAsync());
        }

    }
}
