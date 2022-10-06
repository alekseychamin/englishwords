using ApplicationCore.Entities.Seeds;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Infrastructure.DataAccess
{
    public class EnglishWordDbContextSeed
    {
        readonly EnglishWordDbContext _dbContext;
        readonly ISeedEnglihsGroup _seedEnglishGroup;
        readonly bool _ensureDeleted;

        public EnglishWordDbContextSeed(
            EnglishWordDbContext dbContext,
            ISeedEnglihsGroup seedEnglihsGroup,
            bool ensureDeleted = false)
        {
            _dbContext = dbContext;

            _ensureDeleted = ensureDeleted;
            _seedEnglishGroup = seedEnglihsGroup;
        }

        public async Task SeedAsync(int retry = 0)
        {
            try
            {
                if (_ensureDeleted) await _dbContext.Database.EnsureDeletedAsync();

                await _dbContext.Database.MigrateAsync();

                if (!await _dbContext.EnglishGroups.AnyAsync())
                {
                    foreach (var group in EnglishWordSeed.SeedEnglishGroups(_seedEnglishGroup))
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
                else
                {
                    throw;
                }
            }
        }
    }
}
