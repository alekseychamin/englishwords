using AutoMapper;
using BusinessLogic.Model;
using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Profiles
{
    public class BLProfile : Profile
    {
        public BLProfile()
        {
            //DAL -> BL
            CreateMap<EnglishWord, EnglishWordBL>();
            CreateMap<Category, CategoryBL>();

            //BL -> DAL
            CreateMap<EnglishWordBL, EnglishWord>()
                .ForMember(dest => dest.Category, opt => opt.Condition(source => source.Category != null));

            CreateMap<CategoryBL, Category>();
        }
    }
}
