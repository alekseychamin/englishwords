using ApplicationCore.Entities;
using ApplicationCore.Specifications.Filter;
using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Specifications
{
    public class EnglishWordWithFilter : Specification<EnglishWord>
    {
        public EnglishWordWithFilter(EnglishWordFilter filter)
        {
            Query
                .Where(x => x.EnglishGroup.Id == filter.EnglishGroupId);

            if (!string.IsNullOrEmpty(filter.Phrase))
            {
                Query.Search(x => x.Phrase, "%" + filter.Phrase + "%");
            }
        }
    }
}
