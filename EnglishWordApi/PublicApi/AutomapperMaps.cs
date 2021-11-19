using ApplicationCore.Entities;
using AutoMapper;
using PublicApi.EnglishGroupEndpoints;
using PublicApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicApi
{
    public class AutomapperMaps : Profile
    {
        public AutomapperMaps()
        {
            CreateMap<EnglishWordDto, EnglishWord>().ReverseMap();
            CreateMap<EnglishGroup, GetByIdEnglishGroupResult>();
                //.ForMember(res => res.EnglishWords, options => options.MapFrom(src => src.EnglishWords.ToList()));
        }
    }
}
