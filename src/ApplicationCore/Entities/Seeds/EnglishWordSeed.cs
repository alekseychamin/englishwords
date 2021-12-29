using ApplicationCore.Interfaces;
using System.Collections.Generic;

namespace ApplicationCore.Entities.Seeds
{
    public class EnglishWordSeed
    {
        public static List<EnglishGroup> Seed(ISeed seed)
        {
            return seed.GetEnglishGroups();
        }
    }
}
