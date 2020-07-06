using DataAccess.Model;
using System.Collections.Generic;

namespace BusinessLogic.ReadCSV
{
    public interface IReadCSVFile
    {
        List<EnglishWord> Read(string fileName);
    }
}