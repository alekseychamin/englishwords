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

        public InputWordIdState(IState nextState, bool isInitialize = true)
        {
            NextState = nextState;
            _isInitialize = isInitialize;
        }

        public async Task ChangeState(IUniqueChatId uniqueChatId, string message)
        {
            int id;
            if (int.TryParse(message, out id))
            {
                uniqueChatId.SetId(id);
                
                await uniqueChatId.GetNewEnglishWordById(ChatId, id);

                if (uniqueChatId.State.ContainsKey(ChatId) == false)
                    return;

                uniqueChatId.State[ChatId] = NextState;

                if (NextState is null)
                    return;

                if (_isInitialize)
                    await uniqueChatId.State[ChatId].Initialize();
                else
                    await uniqueChatId.State[ChatId].ChangeState(uniqueChatId, message);
            }
            else            
                await ShowError("Word id is invalid", uniqueChatId);
        }

        public async Task Initialize()
        {
            await Configuration.SendMessageCommand
                               .Execute(ChatId, "Input word id:", ParseMode.Html, new ReplyKeyboardRemove());
        }

        private async Task ShowError(string message, IUniqueChatId uniqueChatId)
        {
            await Configuration.SendMessageCommand
                               .Execute(ChatId, message, ParseMode.Html, new ReplyKeyboardRemove());
            
            uniqueChatId.RemoveChatId(ChatId);
        }
    }
}
