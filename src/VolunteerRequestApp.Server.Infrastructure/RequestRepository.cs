using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteerRequestApp.Server.Core;
using VolunteerRequestApp.Shared.Dtos.Request;

namespace VolunteerRequestApp.Server.Infrastructure
{
    public class RequestRepository
    {
        private readonly DataContext dataContext;
        private readonly IMapper mapper;
        public RequestRepository(DataContext dataContext, IMapper mapper)
        {
            this.dataContext = dataContext;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<RequestReadDto>> GetListAsync()
        {
            return  mapper.Map<IEnumerable<RequestReadDto>>(await dataContext.Requests
                .Include(x => x.Status).ToListAsync());
        }

        public async Task<int> CreateAsync(RequestCreateDto obj)
        {
            var data = await dataContext.Requests.AddAsync(mapper.Map<Request>(obj));            
            await dataContext.SaveChangesAsync();
            dataContext.Requests.Find(data.Entity.Id).Status = await dataContext.Statuses.FirstAsync();
            await dataContext.SaveChangesAsync();
            return data.Entity.Id;
        }

        public async Task<RequestReadDto> GetAsync(int id)
        {
            return mapper.Map<RequestReadDto>(await dataContext.Requests.FirstAsync(x => x.Id == id));
        }
    }
}
