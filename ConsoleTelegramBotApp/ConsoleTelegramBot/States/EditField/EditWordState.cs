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
    public class EditWordState : IState
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

        public EditWordState(IState nextState, bool isInitialize = true)
        {
            NextState = nextState;
            _isInitialize = isInitialize;
        }

        public async Task ChangeState(IUniqueChatId uniqueChatId, string message)
        {
            if (message.Equals("Yes"))
            {
                var field = uniqueChatId.GetWordName(ChatId);

                if (string.IsNullOrEmpty(field) == false)
                    await _configuration.SendMessageCommand.Execute(ChatId, field, ParseMode.Html, new ReplyKeyboardRemove());

                var inputWordNameState = new InputWordNameState(this, isInitialize: false);
                _configuration.Operation.SetStateChatIdConfig(inputWordNameState, this, ChatId, _configuration);

                uniqueChatId.State[ChatId] = inputWordNameState;
                await uniqueChatId.State[ChatId].Initialize();

                return;
            }

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
            await _configuration.Operation.MakeQuestion(ChatId, "Do you need to edit word?", _configuration);
        }
    }
}
