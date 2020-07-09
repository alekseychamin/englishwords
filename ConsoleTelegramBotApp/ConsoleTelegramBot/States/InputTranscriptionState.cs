using ConsoleTelegramBot.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTelegramBot.States
{
    public class InputTranscriptionState : IState
    {
        private readonly ICommand _sendMessageCommand;
        private long _chatId;

        public InputTranscriptionState(long chatId, ICommand sendMessageCommand)
        {
            _sendMessageCommand = sendMessageCommand;
            _chatId = chatId;
        }

        public async Task ChangeState(IUniqueChatId uniqueChatId, string message)
        {
            uniqueChatId.MessagesFromUser[_chatId].Add(message);

            uniqueChatId.State[_chatId] = new InputTranslateState(_chatId, _sendMessageCommand);
            await uniqueChatId.State[_chatId].Initialize();
        }

        public async Task Initialize()
        {
            await _sendMessageCommand.Execute(_chatId, "Input transcription:");
        }
    }
}
