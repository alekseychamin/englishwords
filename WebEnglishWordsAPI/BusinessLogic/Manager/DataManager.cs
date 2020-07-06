using DataAccess.EF;
using Microsoft.EntityFrameworkCore;
using BusinessLogic.ReadCSV;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using BusinessLogic.Validations;
using DataAccess.Model;
using BusinessLogic.Repository;
using BusinessLogic.Model;
using AutoMapper;

namespace BusinessLogic.Manager
{
    public class DataManager : IDataManager
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<DataManager> _logger;
        private readonly ISeedData _seedData;
        private readonly IRepositoryBL<EnglishWordBL> _englishWordRepositoryBL;
        private readonly IRepositoryBL<CategoryBL> _categoryRepositoryBL;
        private readonly IRuleUniqueValidation<EnglishWordBL> _ruleUniqueEnglishWord;
        private readonly IRuleUniqueValidation<CategoryBL> _ruleUniqueCategory;
        private readonly IMapper _mapper;
        private readonly IReadCSVFile _readCSVFile;

        public DataManager(IConfiguration configuration, ILogger<DataManager> logger,
                           ISeedData seedData, IRepositoryBL<EnglishWordBL> englishWordRepositoryBL,
                           IRepositoryBL<CategoryBL> categoryRepositoryBL,
                           IRuleUniqueValidation<EnglishWordBL> ruleUniqueEnglishWord,
                           IRuleUniqueValidation<CategoryBL> ruleUniqueCategory,
                           IMapper mapper, IReadCSVFile readCSVFile)
        {
            _configuration = configuration;
            _logger = logger;
            _seedData = seedData;
            _englishWordRepositoryBL = englishWordRepositoryBL;
            _categoryRepositoryBL = categoryRepositoryBL;
            _ruleUniqueEnglishWord = ruleUniqueEnglishWord;
            _ruleUniqueCategory = ruleUniqueCategory;
            _mapper = mapper;
            _readCSVFile = readCSVFile;
        }
        public void InitializeDb(DbContextOptions<CurrentDbContext> options)
        {
            _logger.LogInformation("Initializing DB...");

            if (_seedData.Initialize(options))
            {
                AddEnglishWordsToDb();
            }

            _logger.LogInformation("Finished to initialize DB");
        }

        public int AddEnglishWordsToDb()
        {
            var fileName = _configuration.GetValue<string>("CSVFileName");
            var englishWords = _mapper.Map<List<EnglishWordBL>>(_readCSVFile.Read(fileName));
            int count = 0;

            _logger.LogInformation("Try to add new words to DB...");

            foreach (var englishWord in englishWords)
            {
                var category = englishWord.Category;

                if (category is null)
                {
                    _logger.LogWarning("EnglishWord has not category: {0}", englishWord.WordPhrase);
                    continue;
                }

                if (!_ruleUniqueCategory.IsValid(category))
                {
                    category = _categoryRepositoryBL.Read(category.Name);
                    englishWord.CategoryId = category.Id;
                    englishWord.Category = null;
                }

                if (_ruleUniqueEnglishWord.IsValid(englishWord))
                {                    
                    _englishWordRepositoryBL.Create(englishWord);
                    count++;
                }
            }

            _logger.LogInformation("Finished to add new words: {0}", count);

            return count;
        }
    }
}
