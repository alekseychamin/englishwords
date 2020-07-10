using ConsoleTelegramBot.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace ConsoleTelegramBot.Operations
{
    public class Operation
    {
        public static async Task<List<Category>> GetListCategory(long chatId, IConfiguration configuration)
        {
            List<Category> listCategory = new List<Category>();

            var output = await configuration.WebClient.GetStringFromUrl(Configuration.UrlCategory);

            try
            {
                listCategory = JsonSerializer.Deserialize<List<Category>>(output);

                return listCategory;
            }
            catch (Exception)
            {
                foreach (var uniqueChatId in configuration.UniqueChatIds)
                {
                    if (uniqueChatId.ListChatId.Contains(chatId))
                    {
                        uniqueChatId.RemoveChatId(chatId);
                    }
                }

                await configuration.SendMessageCommand.Execute(chatId, output, ParseMode.Html, new ReplyKeyboardRemove());

                return listCategory;
            }
        }

        public static async Task<NewEnglishWord> GetEnglishWordById(int id, long chatId, IConfiguration configuration)
        {
            var urlPut = $"{Configuration.UrlEnglishWord}/{id}";

            var response = await configuration.WebClient.GetStringFromUrl(urlPut);

            try
            {
                var englishWord = JsonSerializer.Deserialize<EnglishWord>(response);

                if ((englishWord is null) == false)
                {
                    return MapEnglishWord(englishWord);
                }

                return null;
            }
            catch (Exception)
            {
                foreach (var uniqueChatId in configuration.UniqueChatIds)
                {
                    if (uniqueChatId.ListChatId.Contains(chatId))
                    {
                        uniqueChatId.RemoveChatId(chatId);
                    }
                }

                await configuration.SendMessageCommand.Execute(chatId, response, ParseMode.Html, new ReplyKeyboardRemove());

                return null;
            }
        }

        public static NewEnglishWord MapEnglishWord(EnglishWord englishWord)
        {
            if (englishWord is null)
                return null;

            var output = new NewEnglishWord {
                WordPhrase = englishWord.wordPhrase,
                Transcription = englishWord.transcription,
                Translate = englishWord.translate,
                Example = englishWord.example,
                CategoryName = englishWord.categoryName
            };

            return output;
        }

        public static async Task GetRandomEnglishWord(long chatId, IConfiguration configuration)
        {
            var result = await configuration.WebClient.GetStringFromUrl(Configuration.UrlRandomWord);

            try
            {
                var englishWord = JsonSerializer.Deserialize<EnglishWord>(result);

                if (englishWord is null)
                {
                    result = "English word is null";
                    await configuration.SendMessageCommand.Execute(chatId, result,
                                                                   ParseMode.Html, new ReplyKeyboardRemove());
                    return;
                }

                result = $"*Id:* {englishWord.id}\n\n*WordPhrase*: {englishWord.wordPhrase}\n\n*Transcription:* {englishWord.transcription}\n\n" +
                         $"*Translate:* {englishWord.translate}\n\n*Example:* {englishWord.example}\n\n*Category:* {englishWord.categoryName}";

                await configuration.SendMessageCommand.Execute(chatId, result, ParseMode.Markdown, new ReplyKeyboardRemove());
                return;

            }
            catch { }

            await configuration.SendMessageCommand.Execute(chatId, result, ParseMode.Html, new ReplyKeyboardRemove());
        }

        public static async Task ShowValidationError(long chatId, string message, string response, IConfiguration configuration)
        {
            var validationError = JsonSerializer.Deserialize<ValidationError>(response);

            string error = string.Empty;

            error += (validationError.Id is null) ? string.Empty : validationError.Id[0] + "\n";
            error += (validationError.WordPhrase is null) ? string.Empty : validationError.WordPhrase[0];

            var result = $"{message}:\n{error}";

            await configuration.SendMessageCommand.Execute(chatId, result, ParseMode.Html, new ReplyKeyboardRemove());
        }

        public static async Task CreateNewWord(long chatId, NewEnglishWord newEnglishWord, IConfiguration configuration)
        {
            var response = await configuration.WebClient.PostNewWord(Configuration.UrlEnglishWord, newEnglishWord);

            try
            {
                var englishWord = JsonSerializer.Deserialize<EnglishWord>(response);

                if (englishWord.id != 0)
                {
                    await configuration.SendMessageCommand.Execute(chatId, "New word was created",
                                                                    ParseMode.Html, new ReplyKeyboardRemove());

                    response = $"*Id:* {englishWord.id}\n\n*WordPhrase*: {englishWord.wordPhrase}\n\n*Transcription:* {englishWord.transcription}\n\n" +
                             $"*Translate:* {englishWord.translate}\n\n*Example:* {englishWord.example}\n\n*Category:* {englishWord.categoryName}";

                    await configuration.SendMessageCommand.Execute(chatId, response, ParseMode.Markdown, new ReplyKeyboardRemove());
                    return;
                }
                else
                {
                    await Operation.ShowValidationError(chatId, "New word was not created", response, configuration);
                    return;
                }
            }
            catch { }

            await configuration.SendMessageCommand.Execute(chatId, response, ParseMode.Html, new ReplyKeyboardRemove());
        }

        public static async Task MakeQuestion(long chatId, string message, IConfiguration configuration)
        {
            var inlineKeyboard = new InlineKeyboardMarkup(
                new[]
                {
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("Yes", "1"),
                        InlineKeyboardButton.WithCallbackData("No", "0")
                    }
                });

            await configuration.SendMessageCommand.Execute(chatId, message, ParseMode.Html, inlineKeyboard);
        }
    }
}
