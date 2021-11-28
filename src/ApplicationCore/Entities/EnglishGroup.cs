using ApplicationCore.Interfaces;
using Ardalis.GuardClauses;
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
            Guard.Against.NullOrEmpty(name, nameof(name));

            Name = name;
        }
    }
}
