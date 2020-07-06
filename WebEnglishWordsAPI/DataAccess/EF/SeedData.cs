using DataAccess.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.EF
{
    public class SeedData : ISeedData
    {
        private readonly ILogger<SeedData> _logger;

        public SeedData(ILogger<SeedData> logger)
        {
            _logger = logger;
        }
        public bool Initialize(DbContextOptions<CurrentDbContext> options)
        {
            using (var db = new CurrentDbContext(options))
            {
                //TryToDeleteDb(db);

                return TryToCreateDb(db);
            }
        }

        private bool TryToCreateDb(DbContext db)
        {
            _logger.LogInformation("Try to create DB...");

            var isCreate = db.Database.EnsureCreated();

            if (isCreate)
                _logger.LogInformation("Finished to create DB");
            else
                _logger.LogInformation("DB did not create");

            return isCreate;
        }

        private bool TryToDeleteDb(DbContext db)
        {
            _logger.LogInformation("Try to delete DB...");

            var isDelete = db.Database.EnsureDeleted();

            if (isDelete)
                _logger.LogInformation("Finished to delete DB");
            else
                _logger.LogInformation("DB is not exist");

            return isDelete;
        }
    }
}
