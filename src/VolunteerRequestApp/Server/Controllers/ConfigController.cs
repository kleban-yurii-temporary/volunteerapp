using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VolunteerRequestApp.Server.Infrastructure.Helpers;
using VolunteerRequestApp.Shared.Dtos.Currency;

namespace VolunteerRequestApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigController : ControllerBase
    {
        private readonly CurrencyApiHelper currencyApiHelper;

        public ConfigController(CurrencyApiHelper currencyApiHelper)
        {
            this.currencyApiHelper = currencyApiHelper;
        }

        [HttpGet("currency")]
        public CurrencyApiConfig GetCurrencyApi()
        {
            return currencyApiHelper.GetConfig();
        }

        [HttpGet("currency/{from}/{to}/latest")]
        public string GetCurrencyApiLatest(string from, string to)
        {
            return currencyApiHelper.GetLatestUrl(from, to);
        }

        [HttpGet("currency/list")]
        public async Task<Dictionary<string, string>> GetCurrencyApiList()
        {
            Dictionary<string, string> keyValuePairs;
            try
            {
                var url = currencyApiHelper.GetCurrenciesListUrl();
                HttpClient client = new HttpClient();
                var response = await client.GetAsync(url);
                keyValuePairs = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
            } 
            catch
            {
                keyValuePairs = new Dictionary<string, string>();
                keyValuePairs.Add("USD", "US Dollar");
                keyValuePairs.Add("EUR", "Euro");
            }

            return keyValuePairs;
            
        }

        [HttpGet("currency/{name}")]
        public async Task<CurrencyPairReadDto> GetCurrencyRate(string name)
        {
            var config = currencyApiHelper.GetConfig();
            name = name.Trim().ToUpper();
            LatestCurrencyData latest;

            try
            {
                var url = currencyApiHelper.GetLatestUrl(config.SecondaryCurrency, name);
                HttpClient client = new HttpClient();
                var response = await client.GetAsync(url);
                latest = await response.Content.ReadFromJsonAsync<LatestCurrencyData>();
            }
            catch
            {
                latest = new LatestCurrencyData
                {
                    Base = "USD",
                    Timestamp = 0,
                    Disclaimer = "",
                    License = "",
                    Rates = new Dictionary<string, decimal>(new List<KeyValuePair<string, decimal>>
                    {
                        new KeyValuePair<string, decimal>(name, (decimal)new Random().NextDouble()),
                        new KeyValuePair<string, decimal>("UAH", 1m),
                    })
                };
            }

            return new CurrencyPairReadDto
            {
                From = config.SecondaryCurrency,
                To = name,
                CurrentValue = (double)Math.Round(latest.Rates[name]/latest.Rates[config.SecondaryCurrency], 6)
            };

        }
    }
}
