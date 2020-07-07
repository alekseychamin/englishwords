using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsoleTelegramBot.Command
{
    public class EnglishWordCommand : ICommand
    {
        private readonly IWebClient _webClient;
        private readonly ICommand _sendMessageCommand;

        public string Name { get; }

        public string Description { get; }

        public EnglishWordCommand(string name, string description, 
                                  IWebClient webClient,
                                  ICommand sendMessageCommand)
        {
            Name = name;
            Description = description;
            _webClient = webClient;
            _sendMessageCommand = sendMessageCommand;
        }

        public async Task Execute(long chatId, string text = null)
        {
            var result = await _webClient.GetRandomWord(Configuration.UrlRandomWord);

            try
            {
                var englishWord = JsonSerializer.Deserialize<EnglisWord>(result);

                if (englishWord is null)
                {
                    result = "English word is null";
                    await _sendMessageCommand.Execute(chatId, result);
                    return;
                }

                result = $"*Id:* {englishWord.id}\n\n*WordPhrase*: {englishWord.wordPhrase}\n\n*Transcription:* {englishWord.transcription}\n\n" +
                         $"*Translate:* {englishWord.translate}\n\n*Example:* {englishWord.example}\n\n*Category:* {englishWord.categoryName}";

            }
            catch { }

            await _sendMessageCommand.Execute(chatId, result);
        }
    }
}
