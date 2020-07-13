using BusinessLogic.Model;
using DataAccess.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Manager
{
    public interface IDataManager
    {
        void InitializeDb(DbContextOptions<CurrentDbContext> options, string fileName, bool isDelete = false);
        
        int AddEnglishWordsToDb(string fileName);

        EnglishWordBL GetRandomEnglishWord(int categoryId);
    }
}
