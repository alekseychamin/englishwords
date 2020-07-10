using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace ConsoleTelegramBot.Command
{
    public interface INamedCommand : ICommand
    {
        string Name { get; }
        string Description { get; }
    }
}
