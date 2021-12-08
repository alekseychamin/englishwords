using ApplicationCore.Entities.Seeds;
using ApplicationCore.Interfaces;
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

        public async Task SeedAsync(ISeed seed, int retry = 0)
        {
            try
            {
                _dbContext.Database.Migrate();

                if (!await _dbContext.EnglishGroups.AnyAsync())
                {
                    foreach (var group in EnglishWordSeed.Seed(seed))
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
                    await SeedAsync(seed, retry - 1);
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
