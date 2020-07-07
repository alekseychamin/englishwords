using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTelegramBot.Command
{
    public class ShowAllCommand : ICommand
    {
        public string Name { get; }

        public string Description { get; }

        private readonly List<ICommand> _commands;
        private readonly ICommand _sendMessageCommand;

        public ShowAllCommand(string name, string description, 
                              List<ICommand> commands, ICommand sendMessageCommand)
        {
            Name = name;
            Description = description;
            _commands = commands;
            _sendMessageCommand = sendMessageCommand;
        }
        
        public async Task Execute(long chatId, string text = null)
        {
            string result = "Commands:\n";

            foreach (var command in _commands)
            {
                result += $"{command.Name} - {command.Description}\n";
            }
            
            await _sendMessageCommand.Execute(chatId, result);
        }
    }
}
