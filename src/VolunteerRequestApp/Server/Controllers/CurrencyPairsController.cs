using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VolunteerRequestApp.Server.Core;
using VolunteerRequestApp.Server.Infrastructure;
using VolunteerRequestApp.Server.Infrastructure.Helpers;
using VolunteerRequestApp.Shared.Dtos.Currency;

namespace VolunteerRequestApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyPairsController : ControllerBase
    {
        private readonly CurrencyPairRepository currencyPairRepository;
        private readonly CurrencyApiHelper currencyApiHelper;
        public CurrencyPairsController(CurrencyPairRepository currencyPairRepository, 
            CurrencyApiHelper currencyApiHelper)
        {
            this.currencyPairRepository = currencyPairRepository;
            this.currencyApiHelper = currencyApiHelper;
        }

        [HttpGet]
        public async Task<IEnumerable<CurrencyPairReadDto>> GetListAsync()
        {
            return await currencyPairRepository.GetListAsync();
        }

        /// <summary>
        /// Метод повертає інформацію про валютну пару з інформацією про найактуальніший курс
        /// </summary>
        /// <param name="from">Валюта 1</param>
        /// <param name="to">Валюта 2</param>
        /// <returns>Інформацію про валютну пару та найактуальніший курс</returns>
        [HttpGet("{from}/{to}")]
        public async Task<CurrencyPairReadDto> GetItemAsync(string from, string to)
        {
            return await currencyPairRepository.GetItemAsync(from, to);
        }

        /// <summary>
        /// Method returns currency pair and array of values
        /// </summary>
        /// <param name="from">currency 1</param>
        /// <param name="to">currency 2</param>
        /// <returns>array of currency changes</returns>
        [HttpGet("{from}/{to}/history")]
        public async Task<CurrencyPairHistroryReadDto> GetHistoryAsync(string from, string to)
        {
            return await currencyPairRepository.GetHistoryAsync(from, to);
        }      

        [HttpPost]
        public async Task<CurrencyPairReadDto> AddExchangeRateAsync(CurrencyPairCreateUpdateDto obj)
        {
            return await currencyPairRepository.AddExchangeRateAsync(obj);
        }

        [HttpGet("apiconfig")]
        public CurrencyApiConfig GetApiConfigAsync()
        {
            return currencyApiHelper.GetConfig();
        }
    }
}
