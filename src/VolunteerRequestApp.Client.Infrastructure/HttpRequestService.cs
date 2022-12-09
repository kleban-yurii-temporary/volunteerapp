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
            return int.Parse(await msg.Content.ReadAsStringAsync());
        }

        public async Task<RequestReadDto> GetAsync(int id)
        {
            return await httpClient.GetFromJsonAsync<RequestReadDto>($"/api/requests/{id}");
        }

        public async Task<RequestUpdateDto> GetRequestForEditAsync(int id)
        {
            return await httpClient.GetFromJsonAsync<RequestUpdateDto>($"/api/requests/{id}/edit");
        }

        public async Task<IEnumerable<DonationReadDto>> GetDonationsAsync(int id, int n = 5)
        {
            return await httpClient.GetFromJsonAsync<IEnumerable<DonationReadDto>>($"/api/requests/{id}/donations/{n}");
        }

        public async Task<bool> CreateDonationAsync(int id, double amount, string userName)
        {
            var msg = await httpClient.PostAsJsonAsync($"/api/requests/{id}/donation", new DonationCreateDto
            {
                Amount = amount,
                UserName = userName
            });

            return await msg.Content.ReadFromJsonAsync<bool>();
        }

        public async Task<bool> ChangeFavStateAsync(int id, bool state)
        {
            await httpClient.PutAsJsonAsync($"/api/requests/{id}", new KeyValuePair<string, string>("isfavorite", state.ToString()));
            return state;
        }

        public async Task UpdateAsync(RequestUpdateDto request)
        {
            await httpClient.PutAsJsonAsync("/api/requests/", request);
        }

        public async Task<IEnumerable<PhotoDto>> GetPhotosAsync(int requestId)
        {
            return await httpClient.GetFromJsonAsync<IEnumerable<PhotoDto>>($"/api/requests/{requestId}/photos");
        }

        public async Task SetMainPhotoAsync(int requestId, int photoId)
        {
            await httpClient.PutAsJsonAsync($"/api/requests/{requestId}", new KeyValuePair<string, string>("main_photo", photoId.ToString()));
        }

        public async Task RemovePhotoAsync(int requestId, int photoId)
        {
            await httpClient.DeleteAsync($"/api/requests/{requestId}/photos/{photoId}");            
        }
    }
}
