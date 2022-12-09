using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using VolunteerRequestApp.Shared.Dtos;

namespace VolunteerRequestApp.Client.Infrastructure
{
    public class HttpStatusService : HttpBaseService
    {
        public HttpStatusService(HttpClient httpClient) : base(httpClient)
        {

        }

        public async Task<IEnumerable<StatusDto>> GetListAsync()
        {
            return await httpClient.GetFromJsonAsync<IEnumerable<StatusDto>>("/api/status");
        }
    }
}
