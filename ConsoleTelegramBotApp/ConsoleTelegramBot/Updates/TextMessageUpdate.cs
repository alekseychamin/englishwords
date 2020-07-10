using ConsoleTelegramBot.Command;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace ConsoleTelegramBot.Updates
{
    public class TextMessageUpdate : IUpdate
    {
        private readonly IConfiguration _configuration;

        public TextMessageUpdate(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task ProcessUpdate(Update update)
        {
            Task action = null;

            var chatId = update.Message.Chat.Id;

            var text = update.Message.Text;

            _configuration.Logger.Debug("Message form Telegram: {0} with chatId {1}", text, chatId);

            if (update.Message.Type != MessageType.Text)
                return;

            var uniqueChatId = GetUniqueChatId(chatId, _configuration.UniqueChatIds);

            if ((uniqueChatId is null) == false)
            {
                await uniqueChatId.SaveInfoFromUser(chatId, text);
                return;
            }


            var inputCommand = update.Message.Text.Split(' ').First();

            foreach (var command in _configuration.ListCommand)
            {
                if (command.Key.Equals(inputCommand))
                    action = command.Value.Execute(chatId);
            }

            if (action is null)
                action = _configuration.ShowAllCommand.Execute(chatId);

            await action;
        }

        private IUniqueChatId GetUniqueChatId(long chatId, List<IUniqueChatId> uniqueChatIds)
        {
            if (uniqueChatIds is null)
                return null;

            var uniqueChatId = uniqueChatIds.Where(x => x.ListChatId.Contains(chatId)).FirstOrDefault();

            return uniqueChatId;
        }
    }
}
