using ConsoleTelegramBot.Model;
using ConsoleTelegramBot.Operations;
using ConsoleTelegramBot.States;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTelegramBot.Command
{
    public class DeleteCategoryCommand : IUniqueChatId
    {
        public HashSet<long> ListChatId { get; } = new HashSet<long>();

        public Dictionary<long, IState> State { get; } = new Dictionary<long, IState>();        

        public Dictionary<long, NewCategory> CategoryFromUser { get; } = new Dictionary<long, NewCategory>();

        public string Name { get; }

        public string Description { get; }

        private int categoryId;

        private readonly IConfiguration _configuration;
        private readonly IState _startState;

        public DeleteCategoryCommand(string name, string description, IConfiguration configuration, IState startState)
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

            State.Add(chatId, _startState);//new InputCategoryIdState(chatId, _configuration, null));

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
                await _configuration.Operation.DeleteCategory(chatId, categoryId, _configuration);

                RemoveChatId(chatId);
            }
        }

        public void SetCategoryId(long chatId, int value)
        {
            categoryId = value;
        }
    }
}
