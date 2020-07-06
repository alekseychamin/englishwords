using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.Repository;

namespace BusinessLogic.Repository
{
    public interface IRepositoryBL<T> : IRepository<T> where T : class
    {
        T Read(string word);

        void Update(T item);
    }
}
