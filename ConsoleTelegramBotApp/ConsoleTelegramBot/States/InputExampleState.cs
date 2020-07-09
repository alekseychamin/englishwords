using ConsoleTelegramBot.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTelegramBot.States
{
    public class InputExampleState : IState
    {
        private long _chatId;
        private readonly ICommand _sendMessageCommand;

        public InputExampleState(long chatId, ICommand sendMessageCommand)
        {
            _chatId = chatId;
            _sendMessageCommand = sendMessageCommand;
        }
        public Task ChangeState(IUniqueChatId uniqueChatId, string message)
        {
            throw new NotImplementedException();
        }

        public async Task Initialize()
        {
            await _sendMessageCommand.Execute(_chatId, "Input example:");
        }
    }
}
