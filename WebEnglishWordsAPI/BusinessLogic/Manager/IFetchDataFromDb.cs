using BusinessLogic.Model;

namespace BusinessLogic.Manager
{
    public interface IFetchDataFromDb
    {
        EnglishWordBL GetRandomEnglishWord();
    }
}