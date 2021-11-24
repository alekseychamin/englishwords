using ApplicationCore.Entities;
using ApplicationCore.Specifications.Filter;
using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Specifications
{
    public class EnglishGroupWithFilter : Specification<EnglishGroup>
    {
        public EnglishGroupWithFilter(EnglishGroupFilter filter)
        {
            if (!string.IsNullOrEmpty(filter.Name))
            {
                Query.Search(x => x.Name, "%" + filter.Name + "%");
            }

            if (filter.IsPagingEnabled)
            {
                Query.Skip(PaginationHelper.CalculateSkip(filter))
                     .Take(PaginationHelper.CalculateTake(filter));
            }

            Query.OrderBy(x => x.Name);
        }
    }
}
