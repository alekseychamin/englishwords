using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace ConsoleTelegramBot.Command
{
    public class ShowAllCommand : ICommand
    {
        private readonly IConfiguration _configuration;        

        public ShowAllCommand(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public async Task Execute(long chatId)
        {
            string result = "Commands:\n";
            int count = 0;
            foreach (var command in _configuration.ListCommand)
            {
                if (count == 3)
                {
                    result += "\n";
                    count = 0;
                }

                result += $"{command.Key} - {command.Value.Description}\n";
                count++;
            }
            
            await _configuration.SendMessageCommand.Execute(chatId, result, ParseMode.Markdown, new ReplyKeyboardRemove());
        }
    }
}
