using ApplicationCore.Entities.Dto;
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

        public EnglishGroup(EnglishGroupCoreDto englishGroup)
        {
            Guard.Against.NullOrEmpty(englishGroup.Name, nameof(englishGroup.Name));

            Name = englishGroup.Name;
        }

        public void AddItem(EnglishWord englishWord)
        {
            _englishWords.Add(englishWord);
        }

        public void Update(EnglishGroupCoreDto englishGroup)
        {
            Guard.Against.NullOrEmpty(englishGroup.Name, nameof(englishGroup.Name));

            Name = englishGroup.Name;
        }
    }
}
