using DataAccess.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Manager
{
    public interface IDataManager
    {
        void InitializeDb(DbContextOptions<CurrentDbContext> options);
        int AddEnglishWordsToDb();
    }
}
