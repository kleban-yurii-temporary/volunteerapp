using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VolunteerRequestApp.Server.Infrastructure;
using VolunteerRequestApp.Shared.Dtos;

namespace VolunteerRequestApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly StatusRepository statusRepository;

        public StatusController(StatusRepository statusRepository)
        {
            this.statusRepository = statusRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<StatusDto>> GetListAsync()
        {
            return await statusRepository.GetListAsync();
        }

        [HttpPost] 
        public async Task<int> Create(StatusDto obj)
        {
            return obj.Id * 3;
        }
    }
}
