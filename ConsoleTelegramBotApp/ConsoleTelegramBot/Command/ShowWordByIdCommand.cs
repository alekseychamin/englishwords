﻿using ConsoleTelegramBot.Model;
using ConsoleTelegramBot.Operations;
using ConsoleTelegramBot.States;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTelegramBot.Command
{
    public class ShowWordByIdCommand : IUniqueChatId
    {
        public HashSet<long> ListChatId { get; } = new HashSet<long>();

        public Dictionary<long, IState> State { get; } = new Dictionary<long, IState>();        

        public string Name { get; }

        public string Description { get; }

        private int wordId;

        private readonly IConfiguration _configuration;

        public ShowWordByIdCommand(string name, string description, IConfiguration configuration)
        {
            Name = name;
            Description = description;
            _configuration = configuration;
        }
        
        public async Task Execute(long chatId)
        {
            ListChatId.Add(chatId);

            State.Add(chatId, new InputWordIdState(chatId, _configuration, null));

            await State[chatId].Initialize();
        }

        public void RemoveChatId(long chatId)
        {
            ListChatId.Remove(chatId);            
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
                await Operation.GetEnglishWordById(wordId, chatId, _configuration);

                RemoveChatId(chatId);
            }
        }

        public void SetId(int value)
        {
            wordId = value;
        }
    }
}
