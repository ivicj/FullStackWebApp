using FullStackWebApp.Models;
using Microsoft.AspNetCore.Mvc;
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
        private readonly HttpClient _client;
        private const string key = "ac1b0b1572524640a0ecc54de453ea9f";
        private string url = $"http://partnerapi.funda.nl/feeds/Aanbod.svc/json/{key}/?type=koop&zo=/amsterdam/tuin/&page=1&pagesize=25";

        public Service(HttpClient client)
        {
            _client = client;    
        }

        public async Task<T> GetData<T>()
        {
            //var response = await _client.GetStringAsync(path);
            //return response;
            try
            {
                var response = await _client.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<T>(null);
                }
                var currentRes = JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());

                return currentRes;
            }
            catch (Exception e)
            {
                //_logger.LogError(e, $"Something went wrong when calling url");
                return JsonConvert.DeserializeObject<T>(null);
            }
        }

    }
}
