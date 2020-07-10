using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace ConsoleTelegramBot.Command
{
    public interface ISendMessageCommand
    {
        Task Execute(long chatId, string text, ParseMode mode, IReplyMarkup replyMarkup);
    }
}
