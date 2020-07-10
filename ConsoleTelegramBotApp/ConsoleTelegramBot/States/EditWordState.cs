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
        private readonly IConfiguration _configuration;
        private long _chatId;
        private readonly IState _nextState;

        public EditWordState(long chatId, IConfiguration configuration, IState nextState)
        {
            _configuration = configuration;
            _chatId = chatId;
            _nextState = nextState;
        }

        public async Task ChangeState(IUniqueChatId uniqueChatId, string message)
        {
            int answer;

            if (int.TryParse(message, out answer))
            {
                if (answer == 1)
                {
                    uniqueChatId.State[_chatId] = new InputWordState(_chatId, _configuration, this, isInitialize: false);
                    await uniqueChatId.State[_chatId].Initialize();

                    return;
                }
            }

            uniqueChatId.State[_chatId] = _nextState;
            await uniqueChatId.State[_chatId].Initialize();
        }

        public async Task Initialize()
        {
            await Operation.MakeQuestion(_chatId, "Do you need to edit word?", _configuration);
        }
    }
}
