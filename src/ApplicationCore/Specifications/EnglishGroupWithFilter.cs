using ApplicationCore.Entities;
using ApplicationCore.Specifications.Filter;
using Ardalis.Specification;

namespace ApplicationCore.Specifications
{
    public class EnglishGroupWithFilter : Specification<EnglishGroup>
    {
        public EnglishGroupWithFilter(EnglishGroupFilter filter)
        {
            FilterHelper.SearchByTerms(Query, x => x.Name, filter.Name, SearchType.Like);

            if (filter.IsPagingEnabled)
            {
                Query.Skip(PaginationHelper.CalculateSkip(filter))
                     .Take(PaginationHelper.CalculateTake(filter));
            }

            Query.OrderBy(x => x.Name);
        }
    }
}
