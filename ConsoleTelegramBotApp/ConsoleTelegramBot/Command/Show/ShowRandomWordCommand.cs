using ConsoleTelegramBot.Operations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace ConsoleTelegramBot.Command
{
    public class ShowRandomWordCommand : INamedCommand
    {
        public string Name { get; }

        public string Description { get; }

        private readonly IConfiguration _configuration;

        public ShowRandomWordCommand(string name, string description, IConfiguration configuration)
        {
            Name = name;
            Description = description;
            _configuration = configuration;
        }

        public async Task Execute(long chatId)
        {
            await _configuration.Operation.GetRandomEnglishWord(chatId, _configuration);
        }
    }
}
