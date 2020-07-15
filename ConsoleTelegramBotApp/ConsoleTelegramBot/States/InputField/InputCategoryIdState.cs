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
        private readonly IConfiguration _configuration;
        private long _chatId;
        private readonly IState _nextState;
        private bool _isInitialize;

        public InputCategoryIdState(long chatId, IConfiguration configuration, IState nextState, bool isInitialize = true)
        {
            _configuration = configuration;
            _chatId = chatId;
            _nextState = nextState;
            _isInitialize = isInitialize;
        }
        public async Task ChangeState(IUniqueChatId uniqueChatId, string message)
        {
            int categoryId;

            if (int.TryParse(message, out categoryId))
            {
                uniqueChatId.SetCategoryId(_chatId, categoryId);
                
                await uniqueChatId.GetCategoryById(_chatId, categoryId);

                if (uniqueChatId.State.ContainsKey(_chatId) == false)
                    return;

                uniqueChatId.State[_chatId] = _nextState;

                if (_nextState is null)
                    return;

                if (_isInitialize)
                    await uniqueChatId.State[_chatId].Initialize();
                else
                    await uniqueChatId.State[_chatId].ChangeState(uniqueChatId, message);
            }
            else
            {
                await _configuration.SendMessageCommand.Execute(_chatId, "CategoryId is invalid", ParseMode.Html, new ReplyKeyboardRemove());
                uniqueChatId.RemoveChatId(_chatId);                
            }
        }

        public async Task Initialize()
        {
            var listCategories = await Operation.GetListCategory(_chatId, _configuration);

            if (listCategories is null)
                return;

            List<List<InlineKeyboardButton>> listKeyBut = new List<List<InlineKeyboardButton>>();
            listKeyBut.Add(new List<InlineKeyboardButton>());

            foreach (var category in listCategories)
            {
                var inlineKeyBut =  InlineKeyboardButton.WithCallbackData(category.name, category.id.ToString());

                listKeyBut[0].Add(inlineKeyBut);
            }

            await _configuration.SendMessageCommand.Execute(_chatId, "Input category:", ParseMode.Html, new InlineKeyboardMarkup(listKeyBut));
        }
    }
}
