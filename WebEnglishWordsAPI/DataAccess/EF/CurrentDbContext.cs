using DataAccess.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.EF
{
    public class CurrentDbContext : DbContext
    {
        public DbSet<EnglishWord> EnglishWords { get; set; }
        public DbSet<Category> Categories { get; set; }

        public CurrentDbContext(DbContextOptions<CurrentDbContext> options) : base(options)
        {

        }
    }
}
