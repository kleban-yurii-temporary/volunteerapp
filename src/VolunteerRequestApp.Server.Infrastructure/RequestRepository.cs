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
    public class RequestRepository
    {
        private readonly DataContext dataContext;
        private readonly IMapper mapper;

        public RequestRepository(DataContext dataContext, IMapper mapper)
        {
            this.dataContext = dataContext;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<RequestReadDto>> GetListAsync()
        {
            return  mapper.Map<IEnumerable<RequestReadDto>>(await dataContext.Requests
                .Include(x => x.Status).Include(x=> x.Photos).Include(x=> x.Donations).ToListAsync());
        }

        public async Task<int> CreateAsync(RequestCreateDto obj)
        {
            var data = await dataContext.Requests.AddAsync(mapper.Map<Request>(obj));            
            await dataContext.SaveChangesAsync();
            dataContext.Requests.Find(data.Entity.Id).Status = await dataContext.Statuses.FirstAsync();
            await dataContext.SaveChangesAsync();
            return data.Entity.Id;
        }

        public async Task<RequestReadDto> GetAsync(int id)
        {
            return mapper.Map<RequestReadDto>(await dataContext.Requests.Include(x => x.Status).Include(x=> x.Donations).FirstAsync(x => x.Id == id));
        }

        public async Task<RequestUpdateDto> GetForEditAsync(int id)
        {
            var data = mapper.Map<RequestUpdateDto>(await dataContext.Requests.Include(x => x.Status).FirstAsync(x => x.Id == id));
            return data;
        }

        public async Task UpdateAsyncByField(int id, string field, string value)
        {
            var request = await dataContext.Requests.FindAsync(id);

            switch(field)
            {
                case "isfavorite":
                    request.IsFavorite = bool.Parse(value);
                    break;
                case "main_photo":
                    var photoId = int.Parse(value);
                    var photos = dataContext.Photos.Where(x => x.Request.Id == id);
                    foreach (var photo in photos)
                    {
                        photo.IsMain = photoId == photo.Id;
                    }
                    break;
            }

            await dataContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(RequestUpdateDto requestDto)
        {
            var requestUpd = mapper.Map<Request>(requestDto);
            var requestOrg = await dataContext.Requests.Include(x=> x.Status).FirstAsync(x=> x.Id == requestUpd.Id);

            if (requestOrg.Title != requestOrg.Title)
                requestOrg.Title = requestUpd.Title;

            if (requestOrg.Details != requestUpd.Details)
                requestOrg.Details = requestUpd.Details;

            if (requestOrg.NeedSum != requestUpd.NeedSum)
                requestOrg.NeedSum = requestUpd.NeedSum;

            if (requestOrg.OpenDate != requestUpd.OpenDate)
                requestOrg.OpenDate = requestUpd.OpenDate;

            if (requestOrg.CloseDate != requestUpd.CloseDate)
                requestOrg.CloseDate = requestUpd.CloseDate;

            if (requestOrg.Status.Id != requestUpd.Status.Id)
                requestOrg.Status = await dataContext.Statuses.FindAsync(requestUpd.Status.Id);

            await dataContext.SaveChangesAsync();

        }

        public async Task<IEnumerable<PhotoDto>> GetPhotosAsync(int id)
        {
            return mapper.Map<IEnumerable<PhotoDto>>(await dataContext.Photos
                .Where(x => x.Request.Id == id)
                .OrderByDescending(x=> x.Id)
                .ToListAsync());
        }

        public async Task<string> GetFrontPictureNameAsync(int id)
        {
            return (await dataContext.Photos.FirstAsync(x => x.Request.Id == id && x.IsMain)).Path;
        }

        public async Task AddPhotoAsync(int requestId, string newName)
        {
            var photo = new Photo
            {
                Request = await dataContext.Requests.FindAsync(requestId),
                Path = newName
            };

            if(!dataContext.Photos.Any(x=> x.Request.Id == requestId))            
                photo.IsMain = true;            

            await dataContext.Photos.AddAsync(photo);

            await dataContext.SaveChangesAsync();
        }

        public async Task RemovePhotoAsync(int id, int photoId)
        {
            dataContext.Photos.Remove(await dataContext.Photos.FirstAsync(x=> x.Id == photoId && x.Request.Id == id));
            await dataContext.SaveChangesAsync();
        }
    }
}
