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
        private readonly ILogger _logger;
        private readonly ICommand _showAllCommand;
        private readonly Dictionary<string, ICommand> _listCommand;
        private readonly List<IUniqueChatId> _uniqueChatIds;

        public TextMessageUpdate(ICommand showAllCommand, Dictionary<string, ICommand> listCommand,
                                 ILogger logger, List<IUniqueChatId> uniqueChatIds)
        {
            _logger = logger;
            _showAllCommand = showAllCommand;
            _listCommand = listCommand;
            _uniqueChatIds = uniqueChatIds;
        }
        public async Task ProcessUpdate(Update update)
        {
            Task action = null;

            var chatId = update.Message.Chat.Id;

            var text = update.Message.Text;

            _logger.Debug("Message form Telegram: {0} with chatId {1}", text, chatId);

            if (update.Message.Type != MessageType.Text)
                return;

            var uniqueChatId = GetUniqueChatId(chatId, _uniqueChatIds);

            if ((uniqueChatId is null) == false)
            {
                await uniqueChatId.SaveInfoFromUser(chatId, text);
                return;
            }


            var inputCommand = update.Message.Text.Split(' ').First();

            foreach (var command in _listCommand)
            {
                if (command.Key.Equals(inputCommand))
                    action = command.Value.Execute(chatId);
            }

            if (action is null)
                action = _showAllCommand.Execute(chatId);

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
