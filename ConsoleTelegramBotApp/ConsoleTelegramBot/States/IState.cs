using ConsoleTelegramBot.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTelegramBot.States
{
    public interface IState
    {
        IState NextState { get; }
        IConfiguration Configuration { get; set; }
        long ChatId { get; set; }
        Task ChangeState(IUniqueChatId uniqueChatId, string message);
        Task Initialize();
    }
}
