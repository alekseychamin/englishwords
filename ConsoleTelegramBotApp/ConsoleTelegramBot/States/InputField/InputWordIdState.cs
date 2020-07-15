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
        private bool _isInitialize;

        public InputWordIdState(long chatId, IConfiguration configuration, IState nextState, bool isInitialize = true)
        {
            _chatId = chatId;
            _configuration = configuration;
            _nextState = nextState;
            _isInitialize = isInitialize;
        }
        public async Task ChangeState(IUniqueChatId uniqueChatId, string message)
        {
            int id;
            if (int.TryParse(message, out id))
            {
                uniqueChatId.SetId(id);
                
                await uniqueChatId.GetNewEnglishWordById(_chatId, id);

                if (uniqueChatId.State.ContainsKey(_chatId) == false)
                    return;

                uniqueChatId.State[_chatId] = _nextState;

                if (_nextState is null)
                    return;

                if (_isInitialize)
                    await uniqueChatId.State[_chatId].Initialize();
                else
                    await uniqueChatId.State[_chatId].ChangeState(uniqueChatId, message);
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
