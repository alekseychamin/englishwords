using ApplicationCore.Entities.Dto;
using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities.Seeds
{
    public class EnglishWordSeed
    {
        public static List<EnglishGroup> Seed(ISeed seed)
        {
            return seed.Seed();
        }
    }
}
