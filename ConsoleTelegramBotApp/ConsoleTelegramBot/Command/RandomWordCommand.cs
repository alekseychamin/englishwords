﻿using System;
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
            var result = await _configuration.WebClient.GetStringFromUrl(Configuration.UrlRandomWord);

            try
            {
                var englishWord = JsonSerializer.Deserialize<EnglisWord>(result);

                if (englishWord is null)
                {
                    result = "English word is null";
                    await _configuration.SendMessageCommand.Execute(chatId, result, 
                                                                    ParseMode.Html, new ReplyKeyboardRemove());
                    return;
                }

                result = $"*Id:* {englishWord.id}\n\n*WordPhrase*: {englishWord.wordPhrase}\n\n*Transcription:* {englishWord.transcription}\n\n" +
                         $"*Translate:* {englishWord.translate}\n\n*Example:* {englishWord.example}\n\n*Category:* {englishWord.categoryName}";

                await _configuration.SendMessageCommand.Execute(chatId, result, ParseMode.Markdown, new ReplyKeyboardRemove());
                return;

            }
            catch { }

            await _configuration.SendMessageCommand.Execute(chatId, result, ParseMode.Html, new ReplyKeyboardRemove());
        }
    }
}
