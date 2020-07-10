using ConsoleTelegramBot.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace ConsoleTelegramBot.States
{
    public class InputTranslateState : IState
    {
        private long _chatId;
        private readonly IConfiguration _configuration;

        public InputTranslateState(long chatId, IConfiguration configuration)
        {
            _chatId = chatId;
            _configuration = configuration;
        }
        public async Task ChangeState(IUniqueChatId uniqueChatId, string message)
        {
            uniqueChatId.EnglishWordFormUser[_chatId].Translate = message;

            uniqueChatId.State[_chatId] = new InputExampleState(_chatId, _configuration);
            await uniqueChatId.State[_chatId].Initialize();
        }

        public async Task Initialize()
        {
            await _configuration.SendMessageCommand.Execute(_chatId, "Input translate:", ParseMode.Html, new ReplyKeyboardRemove());
        }
    }
}
