using BusinessLogic.Model;
using System.Threading.Tasks;

namespace BusinessLogic.Manager
{
    public interface IFetchDataFromDb
    {
        EnglishWordBL GetRandomEnglishWord(int categoryId);
    }
}