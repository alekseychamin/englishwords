using BusinessLogic.Model;
using System.Collections.Generic;

namespace BusinessLogic.Repository
{
    public interface IEnglishWordRepositoryBL
    {
        void Create(EnglishWordBL item);
        bool Delete(int id);
        IEnumerable<EnglishWordBL> GetAll();
        IEnumerable<EnglishWordBL> GetAll(int categoryId);
        EnglishWordBL Read(int id);
        EnglishWordBL Read(string word);
        void SaveChanges();
        void Update(EnglishWordBL item);
    }
}