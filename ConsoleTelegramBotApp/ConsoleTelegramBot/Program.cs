using NLog;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using System.Text.Json;

namespace ConsoleTelegramBot
{
    class Program
    {
        private static long _chatId = -1;
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private static ITelegramBotClient _bot;
        private static IWebClient _webClient;

        static async Task Main(string[] args)
        {
            _logger.Debug("Init main");

            _bot = new TelegramBotClient(Configuration.BotToken);
            _webClient = new WebClient(_logger);

            var user = await _bot.GetMeAsync();
            Console.Title = user.Username;

            var cts = new CancellationTokenSource();

            // StartReceiving does not block the caller thread. Receiving is done on the ThreadPool.
            _bot.StartReceiving(
                new DefaultUpdateHandler(HandleUpdateAsync, HandleErrorAsync),
                cts.Token
            );

            _logger.Debug("Start listening for @{0}", user.Username);
            Console.ReadLine();

            // Send cancellation request to stop bot
            cts.Cancel();
        }

        public static async Task HandleUpdateAsync(ITelegramBotClient bot, Update update, CancellationToken cancellationToken)
        {
            var handler = update.Type switch
            {
                UpdateType.Message => BotOnMessageReceived(update.Message),
                _ => UnknownUpdateHandlerAsync(update)
            };

            try
            {
                await handler;
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(bot, ex, cancellationToken);
            }
        }

        private static async Task UnknownUpdateHandlerAsync(Update update)
        {
            _logger.Error("Unknown update type: {0}", update.Type);
            
            await SendMessage(_chatId, $"Unknown update type: {update.Type}");
        }

        private static async Task SendMessage(long chatId, string text)
        {
            if (chatId == -1)
            {
                _logger.Error("Can`t send message to bot, ChatId is -1");
                return;
            }
            
            await _bot.SendTextMessageAsync(
                        chatId: chatId,
                        parseMode: ParseMode.Markdown,
                        text: text,
                        replyMarkup: new ReplyKeyboardRemove()
             );

            _chatId = chatId;
        }

        private static async Task BotOnMessageReceived(Message message)
        {
            _logger.Debug("Message form Telegram: {0}", message.Text);

            if (message.Type != MessageType.Text)
                return;

            var action = (message.Text.Split(' ').First()) switch
            {
                "/word" => GetRandomEnglishWord(message),
                _ => ShowCommand(message)
            };

            await action;

            static async Task ShowCommand(Message message)
            {
                const string commdands = "Commands:\n" +
                                        "/word     - show random english word\n" +
                                        "/newword  - add new english word\n" +
                                        "/addwords - add english words from csv file";

                await SendMessage(message.Chat.Id, commdands);
            }

            static async Task GetRandomEnglishWord(Message message)
            {                
                var result = await _webClient.GetRandomWord(Configuration.UrlRandomWord);

                try
                {
                    var englishWord = JsonSerializer.Deserialize<EnglisWord>(result);

                    if (englishWord is null)
                    {
                        result = "English word is null";
                        await SendMessage(message.Chat.Id, result);
                        return;
                    }

                    result = $"*Id:* {englishWord.id}\n\n*WordPhrase*: {englishWord.wordPhrase}\n\n*Transcription:* {englishWord.transcription}\n\n" +
                             $"*Translate:* {englishWord.translate}\n\n*Example:* {englishWord.example}\n\n*Category:* {englishWord.categoryName}";
                    
                }
                catch { }

                await SendMessage(message.Chat.Id, result);
            }
        }

        private static async Task HandleErrorAsync(ITelegramBotClient bot, Exception ex, CancellationToken cancellationToken)
        {
            var errorMessage = ex switch
            {
                ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => ex.Message
            };

            _logger.Error(errorMessage);

            await SendMessage(_chatId, errorMessage);
        }
    }
}
