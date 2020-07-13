using DataAccess.EF;
using DataAccess.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class EnglishWordRepository : IRepository<EnglishWord>
    {
        private readonly CurrentDbContext _db;

        public EnglishWordRepository(CurrentDbContext db)
        {
            _db = db;
        }

        public void Create(EnglishWord item)
        {
            _db.EnglishWords.Add(item);

            SaveChanges();
        }

        public bool Delete(int id)
        {
            var item = Read(id);

            if (item is null)
                return false;
            
            _db.EnglishWords.Remove(item);

            return true;
        }

        public IEnumerable<EnglishWord> GetAll()
        {
            return _db.EnglishWords
                      .Include(x => x.Category);
                        //.ThenInclude(x => x.EnglishWords);
        }

        public EnglishWord Read(int id)
        {
            return _db.EnglishWords
                      .Include(x => x.Category)
                        //.ThenInclude(x => x.EnglishWords)
                      .SingleOrDefault(x => x.Id == id);
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}
