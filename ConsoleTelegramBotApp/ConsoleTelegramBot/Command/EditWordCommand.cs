﻿using ConsoleTelegramBot.Model;
using ConsoleTelegramBot.Operations;
using ConsoleTelegramBot.States;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTelegramBot.Command
{
    public class EditWordCommand : IUniqueChatId
    {
        public HashSet<long> ListChatId { get; } = new HashSet<long>();

        public Dictionary<long, IState> State { get; } = new Dictionary<long, IState>();

        public Dictionary<long, NewEnglishWord> EnglishWordFromUser { get; } = new Dictionary<long, NewEnglishWord>();

        public string Name { get; }

        public string Description { get; }
        
        public int WordId { get; set; }

        private readonly IConfiguration _configuration;

        public EditWordCommand(string name, string description, IConfiguration configuration)
        {
            Name = name;
            Description = description;
            _configuration = configuration;
        }

        public async Task Execute(long chatId)
        {
            ListChatId.Add(chatId);

            //EnglishWordFormUser.Add(chatId, new NewEnglishWord());
            State.Add(chatId, new InputWordIdState(chatId, _configuration,
                                new EditWordState(chatId, _configuration,
                                new EditTranscriptionState(chatId, _configuration,
                                new EditTranslateState(chatId, _configuration,
                                new EditExampleState(chatId, _configuration,
                                new EditCategoryIdState(chatId, _configuration,
                                null)))))));

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
                await Operation.EditEnglishWord(chatId, WordId, EnglishWordFromUser[chatId], _configuration);

                RemoveChatId(chatId);
            }
        }
    }
}
