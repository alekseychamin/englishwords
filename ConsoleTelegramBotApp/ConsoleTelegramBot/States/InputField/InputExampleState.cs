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
        private bool _isInitialize;

        public InputExampleState(long chatId, IConfiguration configuration, IState nextState, bool isInitialize = true)
        {
            _chatId = chatId;
            _configuration = configuration;
            _nextState = nextState;
            _isInitialize = isInitialize;
        }
        public async Task ChangeState(IUniqueChatId uniqueChatId, string message)
        {
            uniqueChatId.SetExample(_chatId, message);

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
            await _configuration.SendMessageCommand.Execute(_chatId, "Input example:", ParseMode.Html, new ReplyKeyboardRemove());
        }
    }
}
