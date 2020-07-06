using DataAccess.EF;
using DataAccess.Model;
using DataAccess.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Extension
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection collection)
        {
            return collection.AddScoped<ISeedData, SeedData>()
                             .AddScoped<IRepository<Category>, CategoryRepository>()
                             .AddScoped<IRepository<EnglishWord>, EnglishWordRepository>();
        }
    }
}
