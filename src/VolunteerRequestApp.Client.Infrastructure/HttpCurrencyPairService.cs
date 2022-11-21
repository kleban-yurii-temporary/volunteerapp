using System.Net.Http.Json;
using VolunteerRequestApp.Shared.Dtos.Currency;

namespace VolunteerRequestApp.Client.Infrastructure
{
    public class HttpCurrencyPairService : HttpBaseService
    {
        public HttpCurrencyPairService(HttpClient httpClient)
            : base(httpClient) { }

        public async Task<Dictionary<string, string>> GetCurrenciesListAsync()
        {
            return await httpClient.GetFromJsonAsync<Dictionary<string, string>>("/api/config/currency/list");
        }

        public async Task<CurrencyPairReadDto> GetCurrencyRateAsync(string name)
        {
            return await httpClient.GetFromJsonAsync<CurrencyPairReadDto>($"/api/config/currency/{name}");
        }

        public async Task<CurrencyApiConfig> GetApiConfigAsync()
        {
            return await httpClient.GetFromJsonAsync<CurrencyApiConfig>("/api/config/currency");
        }

        public async Task<IEnumerable<CurrencyPairReadDto>> GetCurrencyPairsListAsync(bool activeOnly = false)
        {
            return await httpClient.GetFromJsonAsync<IEnumerable<CurrencyPairReadDto>>($"/api/currencypairs?activeOnly={activeOnly}");
        }

        public async Task<CurrencyPairHistroryReadDto> GetHistoryAsync(string from, string to)
        {
            return await httpClient.GetFromJsonAsync<CurrencyPairHistroryReadDto>($"/api/currencypairs/{from}/{to}/history");
        }

        public async Task<bool> ChangeCurrencyPairStatusAsync(int id)
        {
            var response = await httpClient.PostAsJsonAsync($"/api/currencypairs/changestatus/{id}", "");
            return bool.Parse(await response.Content.ReadAsStringAsync());
        }

        public async Task<CurrencyPairReadDto> SaveCurrencyPairDataAsync(CurrencyPairCreateUpdateDto obj)
        {
            var response = await httpClient.PostAsJsonAsync<CurrencyPairCreateUpdateDto>($"/api/currencypairs", obj);
            return await response.Content.ReadFromJsonAsync<CurrencyPairReadDto>();
        }
    }
}