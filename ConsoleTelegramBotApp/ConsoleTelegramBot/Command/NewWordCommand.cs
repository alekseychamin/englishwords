using ConsoleTelegramBot.Model;
using ConsoleTelegramBot.States;
using NLog;
using NLog.Fluent;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace ConsoleTelegramBot.Command
{
    public class NewWordCommand : IUniqueChatId
    {
        public string Name { get; }

        public string Description { get; }

        private readonly IConfiguration _configuration;

        public HashSet<long> ListChatId { get; } = new HashSet<long>();
        public Dictionary<long, IState> State { get; } = new Dictionary<long, IState>();

        public Dictionary<long, NewEnglishWord> EnglishWordFormUser { get; } = new Dictionary<long, NewEnglishWord>();

        public NewWordCommand(string name, string description, IConfiguration configuration)
        {
            Name = name;
            Description = description;
            _configuration = configuration;
        }
        
        public async Task Execute(long chatId)
        {
            ListChatId.Add(chatId);

            EnglishWordFormUser.Add(chatId, new NewEnglishWord());
            State.Add(chatId, new InputWordState(chatId, _configuration));
                        
            await State[chatId].Initialize();
        }

        public async Task SaveInfoFromUser(long chatId, string message)
        {
            _configuration.Logger.Debug("Get message from user : {0}", message);

            await State[chatId].ChangeState(this, message);

            if (State.ContainsKey(chatId) == false)
                return;
            
            if (State[chatId] == null)
            {
                await CreateNewWord(chatId);

                RemoveChatId(chatId);
            }
        }

        public void RemoveChatId(long chatId)
        {
            ListChatId.Remove(chatId);
            EnglishWordFormUser.Remove(chatId);
            State.Remove(chatId);
        }

        private async Task CreateNewWord(long chatId)
        {
            var result = await _configuration.WebClient.PostNewWord(Configuration.UrlCreateWord, EnglishWordFormUser[chatId]);

            try
            {
                var englishWord = JsonSerializer.Deserialize<EnglisWord>(result);

                if (englishWord.id != 0)
                {
                    await _configuration.SendMessageCommand.Execute(chatId, "New word was created", 
                                                                    ParseMode.Html, new ReplyKeyboardRemove());

                    result = $"*Id:* {englishWord.id}\n\n*WordPhrase*: {englishWord.wordPhrase}\n\n*Transcription:* {englishWord.transcription}\n\n" +
                             $"*Translate:* {englishWord.translate}\n\n*Example:* {englishWord.example}\n\n*Category:* {englishWord.categoryName}";

                    await _configuration.SendMessageCommand.Execute(chatId, result, ParseMode.Markdown, new ReplyKeyboardRemove());
                    return;
                }
                else
                {
                    var validationError = JsonSerializer.Deserialize<ValidationError>(result);

                    string error = string.Empty;

                    error += (validationError.Id is null) ? string.Empty : validationError.Id[0] + "\n";
                    error += (validationError.WordPhrase is null) ? string.Empty : validationError.WordPhrase[0];
                    
                    result = $"New word was not created:\n{error}";

                    await _configuration.SendMessageCommand.Execute(chatId, result, ParseMode.Html, new ReplyKeyboardRemove());
                    return;
                }
            }
            catch { }

            await _configuration.SendMessageCommand.Execute(chatId, result, ParseMode.Html, new ReplyKeyboardRemove());
        }
    }
}
