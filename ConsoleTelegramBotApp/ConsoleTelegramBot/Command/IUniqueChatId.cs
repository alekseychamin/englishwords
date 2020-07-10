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

        Dictionary<long, NewEnglishWord> EnglishWordFormUser { get; }

        Task SaveInfoFromUser(long chatId, string message);

        void RemoveChatId(long chatId);
    }
}
