using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Infrastructure.Data;
using Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntegrationsTests.Services
{
    public class TestService : IDisposable
    {
        public IEnglishWordService EnglishWordService => _englishWordService;
        public IEnglishGroupService EnglishGroupService => _englishGroupService;
        public IRepository<EnglishWord> WordRepository => _wordRepository;
        public IRepository<EnglishGroup> GroupRepository => _groupRepository;

        public TestService()
        {
            var dbName = Guid.NewGuid();

            var dbOptions = new DbContextOptionsBuilder<EnglishWordDbContext>()
            .UseInMemoryDatabase(dbName.ToString())
            .Options;
            _englishWordDbContext = new EnglishWordDbContext(dbOptions);

            _englishWordDbContext.Database.EnsureDeleted();

            _wordRepository = new Repository<EnglishWord>(_englishWordDbContext);
            _groupRepository = new Repository<EnglishGroup>(_englishWordDbContext);

            _englishWordService = new EnglishWordService(_groupRepository, _wordRepository);
            _englishGroupService = new EnglishGroupService(_groupRepository, _wordRepository);
        }

        public static EnglishWord GetEnglishWord(int index = 1)
        {
            var outIndex = index < 10 ? $"0{index}" : index.ToString();

            return new EnglishWord()
            {
                Phrase = $"{nameof(EnglishWord.Phrase)} {outIndex}",
                Transcription = $"{nameof(EnglishWord.Transcription)} {outIndex}",
                Translation = $"{nameof(EnglishWord.Translation)} {outIndex}",
                Example = $"{nameof(EnglishWord.Example)} {outIndex}",
                PictureUri = $"{nameof(EnglishWord.PictureUri)} {outIndex}"
            };
        }

        public static EnglishGroup GetEnglishGroup(int index = 1)
        {
            var outIndex = index < 10 ? $"0{index}" : index.ToString();

            return new EnglishGroup()
            {
                Name = $"{nameof(EnglishGroup.Name)}Group {outIndex}"
            };
        }

        public static List<EnglishWord> GetEnglishWordCollection()
        {
            var result = new List<EnglishWord>();
            
            EnglishWord englishWord;
            EnglishGroup englishGroup = GetEnglishGroup(1);
            for (int i = 1; i <= 26; i++)
            {
                if (i % 3 == 0)
                { 
                    englishGroup = GetEnglishGroup(i);
                }
                
                englishWord = GetEnglishWord(i);
                englishWord.EnglishGroup = englishGroup;
                
                result.Add(englishWord);
            }

            return result;
        }

        public async Task SetEnglishWordCollection()
        {
            var englishWordCollection = TestService.GetEnglishWordCollection();

            foreach (var englishWord in englishWordCollection)
            {
                if (englishWord.EnglishGroup.Id == 0)
                {
                    await EnglishGroupService.AddAsync(englishWord.EnglishGroup);
                }

                await EnglishWordService.AddAsync(englishWord);
            }
        }

        public void Dispose()
        {
            _englishWordDbContext = null;
        }

        private EnglishWordDbContext _englishWordDbContext;
        private readonly IEnglishGroupService _englishGroupService;
        private readonly IEnglishWordService _englishWordService;
        private readonly IRepository<EnglishWord> _wordRepository;
        private readonly IRepository<EnglishGroup> _groupRepository;
    }
}
