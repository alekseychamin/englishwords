using NLog.Web;
using System;

namespace PublicAPI
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

			try
			{
				logger.Debug("Init main");
				
				var app = Startup.InitializeWebApp(args);
				app.Run();
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
	}
}
