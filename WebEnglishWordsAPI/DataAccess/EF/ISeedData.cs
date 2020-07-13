using DataAccess.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DataAccess.EF
{
    public interface ISeedData
    {
        bool Initialize(DbContextOptions<CurrentDbContext> options, bool isDelete = false);        
    }
}