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
                .ForMember(d => d.Category, opt => opt.Condition(s => (s.Category is null == false)))
                .AfterMap((s, d) =>
                {
                    if ((s.Category is null == false) && (s.CategoryId != s.Category.Id))
                    {
                        d.Category = null;
                    }
                });


            CreateMap<CategoryBL, Category>()
                .ForMember(dest => dest.EnglishWords, opt => opt.Ignore());
        }
    }
}
