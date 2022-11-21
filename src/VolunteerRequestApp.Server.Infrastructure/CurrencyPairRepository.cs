using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteerRequestApp.Server.Core;
using VolunteerRequestApp.Shared.Dtos.Currency;

namespace VolunteerRequestApp.Server.Infrastructure
{
    public class CurrencyPairRepository
    {
        private readonly IMapper mapper;
        private readonly DataContext dataContext;

        public CurrencyPairRepository(IMapper mapper, DataContext dataContext)
        {
            this.mapper = mapper;
            this.dataContext = dataContext;
        }

        public async Task<CurrencyPairReadDto> AddExchangeRateAsync(CurrencyPairCreateUpdateDto obj)
        {
            var pair = await dataContext.CurrencyPairs
                .FirstOrDefaultAsync(x => x.СurrencyFrom.ToLower() == obj.From.ToLower() && x.СurrencyTo.ToLower() == obj.To.ToLower());


            if (pair == null)
            {
                pair = dataContext.CurrencyPairs.Add(
                    new CurrencyPair
                    {
                        СurrencyFrom = obj.From,
                        СurrencyTo = obj.To
                    }).Entity;
                await dataContext.SaveChangesAsync();
            }

            await dataContext.ExchangeRates.AddAsync(new ExchangeRate
            {
                CurrencyPairId = pair.Id,
                Value = obj.CurrentValue                
            });

            await dataContext.SaveChangesAsync();

            return await GetItemAsync(obj.From, obj.To);
        }

        private async Task<CurrencyPair> getListByPair(string from, string to)
        {
            return await dataContext.CurrencyPairs.Include(x => x.Records)
                .FirstAsync(x => x.СurrencyFrom.ToLower() == from.ToLower() && x.СurrencyTo.ToLower() == to.ToLower());
        }

        public async Task<CurrencyPairHistroryReadDto> GetHistoryAsync(string from, string to)
        {
            return mapper.Map<CurrencyPairHistroryReadDto>(await getListByPair(from, to));
        }

        public async Task<CurrencyPairReadDto> GetItemAsync(string from, string to)
        {
            return mapper.Map<CurrencyPairReadDto>(await getListByPair(from, to));
        }

        public async Task<IEnumerable<CurrencyPairReadDto>> GetListAsync(bool activeOnly)
        {
            var data =  mapper.Map<IEnumerable<CurrencyPairReadDto>>(await dataContext
                .CurrencyPairs
                .Include(x => x.Records).ToListAsync());

            if (activeOnly)
                data = data.Where(x => x.IsActive).ToList();

            return data;
        }

        public async Task<bool> ChangeCurrencyPairStatus(int id)
        {
            var pair = await dataContext.CurrencyPairs.FindAsync(id);
            pair.IsActive = !pair.IsActive;
            await dataContext.SaveChangesAsync();
            return pair.IsActive;
        }
    }
}
