using AutoMapper;
using BusinessLogic.Manager;
using BusinessLogic.Model;
using BusinessLogic.Repository;
using BusinessLogic.Validations;
using DataAccess.EF;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using BusinessLogic.Extension;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using WebAPI.Controllers;
using WebAPI.Model;
using DataAccess.Extension;
using NLog.Extensions.Logging;
using BusinessLogic.Profiles;
using WebAPI.Profiles;

namespace Tests.WebAPI
{
    public class EnglishWordControllerTests
    {
        private IConfiguration configuration;
        private IServiceProvider serviceProvider;
        private IServiceCollection services;
        private IRepositoryBL<EnglishWordBL> englishWordRepositoryBL;
        private IMapper mapper;
        private IUniqueValidation<EnglishWordBL> uniqueEnglishWordValidation;
        private IExistIdCategoryValidation existIdCategoryValidation;
        private EnglishWordController englishWordController;

        [SetUp]
        public void Init()
        {
            Helper.CreateServiceProviderWithConfig(out configuration, out services);

            services.AddDataAccess();
            services.AddBusinessLogic();

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.AddConfiguration(configuration.GetSection("Logging"));
                loggingBuilder.AddNLog(configuration);
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            serviceProvider = services.BuildServiceProvider();

            var options = serviceProvider.GetRequiredService<DbContextOptions<CurrentDbContext>>();
            var dataManager = serviceProvider.GetRequiredService<IDataManager>();
            
            var fileName = configuration.GetValue<string>("CSVFileNameTests");
            dataManager.InitializeDb(options, fileName, isDelete: false);

            englishWordRepositoryBL = serviceProvider.GetRequiredService<IRepositoryBL<EnglishWordBL>>();
            mapper = serviceProvider.GetRequiredService<IMapper>();
            uniqueEnglishWordValidation = serviceProvider.GetRequiredService<IUniqueValidation<EnglishWordBL>>();
            existIdCategoryValidation = serviceProvider.GetRequiredService<IExistIdCategoryValidation>();

            var logger = serviceProvider.GetRequiredService<ILogger<EnglishWordController>>();

            englishWordController = new EnglishWordController(englishWordRepositoryBL, mapper, uniqueEnglishWordValidation, 
                                                              existIdCategoryValidation, logger);

        }
        
        [Test]
        public void CreateItemTest()
        {
            // Assign
            var itemCreate = new EnglishWordCreate 
            {
                WordPhrase = "Test",
                Transcription = "Test",
                Translate = "Test",
                Example = "Test",
                CategoryId = 1
            };

            // Act
            var currentDbContext = serviceProvider.GetRequiredService<CurrentDbContext>();
            var count = currentDbContext.EnglishWords.Count();

            englishWordController.CreateItem(itemCreate);

            // Assert
            Assert.AreEqual(count + 1, currentDbContext.EnglishWords.Count());
        }

        [TestCase(1, 2)]
        [TestCase(1, 2)]
        [TestCase(2, 1)]
        public void UpdateItemTest(int id, int categoryId)
        {
            // Assign
            var itemUpdate = new EnglishWordUpdate
            {                
                CategoryId = categoryId
            };

            // Act
            var currentDbContext = serviceProvider.GetRequiredService<CurrentDbContext>();
            var count = currentDbContext.EnglishWords.Count();

            englishWordController.UpdateItem(id, itemUpdate);

            // Assert
            Assert.AreEqual(count, currentDbContext.EnglishWords.Count());
        }
    }
}
