using ConsoleTelegramBot.States;
using NLog;
using NLog.Fluent;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace ConsoleTelegramBot.Command
{
    public class NewWordCommand : IUniqueChatId
    {
        public string Name { get; }

        public string Description { get; }

        public HashSet<long> ListChatId { get; } = new HashSet<long>();
        public Dictionary<long, IState> State { get; } = new Dictionary<long, IState>();

        public Dictionary<long, List<string>> MessagesFromUser { get; } = new Dictionary<long, List<string>>();

        private readonly ICommand _sendMessageCommand;
        private readonly ILogger _logger;
        private readonly IWebClient _webClient;

        public NewWordCommand(string name, string description, ICommand sendMessageCommand, 
                              ILogger logger, IWebClient webClient)
        {
            Name = name;
            Description = description;
            _sendMessageCommand = sendMessageCommand;
            _logger = logger;
            _webClient = webClient;
        }
        
        public async Task Execute(long chatId, string text = null)
        {
            ListChatId.Add(chatId);

            MessagesFromUser.Add(chatId, new List<string>());
            State.Add(chatId, new InputWordState(chatId, _sendMessageCommand));
                        
            await State[chatId].Initialize();
        }

        public async Task SaveInfoFromUser(long chatId, string message)
        {
            _logger.Debug("Get message from user : {0}", message);

            await State[chatId].ChangeState(this, message);
            
            if (State[chatId] == null)
            {
                await CreateNewWord(chatId);

                ListChatId.Remove(chatId);
                MessagesFromUser.Remove(chatId);
                State.Remove(chatId);
            }
        }

        private async Task CreateNewWord(long chatId)
        {
            string result = string.Join('\n', MessagesFromUser[chatId]);

            await _sendMessageCommand.Execute(chatId, $"Input info:\n{result}");            
        }
    }
}
