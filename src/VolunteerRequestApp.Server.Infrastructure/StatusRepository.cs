using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteerRequestApp.Server.Core;
using VolunteerRequestApp.Shared.Dtos;

namespace VolunteerRequestApp.Server.Infrastructure
{
    public class StatusRepository
    {
        private readonly DataContext _ctx;
        private readonly IMapper _mapper;

        public StatusRepository(DataContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StatusDto>> GetListAsync()
        {
            return _mapper.Map<IEnumerable<StatusDto>>(await _ctx.Statuses.ToListAsync());
        }
    }
}
