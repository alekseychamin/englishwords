using ConsoleTelegramBot.Command;
using ConsoleTelegramBot.Model;
using ConsoleTelegramBot.Operations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace ConsoleTelegramBot.States
{
    public class InputCategoryIdState : IState
    {
        private IConfiguration _configuration;
        public IConfiguration Configuration
        {
            get
            {
                if (_configuration is null)
                    throw new ArgumentNullException(nameof(Configuration));

                return _configuration;
            }
            set
            {
                if (value is null)
                    throw new ArgumentNullException(nameof(Configuration));

                _configuration = value;
            }
        }
        public long ChatId { get; set; }

        public IState NextState { get; private set; }

        private bool _isInitialize;

        public InputCategoryIdState(IState nextState, bool isInitialize = true)
        {
            NextState = nextState;
            _isInitialize = isInitialize;
        }
        public async Task ChangeState(IUniqueChatId uniqueChatId, string message)
        {
            int categoryId;

            if (int.TryParse(message, out categoryId))
            {
                uniqueChatId.SetCategoryId(ChatId, categoryId);
                
                await uniqueChatId.GetCategoryById(ChatId, categoryId);

                if (uniqueChatId.State.ContainsKey(ChatId) == false)
                    return;

                uniqueChatId.State[ChatId] = NextState;

                if (NextState is null)
                    return;

                if (_isInitialize)
                    await uniqueChatId.State[ChatId].Initialize();
                else
                    await uniqueChatId.State[ChatId].ChangeState(uniqueChatId, message);
            }
            else
            {
                await _configuration.SendMessageCommand.Execute(ChatId, "CategoryId is invalid", ParseMode.Html, new ReplyKeyboardRemove());
                uniqueChatId.RemoveChatId(ChatId);                
            }
        }

        public async Task Initialize()
        {
            var listCategories = await _configuration.Operation.GetListCategory(ChatId, _configuration);

            if (listCategories is null)
                return;

            List<List<InlineKeyboardButton>> listKeyBut = new List<List<InlineKeyboardButton>>();
            listKeyBut.Add(new List<InlineKeyboardButton>());

            foreach (var category in listCategories)
            {
                var inlineKeyBut =  InlineKeyboardButton.WithCallbackData(category.name, category.id.ToString());

                listKeyBut[0].Add(inlineKeyBut);
            }

            await _configuration.SendMessageCommand.Execute(ChatId, "Input category:", ParseMode.Html, new InlineKeyboardMarkup(listKeyBut));
        }
    }
}
