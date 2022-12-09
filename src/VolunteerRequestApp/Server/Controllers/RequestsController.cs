using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mime;
using VolunteerRequestApp.Server.Core;
using VolunteerRequestApp.Server.Infrastructure;
using VolunteerRequestApp.Shared.Dtos.Request;

namespace VolunteerRequestApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly RequestRepository requestRepository;
        private readonly DonationRepository donationRepository;
        private readonly IWebHostEnvironment webHostEnvironment;

        public RequestsController(RequestRepository requestRepository, 
            DonationRepository donationRepository,
            IWebHostEnvironment webHostEnvironment)
        {
            this.requestRepository = requestRepository;
            this.donationRepository = donationRepository;
            this.webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IEnumerable<RequestReadDto>> GetListAsync()
        {
            return await requestRepository.GetListAsync();
        }

        [HttpGet("{id}")]
        public async Task<RequestReadDto> GetAsync(int id)
        {
            return await requestRepository.GetAsync(id);
        }

        [HttpPost]
        public async Task<int> CreateAsync(RequestCreateDto request)
        {
            return await requestRepository.CreateAsync(request);
        }

        [HttpGet("{id}/edit")]
        public async Task<RequestUpdateDto> GetForEditAsync(int id)
        {
            return await requestRepository.GetForEditAsync(id);
        }

        [HttpPut]
        public async Task UpdateAsync(RequestUpdateDto requestDto)
        {
            await requestRepository.UpdateAsync(requestDto);
        }

        [HttpPut("{id}")]
        public async Task UpdateAsyncByField(int id, KeyValuePair<string, string> kv)
        {
            await requestRepository.UpdateAsyncByField(id, kv.Key, kv.Value);
        }

        [HttpGet("{id}/donations/{n}")]
        public async Task<IEnumerable<DonationReadDto>> GetDonationsAsync(int id, int n = 5)
        {
            return (await donationRepository.GetListAsync(id)).Take(n);
        }

        [HttpPost("{id}/donation")]
        public async Task<bool> CreateDonationAsync(int id, DonationCreateDto donation)
        {
            return await donationRepository.CreateAsync(donation, id);
        }

        [HttpGet("{id}/photos")]
        public async Task<IEnumerable<PhotoDto>> GetPhotosAsync(int id)
        {
            return await requestRepository.GetPhotosAsync(id);
        }

        [HttpGet("{id}/photos/view")]
        public async Task<IActionResult> GetImageAsync(int id, int? photoId = null)
        {
            var request = await requestRepository.GetAsync(id);
            var photos = await requestRepository.GetPhotosAsync(id);

            string fileName = photoId is not null
                ? photos.First(x => x.Id == photoId).Path
                : (photos.Any()
                    ? photos.First(x => x.IsMain).Path
                    : $"no-picture.png");

            return await getPhotoFileByNameAsync(fileName);
        }

        [HttpPost("{id}/photos")]
        public async Task UploadPhotoAsync(int id, IList<IFormFile> UploadFiles)
        {
            try
            {
                var file = UploadFiles.First();
                var extension = new FileInfo(file.FileName).Extension;
                var newName = $"{Guid.NewGuid()}{extension}";
                var filePath = Path.Combine(webHostEnvironment.ContentRootPath, "assets", "requests", newName);

                using (FileStream fs = System.IO.File.Create(filePath))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                };

                await requestRepository.AddPhotoAsync(id, newName);
            }
            catch (Exception e)
            {
                Response.Clear();
                Response.StatusCode = 204;
                Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "File failed to upload";
                Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = e.Message;
            }
        }

        [HttpDelete("{id}/photos/{photoId}")]
        public async Task RemovePhotoAsync(int id, int photoId)
        {
            var photo = (await requestRepository.GetPhotosAsync(id)).First(x=> x.Id == photoId);
            await requestRepository.RemovePhotoAsync(id, photoId);
            var filePath = getPhotoPathByName(photo.Path);

            if (System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);
        }

        [NonAction]
        private async Task<IActionResult> getPhotoFileByNameAsync(string fileName)
        {
            var filePath = getPhotoPathByName(fileName);
            var contentType = getMimeTypeForFileExtension(filePath);
            return PhysicalFile(filePath, contentType);
        }

        [NonAction]
        private string getPhotoPathByName(string fileName)
        {
            return Path.Combine(webHostEnvironment.ContentRootPath, "assets", "requests", fileName);
        }

        [NonAction]
        private string getMimeTypeForFileExtension(string filePath)
        {
            const string DefaultContentType = "application/octet-stream";

            var provider = new FileExtensionContentTypeProvider();

            if (!provider.TryGetContentType(filePath, out string contentType))
                contentType = DefaultContentType;

            return contentType;
        }

    }
}
