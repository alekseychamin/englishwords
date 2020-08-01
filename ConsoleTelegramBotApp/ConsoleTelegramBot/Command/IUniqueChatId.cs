using ConsoleTelegramBot.Model;
using ConsoleTelegramBot.States;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTelegramBot.Command
{
    public interface IUniqueChatId : INamedCommand
    {        
        HashSet<long> ListChatId { get; }

        Dictionary<long, IState> State { get; }

        Task GetNewEnglishWordById(long chatId, int id) { return Task.CompletedTask; }

        Task GetCategoryById(long chatId, int id) { return Task.CompletedTask; }

        void SetId(int value) { }

        void SetWordName(long chatId, string value) { }

        void SetTranscription(long chatId, string value) { }

        void SetTranslate(long chatId, string value) { }

        void SetExample(long chatId, string value) { }

        void SetCategoryId(long chatId, int value) { }

        void SetCategoryName(long chatId, string value) { }

        //void SetNewEnglisgWord(long chatId, NewEnglishWord newEnglishWord) { }

        string GetWordName(long chatId) { return null; }

        string GetTranscription(long chatId) { return null; }

        string GetTranslate(long chatId) { return null; }

        string GetExample(long chatId) { return null; }

        string GetCategoryName(long chatId) { return null; }

        Task SaveInfoFromUser(long chatId, string message);

        void RemoveChatId(long chatId);
    }
}
