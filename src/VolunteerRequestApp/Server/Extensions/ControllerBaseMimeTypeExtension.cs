using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace VolunteerRequestApp.Server.Extensions
{
    public static class ControllerBaseMimeTypeExtension
    {
        public static string GetMimeType(this ControllerBase controller, string filePath)
        {
            var provider = new FileExtensionContentTypeProvider();

            if (!provider.TryGetContentType(filePath, out string contentType))            
                contentType = "application/octet-stream";            

            return contentType;
        }
    }
}
