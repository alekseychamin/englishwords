﻿using ConsoleTelegramBot.Operations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace ConsoleTelegramBot.Command
{
    public class RandomWordCommand : INamedCommand
    {
        public string Name { get; }

        public string Description { get; }

        private readonly IConfiguration _configuration;

        public RandomWordCommand(string name, string description, IConfiguration configuration)
        {
            Name = name;
            Description = description;
            _configuration = configuration;
        }

        public async Task Execute(long chatId)
        {
            await Operation.GetRandomEnglishWord(chatId, _configuration);
        }
    }
}