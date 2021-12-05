using ApplicationCore.Entities;
using Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataAccess
{
    public class EnglishWordDbContext : DbContext
    {
        public DbSet<EnglishGroup> EnglishGroups { get; set; }

        public DbSet<EnglishWord> EnglishWords { get; set; }

        public EnglishWordDbContext(DbContextOptions<EnglishWordDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new EnglishGroupConfiguration());
            builder.ApplyConfiguration(new EnglishWordConfiguration());

            builder.Entity<EnglishGroup>()
                   .HasMany(x => x.EnglishWords)
                   .WithOne(x => x.EnglishGroup)
                   .HasForeignKey(x => x.EnglishGroupId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
