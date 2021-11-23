using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Specifications.Filter
{
    public class BaseFilter
    {
        public bool LoadChildren { get; set; } = false;
        public bool IsPagingEnabled { get; set; } = true;

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
