using ApplicationCore.Interfaces;
using System.Collections.Generic;

namespace ApplicationCore.Entities
{
    public class EnglishGroup : BaseEntity, IAggregateRoot
    {
        public string Name { get; private set; }

        private readonly List<EnglishWord> _englishWords = new List<EnglishWord>();
        public IReadOnlyCollection<EnglishWord> EnglishWords => _englishWords.AsReadOnly();

        private EnglishGroup()
        {

        }

        public EnglishGroup(string name)
        {
            Name = name;
        }

        public void AddItem(EnglishWord englishWord)
        {
            _englishWords.Add(englishWord);
        }

        public void Update(string name)
        {
            Name = name;
        }
    }
}
