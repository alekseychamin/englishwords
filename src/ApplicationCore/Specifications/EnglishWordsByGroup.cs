using ApplicationCore.Entities;
using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Specifications
{
    public class EnglishWordsByGroup : Specification<EnglishWord>, ISingleResultSpecification
    {
        public EnglishWordsByGroup(int englishGroupId)
        {
            Query
                .Where(x => x.EnglishGroupId == englishGroupId);
        }
    }
}
