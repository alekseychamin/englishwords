using ApplicationCore.Entities;
using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ApplicationCore.Specifications
{
    public enum SearchType
    {
        Like = 0
    }

    public static class FilterHelper
    {
        private static readonly Dictionary<SearchType, Func<string, string>> processSearchTerms =
            new Dictionary<SearchType, Func<string, string>>()
            {
                { SearchType.Like, (s) => { return $"%{s}%"; } }
            };
        
        public static void SearchByTerms<T>(ISpecificationBuilder<T> spec, Expression<Func<T, string>> selector, 
            string searchTerm, SearchType type) where T : BaseEntity
        {
            if (!string.IsNullOrEmpty(searchTerm))
            {
                spec.Search<T>(selector, processSearchTerms[type](searchTerm));
            }
        }
    }
}
