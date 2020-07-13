using System.Threading.Tasks;

namespace BusinessLogic.Manager
{
    public interface IDataFromFileToDb
    {
        int AddEnglishWordFromCSVFile(string fileName);
    }
}