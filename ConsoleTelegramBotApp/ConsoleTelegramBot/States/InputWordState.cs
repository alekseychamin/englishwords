using ConsoleTelegramBot.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace ConsoleTelegramBot.States
{
    public class InputWordState : IState
    {
        private readonly IConfiguration _configuration;
        private long _chatId;

        public InputWordState(long chatId, IConfiguration configuration)
        {
            _configuration = configuration;
            _chatId = chatId;
        }

        public async Task ChangeState(IUniqueChatId uniqueChatId, string message)
        {
            uniqueChatId.EnglishWordFormUser[_chatId].WordPhrase = message;
            
            uniqueChatId.State[_chatId] = new InputTranscriptionState(_chatId, _configuration);
            await uniqueChatId.State[_chatId].Initialize();
        }

        public async Task Initialize()
        {
            await _configuration.SendMessageCommand.Execute(_chatId, "Input new word or phrase:", ParseMode.Html, new ReplyKeyboardRemove());
        }
    }
}
