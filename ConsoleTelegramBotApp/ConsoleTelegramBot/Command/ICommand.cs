using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTelegramBot.Command
{
    public interface ICommand
    {
        Task Execute(long chatId);
    }
}
