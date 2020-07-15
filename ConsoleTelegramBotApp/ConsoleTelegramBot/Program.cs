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
using System.Collections.Generic;
using ConsoleTelegramBot.Command;
using ConsoleTelegramBot.Updates;

namespace ConsoleTelegramBot
{
    class Program
    {
        private static long _chatId = -1;
        private static ILogger _logger;
        private static ISendMessageCommand _sendMessageCommand;        
        private static ITelegramBotClient _bot;        
        private static IUpdate _textMessageUpdate;
        private static IUpdate _callBackQueryUpdate;
        private static IUpdate _uknownUpdate;
        private static IConfiguration configuration; 

        static async Task Main(string[] args)
        {
            configuration = new Configuration();

            _logger = configuration.Logger;
            
            _logger.Debug("Init main");

            _bot = configuration.Bot;            

            var user = await _bot.GetMeAsync();
            Console.Title = user.Username;

            var cts = new CancellationTokenSource();

            _textMessageUpdate = configuration.TextMessageUpdate;
            _callBackQueryUpdate = configuration.CallBackQueryUpdate;
            _uknownUpdate = configuration.UnknownMessageUpdate;

            _sendMessageCommand = configuration.SendMessageCommand;                        

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

        private static async Task HandleUpdateAsync(ITelegramBotClient bot, Update update, CancellationToken cancellationToken)
        {
            var handler = update.Type switch
            {
                UpdateType.Message => _textMessageUpdate.ProcessUpdate(update),
                UpdateType.CallbackQuery => _callBackQueryUpdate.ProcessUpdate(update),
                _ => _uknownUpdate.ProcessUpdate(update)
            };

            try
            {
                await handler;
            }
            catch (Exception ex)
            {
                _chatId = update.Message?.Chat.Id ?? -1;
                await HandleErrorAsync(bot, ex, cancellationToken);

                if ((ex is ApiRequestException) == false)
                    throw;
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

            await _sendMessageCommand.Execute(_chatId, errorMessage, ParseMode.Html, new ReplyKeyboardRemove());
        }
    }
}
