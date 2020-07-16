using ConsoleTelegramBot.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace ConsoleTelegramBot.States
{
    public class InputTranscriptionState : IState
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

        public InputTranscriptionState(IState nextState, bool isInitialize = true)
        {
            NextState = nextState;
            _isInitialize = isInitialize;
        }

        public async Task ChangeState(IUniqueChatId uniqueChatId, string message)
        {
            uniqueChatId.SetTranscription(ChatId, message);

            uniqueChatId.State[ChatId] = NextState;

            if (NextState is null)
                return;

            if (_isInitialize)
                await uniqueChatId.State[ChatId].Initialize();
            else
                await uniqueChatId.State[ChatId].ChangeState(uniqueChatId, message);
        }

        public async Task Initialize()
        {
            await _configuration.SendMessageCommand.Execute(ChatId, "Input transcription:", ParseMode.Html, new ReplyKeyboardRemove());
        }
    }
}
