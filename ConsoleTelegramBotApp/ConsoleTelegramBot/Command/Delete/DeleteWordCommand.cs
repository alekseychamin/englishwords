using ConsoleTelegramBot.Model;
using ConsoleTelegramBot.Operations;
using ConsoleTelegramBot.States;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Exceptions;

namespace ConsoleTelegramBot.Command
{
    public class DeleteWordCommand : IUniqueChatId
    {
        public string Name { get; }

        public string Description { get; }        

        public HashSet<long> ListChatId { get; } = new HashSet<long>();

        public Dictionary<long, IState> State { get; } = new Dictionary<long, IState>();

        public Dictionary<long, NewEnglishWord> EnglishWordFromUser { get; } = new Dictionary<long, NewEnglishWord>();

        private int wordId;

        private readonly IConfiguration _configuration;
        private readonly IState _startState;

        public DeleteWordCommand(string name, string description, IConfiguration configuration, IState startState)
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

            State.Add(chatId, _startState);//new InputWordIdState(chatId, _configuration, null));

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
                await _configuration.Operation.DeleteEnglishWord(chatId, wordId, _configuration);

                RemoveChatId(chatId);
            }
        }

        public void SetId(int value)
        {
            wordId = value;
        }        
    }
}
