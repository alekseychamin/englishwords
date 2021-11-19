using ApplicationCore.Entities.Seeds;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Infrastructure.DataAccess
{
    public class EnglishWordDbContextSeed
    {
        private readonly EnglishWordDbContext _dbContext;

        public EnglishWordDbContextSeed(EnglishWordDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SeedAsync(int retry = 0)
        {
            try
            {
                _dbContext.Database.Migrate();

                if (!await _dbContext.EnglishGroups.AnyAsync())
                {
                    foreach (var group in EnglishWordSeed.Seed())
                    {
                        _dbContext.EnglishGroups.Add(group);
                    }

                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                if (retry > 0)
                {
                    await SeedAsync(retry - 1);
                }
            }
        }
    }
}
