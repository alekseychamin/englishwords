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
                .BeforeMap((s, d) => d.Category = null)
                .ForMember(dest => dest.Category, opt => opt.Ignore());

            CreateMap<CategoryBL, Category>()
                .ForMember(dest => dest.EnglishWords, opt => opt.Ignore());
        }
    }
}
