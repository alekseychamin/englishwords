using DataAccess.EF;
using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Text;

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
            return _db.Categories;
        }

        public Category Read(int id)
        {
            return _db.Categories.Find(id);
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}
