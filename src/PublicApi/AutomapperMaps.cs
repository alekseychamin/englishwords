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
            CreateMap<BaseFilterDto, BaseFilter>().IncludeAllDerived().ReverseMap();

            CreateMap<CreateEnglishWordRequest, EnglishWord>();
            CreateMap<UpdateEnglishWordRequest, EnglishWord>();
            CreateMap<EnglishWordFilterRequest, EnglishWordFilter>().ReverseMap();
            CreateMap<EnglishWord, EnglishWordDto>();
            CreateMap<EnglishWord, GetByIdEnglishWordResult>();
            CreateMap<EnglishWord, CreateEnglishWordResult>();
            CreateMap<EnglishWord, UpdateEnglishWordResult>();

            CreateMap<CreateEnglishGroupRequest, EnglishGroup>();
            CreateMap<UpdateEnglishGroupRequest, EnglishGroup>();
            CreateMap<EnglishGroupFilterRequest, EnglishGroupFilter>().ReverseMap();
            CreateMap<EnglishGroup, EnglishGroupDto>();
            CreateMap<EnglishGroup, GetByIdEnglishGroupResult>();
            CreateMap<EnglishGroup, CreateEnglishGroupResult>();
            CreateMap<EnglishGroup, UpdateEnglishGroupResult>();
        }
    }
}
