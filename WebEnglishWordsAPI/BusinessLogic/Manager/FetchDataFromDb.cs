using BusinessLogic.Model;
using BusinessLogic.Repository;
using DataAccess.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public EnglishWordBL GetRandomEnglishWord()
        {
            var englishWords = _repositoryBL.GetAll();

            var minShowCount = englishWords.Select(x => x.ShowCount).Min();

            var englWordWithMinShCount = englishWords.Where(x => x.ShowCount == minShowCount).ToArray();

            var random = new Random();

            var index = random.Next(0, englWordWithMinShCount.Count() - 1);

            return englWordWithMinShCount[index];
        }
    }
}
