using ConsoleTelegramBot.Model;
using ConsoleTelegramBot.Operations;
using ConsoleTelegramBot.States;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace ConsoleTelegramBot.Command
{
    public class SetCategoryIdCommand : IUniqueChatId
    {
        public HashSet<long> ListChatId { get; } = new HashSet<long>();

        public Dictionary<long, IState> State { get; } = new Dictionary<long, IState>();

        public string Name { get; }

        public string Description { get; }

        private int categoryId;

        private readonly IConfiguration _configuration;

        public SetCategoryIdCommand(string name, string description, IConfiguration configuration)
        {
            Name = name;
            Description = description;
            _configuration = configuration;
        }
        
        public async Task Execute(long chatId)
        {
            ListChatId.Add(chatId);

            State.Add(chatId, new InputCategoryIdState(chatId, _configuration, null));

            await State[chatId].Initialize();
        }

        public void RemoveChatId(long chatId)
        {
            ListChatId.Remove(chatId);            
            State.Remove(chatId);
        }

        public async Task SaveInfoFromUser(long chatId, string message)
        {
            _configuration.Logger.Debug("Get message from user : {0}", message);

            await State[chatId].ChangeState(this, message);

            if (State.ContainsKey(chatId) == false)
                return;

            if (State[chatId] == null)
            {
                Operation.SetCategoryId(categoryId);

                await _configuration.SendMessageCommand.Execute(chatId, "Category id selected", ParseMode.Html, new ReplyKeyboardRemove());

                RemoveChatId(chatId);
            }
        }

        public void SetCategoryId(long chatId, int value)
        {
            categoryId = value;
        }
    }
}
