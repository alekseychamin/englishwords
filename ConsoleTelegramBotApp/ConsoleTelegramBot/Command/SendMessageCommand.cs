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
    public class SendMessageCommand : ISendMessageCommand
    {
        private readonly IConfiguration _configuration;        

        public SendMessageCommand(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public async Task Execute(long chatId, string text, ParseMode mode, IReplyMarkup replyMarkup)
        {
            if (chatId == -1)
            {
                _configuration.Logger.Error("Can`t send message to bot, ChatId is -1");
                return;
            }

            await _configuration.Bot.SendTextMessageAsync(
                        chatId: chatId,
                        parseMode: mode,
                        text: text,
                        replyMarkup: replyMarkup
             );
        }
    }
}
