using AutoMapper;
using BusinessLogic.Manager;
using Castle.Core.Logging;
using DataAccess.EF;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Text;

namespace Tests
{
    public class Helper
    {
        public static IMapper GetMockMapper(params Profile[] listProfile)
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                foreach (var p in listProfile)
                    cfg.AddProfile(p);
            });

            return mockMapper.CreateMapper();
        }

        public static void CreateServiceProviderWithConfig(out IConfiguration configuration, out IServiceCollection services)
        {
            configuration = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                                .Build();

            services = new ServiceCollection();

            services.AddSingleton(configuration);

            services.AddDbContext<CurrentDbContext>(builder =>
            {
                string connectionString = "DataSource=:memory:";
                DbConnection connection = new SqliteConnection(connectionString);
                connection.Open();

                builder.UseSqlite(connection);
            });
        }

        private static DbContextOptions<CurrentDbContext> GetOptionsSQLInMemory()
        {
            string connectionString = "DataSource=:memory:";

            DbConnection connection = new SqliteConnection(connectionString);
            connection.Open();

            var options = new DbContextOptionsBuilder<CurrentDbContext>()
                                         .UseSqlite(connection)
                                         .Options;

            return options;
        }

        public static CurrentDbContext GetEmptyCurrentDbContextSQLInMemory()
        {

            var options = GetOptionsSQLInMemory();
            var currentDbContext = new CurrentDbContext(options);
            
            return currentDbContext;
        }

        public static CurrentDbContext GetWithDataCurrentDbContextSQLInMemory()
        {

            var options = GetOptionsSQLInMemory();
            var currentDbContext = new CurrentDbContext(options);

            

            return currentDbContext;
        }
    }
}
