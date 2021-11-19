using ApplicationCore.Interfaces;
using System.Collections.Generic;

namespace ApplicationCore.Entities
{
    public class EnglishGroup : BaseEntity, IAggregateRoot
    {
        public string Name { get; set; }

        public ICollection<EnglishWord> EnglishWords { get; set; }
    }
}
