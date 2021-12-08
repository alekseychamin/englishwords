using ApplicationCore.Entities.Seeds;
using Infrastructure.DataAccess;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using System;
using System.IO;
using System.Threading.Tasks;

namespace PublicAPI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

            try
            {
                logger.Debug("Init main");
                var host = CreateHostBuilder(args).Build();

                using (var scope = host.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;

                    var dbContext = services.GetRequiredService<EnglishWordDbContext>();

                    var seedJson = new SeedFromJsonEnglishWord(Path.Combine(AppContext.BaseDirectory, "englishwords.json"));

                    await new EnglishWordDbContextSeed(dbContext).SeedAsync(seedJson);
                }

                host.Run();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Stopped program because of exception"); 
                throw;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(LogLevel.Trace);
                })
                .UseNLog();
    }
}
