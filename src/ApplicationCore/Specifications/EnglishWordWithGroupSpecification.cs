using ApplicationCore.Entities;
using Ardalis.Specification;

namespace ApplicationCore.Specifications
{
    public class EnglishWordWithGroupSpecification : Specification<EnglishWord>, ISingleResultSpecification
    {
        public EnglishWordWithGroupSpecification(int englishWordId)
        {
            Query
                .Where(x => x.Id == englishWordId)
                .Include(x => x.EnglishGroup);
        }
    }
}
