using BusinessLogic.Model;
using BusinessLogic.Repository;
using DataAccess.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Manager
{
    public class FetchDataFromDb : IFetchDataFromDb
    {
        private readonly ILogger<FetchDataFromDb> _logger;
        private readonly IRepositoryBL<EnglishWordBL> _repositoryBL;

        public FetchDataFromDb(ILogger<FetchDataFromDb> logger, IRepositoryBL<EnglishWordBL> repositoryBL)
        {
            _logger = logger;
            _repositoryBL = repositoryBL;
        }

        public EnglishWordBL GetRandomEnglishWord(int categoryId)
        {
            var englishWords = _repositoryBL.GetAll();

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

            var itemBL = _repositoryBL.Read(englishWord.Id);
            itemBL.ShowCount++;

            _repositoryBL.Update(itemBL);

            return englWordWithMinShCount[index];
        }
    }
}
