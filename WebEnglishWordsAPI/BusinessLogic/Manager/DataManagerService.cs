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
using System.Threading.Tasks;

namespace BusinessLogic.Manager
{
    public class DataManagerService : IDataManagerService
    {
        private readonly ILogger<DataManagerService> _logger;
        private readonly ISeedData _seedData;
        private readonly IDataFromFileToDb _dataFromFileToDb;
        private readonly IFetchDataFromDb _fetchDataFromDb;

        public DataManagerService(ILogger<DataManagerService> logger,
                           ISeedData seedData, 
                           IDataFromFileToDb dataFormFileToDb,
                           IFetchDataFromDb fetchDataFromDb)
        {
            _logger = logger;
            _seedData = seedData;
            _dataFromFileToDb = dataFormFileToDb;
            _fetchDataFromDb = fetchDataFromDb;
        }
        public void InitializeDb(DbContextOptions<CurrentDbContext> options, string fileName, bool isDelete)
        {
            if (_seedData.Initialize(options, isDelete))
            {
                AddEnglishWordsToDb(fileName);
            }
        }

        public int AddEnglishWordsToDb(string fileName)
        {
            return _dataFromFileToDb.AddEnglishWordFromCSVFile(fileName);
        }

        public EnglishWordBL GetRandomEnglishWord(int categoryId)
        {
            return _fetchDataFromDb.GetRandomEnglishWord(categoryId);
        }
    }
}
