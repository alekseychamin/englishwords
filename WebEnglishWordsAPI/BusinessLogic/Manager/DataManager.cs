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
        private readonly ILogger<DataManager> _logger;
        private readonly ISeedData _seedData;
        private readonly IDataFormFileToDb _dataFromFileToDb;
        private readonly IFetchDataFromDb _fetchDataFromDb;

        public DataManager(ILogger<DataManager> logger,
                           ISeedData seedData, 
                           IDataFormFileToDb dataFormFileToDb,
                           IFetchDataFromDb fetchDataFromDb)
        {
            _logger = logger;
            _seedData = seedData;
            _dataFromFileToDb = dataFormFileToDb;
            _fetchDataFromDb = fetchDataFromDb;
        }
        public void InitializeDb(DbContextOptions<CurrentDbContext> options)
        {
            if (_seedData.Initialize(options))
            {
                AddEnglishWordsToDb();
            }
        }

        public int AddEnglishWordsToDb()
        {
            return _dataFromFileToDb.AddEnglishWordFromCSVFile();
        }

        public EnglishWordBL GetRandomEnglishWord()
        {
            return _fetchDataFromDb.GetRandomEnglishWord();
        }
    }
}
