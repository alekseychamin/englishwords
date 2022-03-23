using ApplicationCore.Interfaces;
using System.Collections.Generic;

namespace ApplicationCore.Entities.Seeds
{
    public class EnglishWordSeed
    {
        public static List<EnglishGroup> SeedEnglishGroups(ISeedEnglihsGroup seed)
        {
            return seed.GetEnglishGroups();
        }
    }
}
