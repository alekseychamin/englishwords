using DataAccess.EF;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests
{
    public class RepositoryFactory
    {
        public static EnglishWordRepository CreateEnglishWordRepository(CurrentDbContext db)
        {
            return new EnglishWordRepository(db);
        }


    }
}
