using Infrastructure.DataAccess;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PublicAPI;
using System;
using System.IO;
using System.Linq;

namespace FunctionalTests.PublicApi
{
	public class ApiTestFixture : WebApplicationFactory<Program>
	{
		protected override void ConfigureWebHost(IWebHostBuilder builder)
		{
			builder.ConfigureServices(configureServices =>
			{
				RemoveService(typeof(DbContextOptions<EnglishWordDbContext>), configureServices);
				RemoveService(typeof(SeedDataFromJson), configureServices);

				var config = new ConfigurationBuilder()
					.AddJsonFile(Path.Combine(AppContext.BaseDirectory, "appsettings.json"), false)
					.Build();

				configureServices.AddDbContext<EnglishWordDbContext>(options =>
				{
					options.UseSqlServer(config.GetConnectionString("EnglishWordDbConnection"));
				});

				configureServices.AddTransient(s => new SeedDataFromJson(ensureDeleted: true));
			});
		}

		private void RemoveService(Type type, IServiceCollection services)
		{
			var descriptor = services.SingleOrDefault(d => d.ServiceType == type);

			services?.Remove(descriptor);
		}
	}
}
