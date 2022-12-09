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
    public class DonationRepository
    {
        private readonly DataContext dataContext;
        private readonly IMapper mapper;

        public DonationRepository(DataContext dataContext, IMapper mapper)
        {
            this.dataContext = dataContext;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<DonationReadDto>> GetListAsync(int requestId)
        {
            return mapper.Map<IEnumerable<DonationReadDto>>(await dataContext.Donations
                .Include(x=> x.Request)
                .Where(x=> x.Request.Id == requestId)
                .OrderByDescending(x=> x.Date)
                .ToListAsync());
        }

        private string[] userNames = { "Stalker08", "Waroir32", "Serius_1", "GoA32", "1st" };

        public async Task<bool> CreateAsync(DonationCreateDto dto, int requestId)
        {
            await dataContext.AddAsync(new Donation
            {
                Date = DateTime.Now,
                Request = await dataContext.Requests.FindAsync(requestId),
                Amount = dto.Amount,
                UserName = userNames[new Random().Next(1,6)]
            });

            await dataContext.SaveChangesAsync();

            return true;
        }
    }
}
