using ConsoleTelegramBot.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace ConsoleTelegramBot.States
{
    public class InputTranscriptionState : IState
    {
        private readonly IConfiguration _configuration;
        private long _chatId;
        private readonly IState _nextState;
        private bool _isInitialize;

        public InputTranscriptionState(long chatId, IConfiguration configuration, IState nextState, bool isInitialize = true)
        {
            _configuration = configuration;
            _chatId = chatId;
            _nextState = nextState;
            _isInitialize = isInitialize;
        }

        public async Task ChangeState(IUniqueChatId uniqueChatId, string message)
        {
            uniqueChatId.EnglishWordFromUser[_chatId].Transcription = message;

            uniqueChatId.State[_chatId] = _nextState; //new InputTranslateState(_chatId, _configuration);

            if (_nextState is null)
                return;

            if (_isInitialize)
                await uniqueChatId.State[_chatId].Initialize();
            else
                await uniqueChatId.State[_chatId].ChangeState(uniqueChatId, message);
        }

        public async Task Initialize()
        {
            await _configuration.SendMessageCommand.Execute(_chatId, "Input transcription:", ParseMode.Html, new ReplyKeyboardRemove());
        }
    }
}
