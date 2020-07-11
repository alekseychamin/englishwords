using ConsoleTelegramBot.Command;
using ConsoleTelegramBot.Operations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace ConsoleTelegramBot.States
{
    public class EditTranslateState : IState
    {
        private readonly IConfiguration _configuration;
        private long _chatId;
        private readonly IState _nextState;
        private bool _isInitialize;

        public EditTranslateState(long chatId, IConfiguration configuration, IState nextState, bool isInitialize = true)
        {
            _configuration = configuration;
            _chatId = chatId;
            _nextState = nextState;
            _isInitialize = isInitialize;
        }

        public async Task ChangeState(IUniqueChatId uniqueChatId, string message)
        {
            if (message.Equals("Yes"))
            {
                var field = uniqueChatId.EnglishWordFromUser[_chatId].Translate;

                if (string.IsNullOrEmpty(field) == false)
                    await _configuration.SendMessageCommand.Execute(_chatId, field, ParseMode.Html, new ReplyKeyboardRemove());

                uniqueChatId.State[_chatId] = new InputTranslateState(_chatId, _configuration, this, isInitialize: false);
                await uniqueChatId.State[_chatId].Initialize();

                return;
            }

            uniqueChatId.State[_chatId] = _nextState;

            if (_nextState is null)
                return;

            if (_isInitialize)
                await uniqueChatId.State[_chatId].Initialize();
            else
                await uniqueChatId.State[_chatId].ChangeState(uniqueChatId, message);
        }

        public async Task Initialize()
        {
            await Operation.MakeQuestion(_chatId, "Do you need to edit translate?", _configuration);
        }
    }
}
