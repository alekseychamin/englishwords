using ApplicationCore.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities
{
    public class EnglishGroup : BaseEntity, IAggregateRoot
    {
        public string Name { get; set; }
        
        public ICollection<EnglishWord> EnglishWords { get; set; }

        [NotMapped]
        public int CountWords { get; set; }
    }
}
