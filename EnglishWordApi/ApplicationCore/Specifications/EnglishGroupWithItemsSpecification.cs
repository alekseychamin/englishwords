using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Specifications
{
    public class EnglishGroupWithItemsSpecification : Specification<EnglishGroup>, ISingleResultSpecification
    {
        public EnglishGroupWithItemsSpecification(int GroupId)
        {
            Query
                .Where(x => x.Id == GroupId)
                .Include(x => x.EnglishWords);
        }
    }
}
