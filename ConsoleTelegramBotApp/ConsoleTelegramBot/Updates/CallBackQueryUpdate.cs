using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace ConsoleTelegramBot.Updates
{
    public class CallBackQueryUpdate : IUpdate
    {
        private readonly IConfiguration _configuration;

        public CallBackQueryUpdate(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task ProcessUpdate(Update update)
        {
            var callbackQuery = update.CallbackQuery;

            await _configuration.Bot.AnswerCallbackQueryAsync(callbackQuery.Id, $"{callbackQuery.Data}");

            var chatId = callbackQuery.Message.Chat.Id;

            foreach (var uniqueChatId in _configuration.UniqueChatIds)
            {
                if (uniqueChatId.ListChatId.Contains(chatId))
                {
                    await uniqueChatId.SaveInfoFromUser(chatId, callbackQuery.Data);
                }
            }
        }
    }
}
