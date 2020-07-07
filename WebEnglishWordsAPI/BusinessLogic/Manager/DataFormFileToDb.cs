using AutoMapper;
using BusinessLogic.Model;
using BusinessLogic.ReadCSV;
using BusinessLogic.Repository;
using BusinessLogic.Validations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Manager
{
    public class DataFormFileToDb : IDataFormFileToDb
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<DataFormFileToDb> _logger;
        private readonly IRepositoryBL<EnglishWordBL> _englishWordRepositoryBL;
        private readonly IRepositoryBL<CategoryBL> _categoryRepositoryBL;
        private readonly IRuleUniqueValidation<EnglishWordBL> _ruleUniqueEnglishWord;
        private readonly IRuleUniqueValidation<CategoryBL> _ruleUniqueCategory;
        private readonly IMapper _mapper;
        private readonly IReadCSVFile _readCSVFile;

        public DataFormFileToDb(IConfiguration configuration, ILogger<DataFormFileToDb> logger,
                                IRepositoryBL<EnglishWordBL> englishWordRepositoryBL,
                                IRepositoryBL<CategoryBL> categoryRepositoryBL,
                                IRuleUniqueValidation<EnglishWordBL> ruleUniqueEnglishWord,
                                IRuleUniqueValidation<CategoryBL> ruleUniqueCategory,
                                IMapper mapper, IReadCSVFile readCSVFile)
        {
            _configuration = configuration;
            _logger = logger;
            _englishWordRepositoryBL = englishWordRepositoryBL;
            _categoryRepositoryBL = categoryRepositoryBL;
            _ruleUniqueEnglishWord = ruleUniqueEnglishWord;
            _ruleUniqueCategory = ruleUniqueCategory;
            _mapper = mapper;
            _readCSVFile = readCSVFile;
        }

        public int AddEnglishWordFromCSVFile()
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
