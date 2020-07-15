using ConsoleTelegramBot.Model;
using ConsoleTelegramBot.Operations;
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

        public Dictionary<long, NewEnglishWord> EnglishWordFromUser { get; } = new Dictionary<long, NewEnglishWord>();        

        public NewWordCommand(string name, string description, IConfiguration configuration)
        {
            Name = name;
            Description = description;
            _configuration = configuration;
        }
        
        public async Task Execute(long chatId)
        {
            ListChatId.Add(chatId);

            SetNewEnglishWord(chatId, new NewEnglishWord());
            
            State.Add(chatId, new InputWordNameState(chatId, _configuration, 
                                new InputTranscriptionState(chatId, _configuration, 
                                new InputTranslateState(chatId, _configuration,
                                new InputExampleState(chatId, _configuration,
                                new InputCategoryIdState(chatId, _configuration,
                                null))))));
                        
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
                await Operation.CreateNewWord(chatId, EnglishWordFromUser[chatId], _configuration);

                RemoveChatId(chatId);
            }
        }

        public void RemoveChatId(long chatId)
        {
            ListChatId.Remove(chatId);
            EnglishWordFromUser.Remove(chatId);
            State.Remove(chatId);
        }

        public void SetWordName(long chatId, string value)
        {
            EnglishWordFromUser[chatId].WordPhrase = value;
        }

        public void SetTranscription(long chatId, string value)
        {
            EnglishWordFromUser[chatId].Transcription = value;
        }

        public void SetTranslate(long chatId, string value)
        {
            EnglishWordFromUser[chatId].Translate= value;
        }

        public void SetExample(long chatId, string value)
        {
            EnglishWordFromUser[chatId].Example = value;
        }

        public void SetCategoryId(long chatId, int value)
        {
            EnglishWordFromUser[chatId].CategoryId = value;
        }

        public void SetNewEnglishWord(long chatId, NewEnglishWord newEnglishWord)
        {
            EnglishWordFromUser.Add(chatId, newEnglishWord);
        }
    }
}
