using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Repository
{
    public interface IRepository<T> where T: class
    {
        IEnumerable<T> GetAll();
        
        void Create(T item);

        T Read(int id);

        //T Read(string word);

        //void Update(T item);

        bool Delete(int id);

        void SaveChanges();
    }
}
