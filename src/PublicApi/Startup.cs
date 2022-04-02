using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Infrastructure.Data;
using Infrastructure.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using NLog.Web;
using PublicApi;
using PublicApi.Middlewares;

namespace PublicAPI
{
	public static class Startup
	{
		public static WebApplication InitializeWebApp(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			ConfigureServices(builder);

			var app = builder.Build();

			app.GetRequiredService<SeedDataFromJson>()
				.SeedData(app.GetRequiredService<EnglishWordDbContext>(), "englishwords.json");
			
			Configure(app);

			return app;
		}

		public static void ConfigureServices(WebApplicationBuilder builder)
		{
			builder.Logging.ClearProviders();
			builder.Logging.SetMinimumLevel(LogLevel.Trace);
			builder.Host.UseNLog();

			string connection = builder.Configuration.GetConnectionString("EnglishWordDbConnection");
			builder.Services.AddDbContext<EnglishWordDbContext>(options => options.UseSqlServer(connection));
			builder.Services.AddTransient(s => new SeedDataFromJson(ensureDeleted: false));

			builder.Services.AddAutoMapper(typeof(AutomapperMaps));
			builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

			builder.Services.AddScoped<IEnglishWordService, EnglishWordService>();
			builder.Services.AddScoped<IEnglishGroupService, EnglishGroupService>();

			builder.Services.AddControllers(options => options.UseNamespaceRouteToken());
			builder.Services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "PublicApi", Version = "v1" });
				c.EnableAnnotations();
			});
		}
		
		public static void Configure(WebApplication app)
		{
			if (app.Environment.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				//app.UseExceptionHandler("/error");
				app.UseMiddleware<ErrorHandlerMiddleware>();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseSwagger();

			app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PublicApi v1"));

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
		private static T GetRequiredService<T>(this WebApplication app)
		{
			var scope = app.Services.CreateScope();
			return scope.ServiceProvider.GetRequiredService<T>();
		}
	}
}
