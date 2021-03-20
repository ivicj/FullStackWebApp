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

namespace FullStackWebApp
{
    public class Service
    {
        private readonly MainSqlServerDbContext _context;
        private readonly IMapper _mapper;
        private readonly HttpClient _client;
        private const string key = "ac1b0b1572524640a0ecc54de453ea9f";
        private string url = $"http://partnerapi.funda.nl/feeds/Aanbod.svc/json/{key}/?type=koop&zo=/amsterdam/tuin/&page=1&pagesize=25";
        ResponseData currentRes = new ResponseData();
        public Service(HttpClient client, MainSqlServerDbContext context, IMapper mapper)
        {
            _client = client;
            _context = context;
            _mapper = mapper;
        }

        public async Task<FullStackWebApp.Models.ResponseData> GetData<ResponseData>()
        {
            List<Aanbod> addAanbodList = new List<Aanbod>();
            List<Aanbod> updateAanbodList = new List<Aanbod>();
            List<Makelaar> makelaarList = new List<Makelaar>();

            //var response = await _client.GetStringAsync(path);
            //return response;
            try
            {
                var response = await _client.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<FullStackWebApp.Models.ResponseData>(null);
                }
                currentRes = JsonConvert.DeserializeObject<FullStackWebApp.Models.ResponseData>(await response.Content.ReadAsStringAsync());


                foreach (var item in currentRes.Objects)
                {
                    var aanbod = _mapper.Map<Aanbod>(item);

                    //TODO ADD TRANSACTION<-------------
                    var existingAanbod = await _context.Aanbod.FindAsync(aanbod.Id);
                    //var existingAanbod = _context.Set<Aanbod>().Include(x => x.Makelaar).FirstOrDefault();

                    if (existingAanbod == null)
                    { 
                        // add
                        addAanbodList.Add(aanbod);
                    } else
                    {
                        //TODO this works when existingAanbod.Makelaar.Id is not null
                        // update - if it is already in database
                        //if (existingAanbod.GUID != aanbod.GUID ||
                        //    existingAanbod.Makelaar.Id != aanbod.Makelaar.Id ||
                        //    existingAanbod.Makelaar.Name != aanbod.Makelaar.Name)
                        //{
                        //    updateAanbodList.Add(aanbod);

                        //}
                    }

                }

                await _context.Aanbod.AddRangeAsync(addAanbodList);
                //_context.Aanbod.UpdateRange(addAanbodList);
                var a = await _context.SaveChangesAsync();

                return currentRes;
            }
            catch (Exception e)
            {
                //_logger.LogError(e, $"Something went wrong when calling url");
                return JsonConvert.DeserializeObject<FullStackWebApp.Models.ResponseData>(null);
            }
        }

    }
}
