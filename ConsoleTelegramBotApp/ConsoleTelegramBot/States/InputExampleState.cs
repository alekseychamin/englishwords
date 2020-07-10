using ConsoleTelegramBot.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace ConsoleTelegramBot.States
{
    public class InputExampleState : IState
    {
        private long _chatId;
        private readonly IConfiguration _configuration;
        private readonly IState _nextState;

        public InputExampleState(long chatId, IConfiguration configuration, IState nextState)
        {
            _chatId = chatId;
            _configuration = configuration;
            _nextState = nextState;
        }
        public async Task ChangeState(IUniqueChatId uniqueChatId, string message)
        {
            uniqueChatId.EnglishWordFormUser[_chatId].Example = message;

            uniqueChatId.State[_chatId] = _nextState; //new InputCategoryIdState(_chatId, _configuration);
            await uniqueChatId.State[_chatId].Initialize();
        }

        public async Task Initialize()
        {
            await _configuration.SendMessageCommand.Execute(_chatId, "Input example:", ParseMode.Html, new ReplyKeyboardRemove());
        }
    }
}
