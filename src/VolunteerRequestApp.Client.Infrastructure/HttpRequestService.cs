using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using VolunteerRequestApp.Shared.Dtos.Request;

namespace VolunteerRequestApp.Client.Infrastructure
{
    public class HttpRequestService : HttpBaseService
    {
        public HttpRequestService(HttpClient httpClient)
           : base(httpClient) { }

        public async Task<IEnumerable<RequestReadDto>> GetRequestsAsync()
        {
            return await httpClient.GetFromJsonAsync<IEnumerable<RequestReadDto>>("/api/requests");
        }

        public async Task<int> CreateRequestsAsync(RequestCreateDto request)
        {
            var msg = await httpClient.PostAsJsonAsync<RequestCreateDto>("/api/requests", request);
            return int.Parse(msg.Content.ToString());
        }
    }
}
