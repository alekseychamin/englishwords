using ApplicationCore.Entities;
using ApplicationCore.Specifications.Filter;
using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ApplicationCore.Specifications
{
    public class EnglishWordWithFilter : Specification<EnglishWord>
    {
        public EnglishWordWithFilter(EnglishWordFilter filter)
        {
            if (!filter.SearchingAllGroups)
            {
                Query
                .Where(x => x.EnglishGroupId == filter.EnglishGroupId);
            }

            FilterHelper.SearchByTerms(Query, x => x.Phrase, filter.Phrase, SearchType.Like);
            FilterHelper.SearchByTerms(Query, x => x.Transcription, filter.Transcription, SearchType.Like);
            FilterHelper.SearchByTerms(Query, x => x.Translation, filter.Translation, SearchType.Like);
            FilterHelper.SearchByTerms(Query, x => x.Example, filter.Example, SearchType.Like);

            if (!string.IsNullOrEmpty(filter.LessThanCreatedDate))
            {
                Query.Where(x => x.CreateDate.CompareTo(DateTime.Parse(filter.LessThanCreatedDate)) >= 0);
            }

            if (!string.IsNullOrEmpty(filter.MoreThanCreatedDate))
            {
                Query.Where(x => x.CreateDate.CompareTo(DateTime.Parse(filter.MoreThanCreatedDate)) <= 0);
            }

            if (filter.IsPagingEnabled)
            {
                Query.Skip(PaginationHelper.CalculateSkip(filter))
                     .Take(PaginationHelper.CalculateTake(filter));
            }

            Query
                .Include(x => x.EnglishGroup)
                .OrderBy(x => x.Phrase);
        }
    }
}
