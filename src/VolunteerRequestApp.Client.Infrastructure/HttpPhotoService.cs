using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using VolunteerRequestApp.Shared.Dtos.Request;

namespace VolunteerRequestApp.Client.Infrastructure
{
    public class HttpPhotoService : HttpBaseService
    {
        public HttpPhotoService(HttpClient httpClient) 
            : base(httpClient) { }
       
        


    }
}
