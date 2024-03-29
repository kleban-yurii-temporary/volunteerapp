﻿using AutoMapper;
using VolunteerRequestApp.Server.Core;
using VolunteerRequestApp.Shared.Dtos;
using VolunteerRequestApp.Shared.Dtos.Currency;
using VolunteerRequestApp.Shared.Dtos.Request;

namespace VolunteerRequestApp.Server.Infrastructure.Helpers
{
    public class AppAutoMapper : Profile
    {
        public AppAutoMapper()
        {
            CreateMap<StatusDto, Status>();
            CreateMap<Status, StatusDto>();

            CreateMap<CurrencyPair, CurrencyPairReadDto>()
                .ForMember(dest => dest.From, opt => opt.MapFrom(c => c.СurrencyFrom))
                .ForMember(dest => dest.To, opt => opt.MapFrom(c => c.СurrencyTo))
                .ForMember(dest => dest.CurrentValue, opt => opt.MapFrom(c => c.Records.MaxBy(x => x.CreatedOn).Value));

            CreateMap<ExchangeRate, ExchangeRateReadDto>();

            CreateMap<CurrencyPair, CurrencyPairHistroryReadDto>()
                .ForMember(dest => dest.From, opt => opt.MapFrom(c => c.СurrencyFrom))
                .ForMember(dest => dest.To, opt => opt.MapFrom(c => c.СurrencyTo))
                .ForMember(dest => dest.Records, opt => opt.MapFrom(c => c.Records.OrderByDescending(x => x.CreatedOn))); ;

            CreateMap<RequestCreateDto, Request>();
            CreateMap<Request, RequestReadDto>()
                .ForMember(dest => dest.CurrentSum, opt => opt.MapFrom( r=> r.Donations.Sum(x=> x.Amount)))
                .ForMember(dest => dest.HasFrontPicture, opt => opt.MapFrom(r => r.Photos.Any(x=> x.IsMain)));

            CreateMap<Request, RequestUpdateDto>();
            CreateMap<RequestUpdateDto, Request>();

            CreateMap<Donation, DonationReadDto>();
            CreateMap<DonationCreateDto, Donation>();

            CreateMap<Photo, PhotoDto>();
        }
    }
}