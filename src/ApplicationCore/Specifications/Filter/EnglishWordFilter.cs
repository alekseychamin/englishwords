using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Specifications.Filter
{
    public class EnglishWordFilter : BaseFilter
    {
        public int? EnglishGroupId { get; set; }

        public bool SearchingAllGroups { get; set; } = false;
        
        public string Phrase { get; set; }
        
        public string Transcription { get; set; }

        public string Translation { get; set; }

        public string Example { get; set; }

        public string LessThanCreatedDate { get; set; }

        public string MoreThanCreatedDate { get; set; }
    }
}
