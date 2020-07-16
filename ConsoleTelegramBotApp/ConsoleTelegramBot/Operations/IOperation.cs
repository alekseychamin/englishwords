using ConsoleTelegramBot.Model;
using ConsoleTelegramBot.States;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleTelegramBot.Operations
{
    public interface IOperation
    {
        Task CreateNewCategory(long chatId, NewCategory newCategory, IConfiguration configuration);
        Task CreateNewWord(long chatId, NewEnglishWord newEnglishWord, IConfiguration configuration);
        Task DeleteCategory(long chatId, int id, IConfiguration configuration);
        Task DeleteEnglishWord(long chatId, int id, IConfiguration configuration);
        Task EditCategory(long chatId, int id, NewCategory newCategory, IConfiguration configuration);
        Task EditEnglishWord(long chatId, int id, NewEnglishWord newEnglishWord, IConfiguration configuration);
        Task GetEnglishWordById(int id, long chatId, IConfiguration configuration);
        Task<List<Category>> GetListCategory(long chatId, IConfiguration configuration);
        Task<NewCategory> GetNewCategoryById(int id, long chatId, IConfiguration configuration);
        Task<NewEnglishWord> GetNewEnglishWordById(int id, long chatId, IConfiguration configuration);
        Task GetRandomEnglishWord(long chatId, IConfiguration configuration);
        Task MakeQuestion(long chatId, string message, IConfiguration configuration);
        NewCategory MapCategory(Category category);
        NewEnglishWord MapEnglishWord(EnglishWord englishWord);
        Task ProcessResponseCategory(long chatId, IConfiguration configuration, string response, string successedMessage, string failMessage);
        Task ProcessResponseEnglishWord(long chatId, IConfiguration configuration, string response, string successedMessage, string failMessage);
        void SetCategoryId(int categoryId);
        void SetStateChatIdConfig(IState startState, IState stopState, long chatId, IConfiguration configuration);
    }
}