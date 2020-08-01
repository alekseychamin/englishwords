using ConsoleTelegramBot.Model;
using ConsoleTelegramBot.Operations;
using ConsoleTelegramBot.States;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace ConsoleTelegramBot.Command
{
    public class EditWordCommand : IUniqueChatId
    {
        public HashSet<long> ListChatId { get; } = new HashSet<long>();

        public Dictionary<long, IState> State { get; } = new Dictionary<long, IState>();

        public Dictionary<long, NewEnglishWord> EnglishWordFromUser { get; } = new Dictionary<long, NewEnglishWord>();

        public string Name { get; }

        public string Description { get; }

        private int wordId;

        private readonly IConfiguration _configuration;
        private readonly IState _startState;

        public EditWordCommand(string name, string description, IConfiguration configuration, IState startState)
        {
            Name = name;
            Description = description;
            _configuration = configuration;
            _startState = startState;
        }

        public async Task Execute(long chatId)
        {
            ListChatId.Add(chatId);

            _configuration.Operation.SetStateChatIdConfig(_startState, null, chatId, _configuration);
            
            State.Add(chatId, _startState);            

            await State[chatId].Initialize();
        }

        public void RemoveChatId(long chatId)
        {
            ListChatId.Remove(chatId);
            EnglishWordFromUser.Remove(chatId);            
            State.Remove(chatId);
        }

        public async Task SaveInfoFromUser(long chatId, string message)
        {
            _configuration.Logger.Debug("Get message from user : {0}", message);

            await State[chatId].ChangeState(this, message);

            if (State.ContainsKey(chatId) == false)
                return;

            if (State[chatId] == null)
            {
                await _configuration.Operation.EditEnglishWord(chatId, wordId, EnglishWordFromUser[chatId], _configuration);

                RemoveChatId(chatId);
            }
        }

        public async Task GetNewEnglishWordById(long chatId, int id)
        {
            var newEnglishWord = await _configuration.Operation.GetNewEnglishWordById(id, chatId, _configuration);

            if ((newEnglishWord is null) == false)
            {
                EnglishWordFromUser.Add(chatId, newEnglishWord);
            }
            else
            {
                await _configuration.SendMessageCommand.Execute(chatId, $"Word with id: {id} not found", 
                                                                ParseMode.Html, new ReplyKeyboardRemove());
                RemoveChatId(chatId);
            }
        }

        public void SetId(int value)
        {
            wordId = value;
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
            EnglishWordFromUser[chatId].Translate = value;
        }

        public void SetExample(long chatId, string value)
        {
            EnglishWordFromUser[chatId].Example = value;
        }

        public void SetCategoryId(long chatId, int value)
        {
            EnglishWordFromUser[chatId].CategoryId = value;
        }        

        public string GetWordName(long chatId)
        {
            return EnglishWordFromUser[chatId].WordPhrase;
        }

        public string GetTranscription(long chatId)
        {
            return EnglishWordFromUser[chatId].Transcription;
        }

        public string GetTranslate(long chatId)
        {
            return EnglishWordFromUser[chatId].Translate;
        }

        public string GetExample(long chatId)
        {
            return EnglishWordFromUser[chatId].Example;
        }

        public string GetCategoryName(long chatId)
        {
            return EnglishWordFromUser[chatId].CategoryName;
        }
    }
}
