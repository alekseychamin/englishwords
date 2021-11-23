using ApplicationCore.Entities;
using ApplicationCore.Specifications.Filter;
using AutoMapper;
using PublicApi.Endpoints.EnglishGroups;
using PublicApi.Endpoints.EnglishWords;
using PublicApi.EnglishGroupEndpoints;
using PublicApi.Models;
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

            CreateMap<BaseFilterDto, BaseFilter>().IncludeAllDerived().ReverseMap();
            CreateMap<EnglishGroupFilterRequest, EnglishGroupFilter>().ReverseMap();
        }
    }
}
