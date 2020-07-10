using ConsoleTelegramBot.Command;
using ConsoleTelegramBot.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace ConsoleTelegramBot.States
{
    public class InputCategoryState : IState
    {
        private readonly IConfiguration _configuration;
        private long _chatId;

        public InputCategoryState(long chatId, IConfiguration configuration)
        {
            _configuration = configuration;
            _chatId = chatId;
        }
        public async Task ChangeState(IUniqueChatId uniqueChatId, string message)
        {
            int categoryId;
            
            if (int.TryParse(message, out categoryId))
                uniqueChatId.EnglishWordFormUser[_chatId].CategoryId = categoryId;
            else
            {
                await _configuration.SendMessageCommand.Execute(_chatId, "CategoryId is invalid", ParseMode.Html, new ReplyKeyboardRemove());
                return;
            }

            uniqueChatId.State[_chatId] = null;
        }

        public async Task Initialize()
        {
            var listCategories = await GetListCategory();

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

        private async Task<List<Category>> GetListCategory()
        {
            List<Category> listCategory = new List<Category>();

            var output = await _configuration.WebClient.GetStringFromUrl(Configuration.UrlCategory);

            try
            {
                listCategory = JsonSerializer.Deserialize<List<Category>>(output);

                return listCategory;
            }
            catch (Exception)
            {
                foreach (var uniqueChatId in _configuration.UniqueChatIds)
                {
                    if (uniqueChatId.ListChatId.Contains(_chatId))
                    {
                        uniqueChatId.RemoveChatId(_chatId);                        
                    }
                }

                await _configuration.SendMessageCommand.Execute(_chatId, output, ParseMode.Markdown, new ReplyKeyboardRemove());

                return listCategory;
            }
        }
    }
}
