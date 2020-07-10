using ConsoleTelegramBot.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTelegramBot.States
{
    public class EditTranscriptionState : IState
    {
        private readonly IConfiguration _configuration;
        private long _chatId;

        public EditTranscriptionState(long chatId, IConfiguration configuration)
        {
            _chatId = chatId;
            _configuration = configuration;
        }
        public Task ChangeState(IUniqueChatId uniqueChatId, string message)
        {
            throw new NotImplementedException();
        }

        public Task Initialize()
        {
            throw new NotImplementedException();
        }
    }
}
