using ConsoleTelegramBot.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTelegramBot.States
{
    public class InputTranslateState : IState
    {
        private long _chatId;
        private readonly ICommand _sendMessageCommand;

        public InputTranslateState(long chatId, ICommand sendMessageCommand)
        {
            _chatId = chatId;
            _sendMessageCommand = sendMessageCommand;
        }
        public async Task ChangeState(IUniqueChatId uniqueChatId, string message)
        {
            uniqueChatId.MessagesFromUser[_chatId].Add(message);

            uniqueChatId.State[_chatId] = new InputExampleState(_chatId, _sendMessageCommand);
            await uniqueChatId.State[_chatId].Initialize();
        }

        public async Task Initialize()
        {
            await _sendMessageCommand.Execute(_chatId, "Input translate:");
        }
    }
}
