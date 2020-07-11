﻿using ConsoleTelegramBot.Command;
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
    public class InputCategoryIdState : IState
    {
        private readonly IConfiguration _configuration;
        private long _chatId;
        private readonly IState _nextState;
        private bool _isInitialize;

        public InputCategoryIdState(long chatId, IConfiguration configuration, IState nextState, bool isInitialize = true)
        {
            _configuration = configuration;
            _chatId = chatId;
            _nextState = nextState;
            _isInitialize = isInitialize;
        }
        public async Task ChangeState(IUniqueChatId uniqueChatId, string message)
        {
            int categoryId;
            
            if (int.TryParse(message, out categoryId))
                uniqueChatId.EnglishWordFromUser[_chatId].CategoryId = categoryId;
            else
            {
                await _configuration.SendMessageCommand.Execute(_chatId, "CategoryId is invalid", ParseMode.Html, new ReplyKeyboardRemove());
                return;
            }

            uniqueChatId.State[_chatId] = _nextState; //null;

            if (_nextState is null)
                return;

            if (_isInitialize)
                await uniqueChatId.State[_chatId].Initialize();
            else
                await uniqueChatId.State[_chatId].ChangeState(uniqueChatId, message);
        }

        public async Task Initialize()
        {
            var listCategories = await Operation.GetListCategory(_chatId, _configuration);

            if (listCategories.Count == 0)
                return;

            List<List<InlineKeyboardButton>> listKeyBut = new List<List<InlineKeyboardButton>>();
            listKeyBut.Add(new List<InlineKeyboardButton>());

            foreach (var category in listCategories)
            {
                var inlineKeyBut =  InlineKeyboardButton.WithCallbackData(category.name, category.id.ToString());

                listKeyBut[0].Add(inlineKeyBut);
            }

            await _configuration.SendMessageCommand.Execute(_chatId, "Input category:", ParseMode.Html, new InlineKeyboardMarkup(listKeyBut));
        }
    }
}
