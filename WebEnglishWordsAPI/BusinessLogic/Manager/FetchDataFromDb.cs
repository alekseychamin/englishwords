using BusinessLogic.Model;
using BusinessLogic.Repository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Manager
{
    public class FetchDataFromDb : IFetchDataFromDb
    {
        private readonly ILogger<FetchDataFromDb> _logger;
        private readonly IEnglishWordRepositoryBL _englishWordRepositoryBL;        

        public FetchDataFromDb(ILogger<FetchDataFromDb> logger, IEnglishWordRepositoryBL englishWordRepositoryBL)
        {
            _logger = logger;
            _englishWordRepositoryBL = englishWordRepositoryBL;            
        }

        public EnglishWordBL GetRandomEnglishWord(int categoryId)
        {
            IEnumerable<EnglishWordBL> englishWords;
            
            _= categoryId == 0 ? englishWords = _englishWordRepositoryBL.GetAll() : englishWords = _englishWordRepositoryBL.GetAll(categoryId);

            var minShowCount = englishWords.Select(x => x.ShowCount).Min();

            EnglishWordBL[] englWordWithMinShCount;

            bool isCategoryId = englishWords.Select(x => x.CategoryId == categoryId).Contains(true);

            if (isCategoryId)
                englWordWithMinShCount = englishWords.Where(x => (x.ShowCount == minShowCount) && (x.CategoryId == categoryId)).ToArray();            
            else            
                englWordWithMinShCount = englishWords.Where(x => x.ShowCount == minShowCount).ToArray();            

            var random = new Random();

            var index = random.Next(0, englWordWithMinShCount.Count() - 1);

            var englishWord = englWordWithMinShCount[index];            

            var itemBL = _englishWordRepositoryBL.Read(englishWord.Id);
            itemBL.ShowCount++;

            _englishWordRepositoryBL.Update(itemBL);

            return englWordWithMinShCount[index];
        }
    }
}
