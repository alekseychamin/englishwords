using ApplicationCore.Entities.Seeds;
using System;
using System.IO;

namespace Infrastructure.DataAccess
{
	public class SeedDataFromJson
	{
		readonly bool _ensureDeleted;

		public SeedDataFromJson(bool ensureDeleted)
		{
			_ensureDeleted = ensureDeleted;
		}

		public void SeedData(EnglishWordDbContext context, string fileName)
		{
			SeedFromJsonEnglishWord seedJson = new 
				(Path.Combine(AppContext.BaseDirectory, fileName));

			new EnglishWordDbContextSeed(context, seedJson, _ensureDeleted)
				.SeedAsync()
				.GetAwaiter()
				.GetResult();
		}
	}
}
