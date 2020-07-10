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
    public class InputWordIdState : IState
    {
        private readonly IConfiguration _configuration;
        private readonly IState _nextState;
        private long _chatId;

        public InputWordIdState(long chatId, IConfiguration configuration, IState nextState)
        {
            _chatId = chatId;
            _configuration = configuration;
            _nextState = nextState;
        }
        public async Task ChangeState(IUniqueChatId uniqueChatId, string message)
        {
            int id;
            if (int.TryParse(message, out id))
            {
                var newEnglishWord = await Operation.GetEnglishWordById(id, _chatId, _configuration);

                if ((newEnglishWord is null) == false)
                {
                    uniqueChatId.EnglishWordFormUser[_chatId] = newEnglishWord;

                    uniqueChatId.State[_chatId] = _nextState;

                    await uniqueChatId.State[_chatId].Initialize();
                }
                else                
                    await ShowError($"Word with id: {id} not found", uniqueChatId);
            }
            else            
                await ShowError("Word id is invalid", uniqueChatId);
        }

        public async Task Initialize()
        {
            await _configuration.SendMessageCommand.Execute(_chatId, "Input word id:", ParseMode.Html, new ReplyKeyboardRemove());
        }

        private async Task ShowError(string message, IUniqueChatId uniqueChatId)
        {
            await _configuration.SendMessageCommand.Execute(_chatId, message, ParseMode.Html, new ReplyKeyboardRemove());
            uniqueChatId.RemoveChatId(_chatId);
        }
    }
}
