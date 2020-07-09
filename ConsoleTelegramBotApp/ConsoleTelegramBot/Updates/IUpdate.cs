using ConsoleTelegramBot.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace ConsoleTelegramBot.Updates
{
    public interface IUpdate
    {
        Task ProcessUpdate(Update update);
    }
}
