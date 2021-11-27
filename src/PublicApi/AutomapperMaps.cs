using ApplicationCore.Entities;
using ApplicationCore.Specifications.Filter;
using AutoMapper;
using PublicApi.Endpoints.EnglishGroups;
using PublicApi.Endpoints.EnglishWords;
using PublicApi.EnglishGroupEndpoints;
using PublicApi.Models;

namespace PublicApi
{
    public class AutomapperMaps : Profile
    {
        public AutomapperMaps()
        {
            CreateMap<EnglishWord, EnglishWordDto>();
            CreateMap<EnglishGroup, EnglishGroupDto>();
            
            CreateMap<EnglishGroup, GetByIdEnglishGroupResult>();
            CreateMap<EnglishWord, GetByIdEnglishWordResult>();

            CreateMap<BaseFilterDto, BaseFilter>().IncludeAllDerived().ReverseMap();
            CreateMap<EnglishGroupFilterRequest, EnglishGroupFilter>().ReverseMap();
            
            CreateMap<UpdateEnglishGroupRequest, UpdateEnglishGroupResult>();

            CreateMap<EnglishGroup, CreateEnglishGroupResult>();
        }
    }
}
