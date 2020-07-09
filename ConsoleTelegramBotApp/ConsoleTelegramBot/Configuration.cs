using ConsoleTelegramBot.Command;
using ConsoleTelegramBot.Updates;
using NLog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Telegram.Bot;

namespace ConsoleTelegramBot
{
    public static class Configuration
    {
        public readonly static string BotToken = "1144632945:AAFWHd0Um4WrUMI4Ijm-5pfkoJGK41cxahE";
        public readonly static string UrlRandomWord = "http://localhost:5000/api/operation/randomword";
        public readonly static string UrlCreateWord = "http://localhost:5000/api/englishword";

        public static ILogger Logger { get; }
        public static ICommand SendMessageCommand { get; }
        public static ICommand ShowAllCommand { get; }
        public static ITelegramBotClient Bot { get; }
        public static IWebClient WebClient { get; }
        public static IUpdate TextMessageUpdate { get; }
        public static IUpdate UnknownMessageUpdate { get; }
        public static Dictionary<string, ICommand> ListCommand { get; }        

        static Configuration()
        {
            Logger = LogManager.GetCurrentClassLogger();

            Bot = new TelegramBotClient(BotToken);            
            WebClient = new WebClient(Logger);
            
            SendMessageCommand = new SendMessageCommand("sendMessage", null, Bot, Logger);
            
            ListCommand = new Dictionary<string, ICommand>
            {
                { "/word", new RandomWordCommand("/word", "show random english word", WebClient, SendMessageCommand) },
                { "/newword", new NewWordCommand("/newword", "add new english word", SendMessageCommand, Logger, WebClient) }
            };

            var listUniqueChatId = GetListUniqueChatId(ListCommand);

            ShowAllCommand = new ShowAllCommand("showAllCommand", null, ListCommand, SendMessageCommand);            

            TextMessageUpdate = new TextMessageUpdate(ShowAllCommand, ListCommand, Logger, listUniqueChatId);
            UnknownMessageUpdate = new UnknownUpdate(SendMessageCommand, Logger);
        }

        private static List<IUniqueChatId> GetListUniqueChatId(Dictionary<string, ICommand> listCommand)
        {
            var listUniqueChatId = new List<IUniqueChatId>();

            foreach (var item in listCommand)
            {
                if (item.Value is IUniqueChatId)
                    listUniqueChatId.Add(item.Value as IUniqueChatId);
            }

            return listUniqueChatId;
        }
    }
}
