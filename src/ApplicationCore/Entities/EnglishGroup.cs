using ApplicationCore.Entities.Dto;
using ApplicationCore.Interfaces;
using Ardalis.GuardClauses;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ApplicationCore.Entities
{
    public class EnglishGroup : BaseEntity<EnglishGroupCoreDto>, IAggregateRoot
    {
        private readonly List<EnglishWord> _englishWords = new List<EnglishWord>();
        
        public string Name { get; private set; }
        
        public IReadOnlyCollection<EnglishWord> EnglishWords => _englishWords.AsReadOnly();

        private EnglishGroup()
        {

        }

        public EnglishGroup(EnglishGroupCoreDto entityDto)
        {
            Guard.Against.NullOrEmpty(entityDto.Name, nameof(entityDto.Name));

            SetProperties(entityDto);
        }

        public void AddItem(EnglishWord englishWord)
        {
            _englishWords.Add(englishWord);
        }

        public override void Update(EnglishGroupCoreDto entityDto)
        {
            Guard.Against.NullOrEmpty(entityDto.Name, nameof(entityDto.Name));

            SetProperties(entityDto);
        }

        protected override void SetProperties(EnglishGroupCoreDto entityDto)
        {
            Name = entityDto.Name;
        }
    }
}
