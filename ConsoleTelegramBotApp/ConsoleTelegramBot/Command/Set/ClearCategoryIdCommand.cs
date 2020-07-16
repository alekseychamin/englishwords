using ConsoleTelegramBot.Operations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace ConsoleTelegramBot.Command
{
    public class ClearCategoryIdCommand : INamedCommand
    {
        public string Name { get; }

        public string Description { get; }

        private readonly IConfiguration _configuration;

        public ClearCategoryIdCommand(string name, string description, IConfiguration configuration)
        {
            Name = name;
            Description = description;
            _configuration = configuration;
        }
        
        public async Task Execute(long chatId)
        {
            _configuration.Operation.SetCategoryId(0);

            await _configuration.SendMessageCommand.Execute(chatId, "Category id cleared", ParseMode.Html, new ReplyKeyboardRemove());
        }
    }
}
