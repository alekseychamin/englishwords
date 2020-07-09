using ConsoleTelegramBot.States;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTelegramBot.Command
{
    public interface IUniqueChatId : ICommand
    {
        HashSet<long> ListChatId { get; }

        Dictionary<long, IState> State { get; }

        Dictionary<long, List<string>> MessagesFromUser { get; }

        Task SaveInfoFromUser(long chatId, string message);
    }
}
