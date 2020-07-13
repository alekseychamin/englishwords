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
    public class CategoryRepository : IRepository<Category>
    {
        private readonly CurrentDbContext _db;

        public CategoryRepository(CurrentDbContext db)
        {
            _db = db;
        }

        public void Create(Category item)
        {
            _db.Categories.Add(item);

            SaveChanges();
        }

        public bool Delete(int id)
        {
            var item = Read(id);

            if (item is null)
                return false;

            _db.Categories.Remove(item);

            return true;
        }

        public IEnumerable<Category> GetAll()
        {
            return _db.Categories
                      .Include(x => x.EnglishWords);
        }

        public Category Read(int id)
        {
            return _db.Categories
                      .Include(x => x.EnglishWords)
                      .SingleOrDefault(x => x.Id == id);
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}
