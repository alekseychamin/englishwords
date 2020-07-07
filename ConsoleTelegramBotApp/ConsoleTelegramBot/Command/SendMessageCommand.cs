using NLog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace ConsoleTelegramBot.Command
{
    public class SendMessageCommand : ICommand
    {
        public string Name { get; }

        public string Description { get; }

        private readonly ITelegramBotClient _bot;
        private readonly ILogger _logger;

        public SendMessageCommand(string name, string description,
                                  ITelegramBotClient bot, ILogger logger)
        {
            Name = name;
            Description = description;
            _bot = bot;
            _logger = logger;
        }
        
        public async Task Execute(long chatId, string text = null)
        {
            if (chatId == -1)
            {
                _logger.Error("Can`t send message to bot, ChatId is -1");
                return;
            }

            await _bot.SendTextMessageAsync(
                        chatId: chatId,
                        parseMode: ParseMode.Markdown,
                        text: text,
                        replyMarkup: new ReplyKeyboardRemove()
             );
        }
    }
}
