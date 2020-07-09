using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTelegramBot.Command
{
    public interface ICommand
    {
        string Name { get; }
        string Description { get; }        

        Task Execute(long chatId, string text = null);
    }
}
