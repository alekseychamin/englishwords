using ApplicationCore.Entities;
using Ardalis.Specification;

namespace ApplicationCore.Specifications
{
    public class EnglishWordWithGroup : Specification<EnglishWord>, ISingleResultSpecification
    {
        public EnglishWordWithGroup(int englishWordId)
        {
            Query
                .Where(x => x.Id == englishWordId)
                .Include(x => x.EnglishGroup);
        }
    }
}
