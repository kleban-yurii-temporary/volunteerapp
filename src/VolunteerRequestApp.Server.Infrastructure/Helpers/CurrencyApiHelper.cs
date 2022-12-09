using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteerRequestApp.Shared.Dtos.Currency;

namespace VolunteerRequestApp.Server.Infrastructure.Helpers
{
    public class CurrencyApiHelper
    {
        private readonly IConfiguration configuration;
        private readonly CurrencyApiConfig configData;

        public CurrencyApiHelper(IConfiguration configuration)
        {
            this.configuration = configuration;
            configData = new CurrencyApiConfig
            {
                ApiKey = configuration["CurrencyApi:Key"],
                ApiUrl = configuration["CurrencyApi:Url"],
                BaseCurrency = configuration["CurrencyApi:Base"],
                SecondaryCurrency = configuration["CurrencyApi:Secondary"]
            };
        }

        public CurrencyApiConfig GetConfig()
        {
            return configData;
        }

        public string GetLatestUrl(params string[] currencies)
        {
            return $"{configData.ApiUrl}/latest.json?app_id={configData.ApiKey}&base={configData.BaseCurrency}&symbols={string.Join(",", currencies)}";
        }

        public string GetCurrenciesListUrl()
        {
            return $"{configData.ApiUrl}/currencies.json";
        }

    }
}
