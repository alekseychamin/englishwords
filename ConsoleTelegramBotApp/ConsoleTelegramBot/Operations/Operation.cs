using ConsoleTelegramBot.Model;
using ConsoleTelegramBot.States;
using NLog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace ConsoleTelegramBot.Operations
{
    public class Operation : IOperation
    {
        private static int _categoryId;
        private readonly ILogger _logger;

        public Operation(ILogger logger)
        {
            _logger = logger;
        }

        private async Task<T> DeserializeResponse<T>(string response, long chatId, IConfiguration configuration)
        {
            try
            {
                var result = JsonSerializer.Deserialize<T>(response);

                return result;
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

                return default(T);
            }
        }

        public async Task<List<Category>> GetListCategory(long chatId, IConfiguration configuration)
        {
            var response = await configuration.WebClient.GetEntity(configuration.UrlCategory);

            return await DeserializeResponse<List<Category>>(response, chatId, configuration);
        }

        public async Task<NewCategory> GetNewCategoryById(int id, long chatId, IConfiguration configuration)
        {
            var urlPut = $"{configuration.UrlCategory}/{id}";

            var response = await configuration.WebClient.GetEntity(urlPut);

            var category = await DeserializeResponse<Category>(response, chatId, configuration);

            if ((category is null) == false)
            {
                return MapCategory(category);
            }

            return null;
        }

        public async Task<NewEnglishWord> GetNewEnglishWordById(int id, long chatId, IConfiguration configuration)
        {
            var urlPut = $"{configuration.UrlEnglishWord}/{id}";

            var response = await configuration.WebClient.GetEntity(urlPut);

            var englishWord = await DeserializeResponse<EnglishWord>(response, chatId, configuration);

            if ((englishWord is null) == false)
            {
                return MapEnglishWord(englishWord);
            }

            return null;
        }

        public void SetCategoryId(int categoryId)
        {
            _categoryId = categoryId;
        }

        public async Task EditCategory(long chatId, int id, NewCategory newCategory, IConfiguration configuration)
        {
            var url = $"{configuration.UrlCategory}/{id}";
            var response = await configuration.WebClient.PutEntity(url, newCategory);

            await ProcessResponseCategory(chatId, configuration, response, "Category was edited", "Category was not edit");
        }

        public async Task CreateNewCategory(long chatId, NewCategory newCategory, IConfiguration configuration)
        {
            var response = await configuration.WebClient.PostEntity(configuration.UrlCategory, newCategory);

            await ProcessResponseCategory(chatId, configuration, response, "New category was created", "New category was not created");
        }

        public async Task DeleteCategory(long chatId, int id, IConfiguration configuration)
        {
            var url = $"{configuration.UrlCategory}/{id}";
            var response = await configuration.WebClient.DeleteEntity(url);

            await configuration.SendMessageCommand.Execute(chatId, response, ParseMode.Html, new ReplyKeyboardRemove());
        }

        public async Task ProcessResponseCategory(long chatId, IConfiguration configuration, string response,
                                                         string successedMessage, string failMessage)
        {
            try
            {
                var category = JsonSerializer.Deserialize<Category>(response);

                if (category.id != 0)
                {
                    if (string.IsNullOrEmpty(successedMessage) == false)
                        await configuration.SendMessageCommand.Execute(chatId, successedMessage,
                                                                       ParseMode.Html, new ReplyKeyboardRemove());

                    response = $"*Id:* {category.id}\n\n*Name*: {category.name}\n\n*Count words:* {category.count}";

                    await configuration.SendMessageCommand.Execute(chatId, response, ParseMode.Markdown, new ReplyKeyboardRemove());
                    return;
                }
                else
                {
                    await ShowValidationError(chatId, failMessage, response, configuration);
                    return;
                }
            }
            catch { }

            await configuration.SendMessageCommand.Execute(chatId, response, ParseMode.Html, new ReplyKeyboardRemove());
        }

        public NewEnglishWord MapEnglishWord(EnglishWord englishWord)
        {
            if (englishWord is null)
                return null;

            var output = new NewEnglishWord
            {
                WordPhrase = englishWord.wordPhrase,
                Transcription = englishWord.transcription,
                Translate = englishWord.translate,
                Example = englishWord.example,
                CategoryName = englishWord.categoryName,
                CategoryId = englishWord.categoryId
            };

            return output;
        }

        public NewCategory MapCategory(Category category)
        {
            if (category is null)
                return null;

            var output = new NewCategory
            {
                Name = category.name
            };

            return output;
        }

        public async Task GetEnglishWordById(int id, long chatId, IConfiguration configuration)
        {
            var urlPut = $"{configuration.UrlEnglishWord}/{id}";

            var response = await configuration.WebClient.GetEntity(urlPut);

            await ProcessResponseEnglishWord(chatId, configuration, response, null, $"English word with id: {id} not found");
        }

        public async Task GetRandomEnglishWord(long chatId, IConfiguration configuration)
        {
            var url = $"{configuration.UrlRandomWord}?categoryId={_categoryId}";
            var response = await configuration.WebClient.GetEntity(url);

            await ProcessResponseEnglishWord(chatId, configuration, response, null, "English word not found");
        }

        private async Task ShowValidationError(long chatId, string message, string response, IConfiguration configuration)
        {
            var validationError = JsonSerializer.Deserialize<ValidationError>(response);

            string error = string.Empty;

            error += (validationError.Id is null) ? string.Empty : validationError.Id[0] + "\n";
            error += (validationError.WordPhrase is null) ? string.Empty : validationError.WordPhrase[0];

            var result = $"{message}:\n{error}";

            await configuration.SendMessageCommand.Execute(chatId, result, ParseMode.Html, new ReplyKeyboardRemove());
        }

        public async Task CreateNewWord(long chatId, NewEnglishWord newEnglishWord, IConfiguration configuration)
        {
            var response = await configuration.WebClient.PostEntity(configuration.UrlEnglishWord, newEnglishWord);

            await ProcessResponseEnglishWord(chatId, configuration, response, "New word was created", "New word was not created");
        }

        public async Task EditEnglishWord(long chatId, int id, NewEnglishWord newEnglishWord, IConfiguration configuration)
        {
            var url = $"{configuration.UrlEnglishWord}/{id}";
            var response = await configuration.WebClient.PutEntity(url, newEnglishWord);

            await ProcessResponseEnglishWord(chatId, configuration, response, "Word was edited", "Word was not edit");
        }

        public async Task DeleteEnglishWord(long chatId, int id, IConfiguration configuration)
        {
            var url = $"{configuration.UrlEnglishWord}/{id}";
            var response = await configuration.WebClient.DeleteEntity(url);

            await configuration.SendMessageCommand.Execute(chatId, response, ParseMode.Html, new ReplyKeyboardRemove());
        }

        public async Task ProcessResponseEnglishWord(long chatId, IConfiguration configuration, string response,
                                                            string successedMessage, string failMessage)
        {
            try
            {
                var englishWord = JsonSerializer.Deserialize<EnglishWord>(response);

                if (englishWord.id != 0)
                {
                    if (string.IsNullOrEmpty(successedMessage) == false)
                        await configuration.SendMessageCommand.Execute(chatId, successedMessage,
                                                                       ParseMode.Html, new ReplyKeyboardRemove());

                    response = $"*Id:* {englishWord.id}\n\n*WordPhrase*: {englishWord.wordPhrase}\n\n*Transcription:* {englishWord.transcription}\n\n" +
                               $"*Translate:* {englishWord.translate}\n\n*Example:* {englishWord.example}\n\n*Category:* {englishWord.categoryName}";

                    await configuration.SendMessageCommand.Execute(chatId, response, ParseMode.Markdown, new ReplyKeyboardRemove());
                    return;
                }
                else
                {
                    await ShowValidationError(chatId, failMessage, response, configuration);
                    return;
                }
            }
            catch { }

            await configuration.SendMessageCommand.Execute(chatId, response, ParseMode.Html, new ReplyKeyboardRemove());
        }

        public async Task MakeQuestion(long chatId, string message, IConfiguration configuration)
        {
            var inlineKeyboard = new InlineKeyboardMarkup(
                new[]
                {
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("Yes", "Yes"),
                        InlineKeyboardButton.WithCallbackData("No", "No")
                    }
                });

            await configuration.SendMessageCommand.Execute(chatId, message, ParseMode.Html, inlineKeyboard);
        }

        public void SetStateChatIdConfig(IState startState, IState stopState, long chatId, IConfiguration configuration)
        {
            var state = startState;
            do
            {
                state.ChatId = chatId;
                state.Configuration = configuration;

                state = state.NextState;
            } 
            while (Equals(state, stopState) == false);
        }
    }
}
