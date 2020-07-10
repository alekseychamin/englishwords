using ConsoleTelegramBot.Command;
using NLog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace ConsoleTelegramBot.Updates
{
    public class UnknownUpdate : IUpdate
    {
        private readonly IConfiguration _configuration;

        public UnknownUpdate(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public Task ProcessUpdate(Update update)
        {
            _configuration.Logger.Error("Unknown update type: {0}", update.Type);

            return Task.CompletedTask;
            //await _configuration.SendMessageCommand.Execute(update.Chat.Id, $"Unknown update type: {update.Type}");
        }
    }
}
