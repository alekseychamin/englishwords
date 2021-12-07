using ApplicationCore.Entities;
using ApplicationCore.Entities.Dto;
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

            CreateMap<CreateEnglishWordRequest, EnglishWordCoreDto>();
            CreateMap<UpdateEnglishWordRequest, EnglishWordCoreDto>();
            CreateMap<EnglishWordFilterRequest, EnglishWordFilter>().ReverseMap();
            CreateMap<EnglishWord, EnglishWordDto>();
            CreateMap<EnglishWord, GetByIdEnglishWordResult>();
            CreateMap<EnglishWord, CreateEnglishWordResult>();
            CreateMap<EnglishWord, UpdateEnglishWordResult>();

            CreateMap<CreateEnglishGroupRequest, EnglishGroupCoreDto>();
            CreateMap<UpdateEnglishGroupRequest, EnglishGroupCoreDto>();
            CreateMap<EnglishGroupFilterRequest, EnglishGroupFilter>().ReverseMap();
            CreateMap<EnglishGroup, EnglishGroupDto>();
            CreateMap<EnglishGroup, GetByIdEnglishGroupResult>();
            CreateMap<EnglishGroup, CreateEnglishGroupResult>();
            CreateMap<EnglishGroup, UpdateEnglishGroupResult>();
        }
    }
}
