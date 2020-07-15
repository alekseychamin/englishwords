using ConsoleTelegramBot.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace ConsoleTelegramBot.Command
{
    public class ShowCategoryCommand : INamedCommand
    {
        public string Name { get; }

        public string Description { get; }

        private readonly IConfiguration _configuration;

        public ShowCategoryCommand(string name, string description, IConfiguration configuration)
        {
            Name = name;
            Description = description;
            _configuration = configuration;
        }

        public async Task Execute(long chatId)
        {
            var result = await _configuration.WebClient.GetEntity(_configuration.UrlCategory);

            try
            {
                var categories = JsonSerializer.Deserialize<List<Category>>(result);

                if (categories is null)
                {
                    result = "Categories is null";
                    await _configuration.SendMessageCommand.Execute(chatId, result, ParseMode.Html, new ReplyKeyboardRemove());
                    return;
                }

                result = string.Empty;
                int totalCount = 0;
                foreach (var category in categories)
                {
                    result += $"*Id:* {category.id}\n*CategoryName:* {category.name}\n*Count words:* {category.count}\n\n";
                    totalCount += category.count;
                }

                result += $"*Total count words:* {totalCount}";

                await _configuration.SendMessageCommand.Execute(chatId, result, ParseMode.Markdown, new ReplyKeyboardRemove());
                return;
            }
            catch { }

            await _configuration.SendMessageCommand.Execute(chatId, result, ParseMode.Html, new ReplyKeyboardRemove());
        }
    }
}
