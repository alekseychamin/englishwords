using AutoMapper;
using BusinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Model;

namespace WebAPI.Profiles
{
    public class WebProfile : Profile
    {
        public WebProfile()
        {
            //BL <-> View
            CreateMap<EnglishWordBL, EnglishWordView>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(source => source.Category.Name))
                .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(source => source.CreateDate.ToShortDateString()));
            CreateMap<EnglishWordView, EnglishWordBL>();

            CreateMap<CategoryBL, CategoryView>().ReverseMap();

            //Create <-> BL
            CreateMap<EnglishWordCreate, EnglishWordBL>().ReverseMap();
            CreateMap<CategoryCreate, CategoryBL>().ReverseMap();

            //Update <-> BL
            CreateMap<EnglishWordUpdate, EnglishWordBL>()
                .ForMember(dest => dest.CategoryId, opt => opt.Condition(source => source.CategoryId.HasValue))
                .ForMember(dest => dest.CreateDate, opt => opt.Condition(source => source.CreateDate.HasValue))
                .ForMember(dest => dest.Example, opt => opt.Condition(source => source.Example != null))
                .ForMember(dest => dest.ShowCount, opt => opt.Condition(source => source.ShowCount.HasValue))
                .ForMember(dest => dest.Transcription, opt => opt.Condition(source => source.Transcription != null))
                .ForMember(dest => dest.Translate, opt => opt.Condition(source => source.Translate != null))
                .ForMember(dest => dest.WordPhrase, opt => opt.Condition(source => source.WordPhrase != null));
            CreateMap<EnglishWordBL, EnglishWordUpdate>();
        }
    }
}
