using ApplicationCore.Entities;
using AutoMapper;
using PublicApi.Endpoints.EnglishGroups;
using PublicApi.Endpoints.EnglishWords;
using PublicApi.EnglishGroupEndpoints;
using PublicApi.Models.EnglishWords;

namespace PublicApi
{
    public class AutomapperMaps : Profile
    {
        public AutomapperMaps()
        {
            CreateMap<EnglishWordDto, EnglishWord>().ReverseMap();
            CreateMap<EnglishGroupDto, EnglishGroup>().ReverseMap();
            
            CreateMap<EnglishGroup, GetByIdEnglishGroupResult>();
            CreateMap<EnglishWord, GetByIdEnglishWordResult>();
        }
    }
}
