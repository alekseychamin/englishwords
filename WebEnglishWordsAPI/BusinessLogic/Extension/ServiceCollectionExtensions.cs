using BusinessLogic.Manager;
using BusinessLogic.Model;
using BusinessLogic.ReadCSV;
using BusinessLogic.Repository;
using BusinessLogic.Validations;
using DataAccess.Model;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Extension
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBusinessLogic(this IServiceCollection collection)
        {
            return collection.AddScoped<IDataManagerService, DataManagerService>()
                             .AddScoped<IRepositoryBL<EnglishWordBL>, EnglishWordRepositoryBL>()
                             .AddScoped<IRepositoryBL<CategoryBL>, CategoryRepositoryBL>()
                             .AddScoped<IReadCSVFile, ReadCSVFile>()
                             .AddScoped<IRuleUniqueValidation<EnglishWordBL>, RuleUniqueEnglishWord>()                             
                             .AddScoped<IRuleUniqueValidation<CategoryBL>, RuleUniqueCategory>()
                             .AddScoped<IRuleExistIdCategory, RuleExistIdCategory>()
                             .AddScoped<IUniqueValidation<EnglishWordBL>, UniqueEnglishWordValidation>()
                             .AddScoped<IUniqueValidation<CategoryBL>, UniqueCategoryValidation>()
                             .AddScoped<IExistIdCategoryValidation, ExistIdCategoryValidation>()
                             .AddScoped<IDataFromFileToDb, DataFromFileToDb>()
                             .AddScoped<IEnglishWordRepositoryBL, EnglishWordRepositoryBL>()
                             .AddScoped<IFetchDataFromDb, FetchDataFromDb>();
        }
    }
}
