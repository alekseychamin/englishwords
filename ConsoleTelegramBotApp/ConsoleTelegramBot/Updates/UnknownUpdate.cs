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
        private readonly ICommand _sendMessageCommand;
        private readonly ILogger _logger;

        public UnknownUpdate(ICommand sendMessageCommand, ILogger logger)
        {
            _sendMessageCommand = sendMessageCommand;
            _logger = logger;
        }
        public async Task ProcessUpdate(Update update)
        {
            _logger.Error("Unknown update type: {0}", update.Type);            

            await _sendMessageCommand.Execute(update.Message.Chat.Id, $"Unknown update type: {update.Type}");
        }
    }
}
