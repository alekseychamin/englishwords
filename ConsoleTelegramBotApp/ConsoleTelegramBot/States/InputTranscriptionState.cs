﻿using ConsoleTelegramBot.Command;
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

        public InputTranscriptionState(long chatId, IConfiguration configuration, IState nextState)
        {
            _configuration = configuration;
            _chatId = chatId;
            _nextState = nextState;
        }

        public async Task ChangeState(IUniqueChatId uniqueChatId, string message)
        {
            uniqueChatId.EnglishWordFormUser[_chatId].Transcription = message;

            uniqueChatId.State[_chatId] = _nextState; //new InputTranslateState(_chatId, _configuration);
            await uniqueChatId.State[_chatId].Initialize();
        }

        public async Task Initialize()
        {
            await _configuration.SendMessageCommand.Execute(_chatId, "Input transcription:", ParseMode.Html, new ReplyKeyboardRemove());
        }
    }
}