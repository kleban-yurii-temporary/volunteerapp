using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System.Net.Mime;
using VolunteerRequestApp.Server.Infrastructure;
using VolunteerRequestApp.Shared.Dtos.Request;

namespace VolunteerRequestApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly RequestRepository requestRepository;
        private readonly IWebHostEnvironment webHostEnvironment;

        public RequestsController(RequestRepository requestRepository, 
            IWebHostEnvironment webHostEnvironment)
        {
            this.requestRepository = requestRepository;
            this.webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IEnumerable<RequestReadDto>> GetListAsync()
        {
            return await requestRepository.GetListAsync();
        }

        [HttpPost]
        public async Task<int> CreateAsync(RequestCreateDto request)
        {
            return await requestRepository.CreateAsync(request);
        }

        [HttpGet("image/{id}")]
        public async Task<IActionResult> Image(int id)
        {
            var request = await requestRepository.GetAsync(id);
            var filename = string.IsNullOrEmpty(request.FrontPicture)?"no-picture.png":request.FrontPicture;
            var filePath = Path.Combine(webHostEnvironment.ContentRootPath, "assets", "request", filename);
            var contentType = GetMimeTypeForFileExtension(filePath);

            if (!System.IO.File.Exists(filePath))
            {                
                filePath = Path.Combine(webHostEnvironment.ContentRootPath, "assets", "requests", filename);             }

            return PhysicalFile(filePath, contentType);
        }

        [NonAction]
        // mime type special!!!!
        public string GetMimeTypeForFileExtension(string filePath)
        {
            const string DefaultContentType = "application/octet-stream";

            var provider = new FileExtensionContentTypeProvider();

            if (!provider.TryGetContentType(filePath, out string contentType))
            {
                contentType = DefaultContentType;
            }

            return contentType;
        }
    }
}
