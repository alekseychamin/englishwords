using ConsoleTelegramBot.Command;
using ConsoleTelegramBot.States;
using ConsoleTelegramBot.Updates;
using NLog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Telegram.Bot;

namespace ConsoleTelegramBot
{
    public class Configuration : IConfiguration
    {
        public const string BotToken = "1144632945:AAFWHd0Um4WrUMI4Ijm-5pfkoJGK41cxahE";
        public const string UrlRandomWord = "http://localhost:5000/api/operation/randomword";        
        public const string UrlCategory = "http://localhost:5000/api/category";
        public const string UrlEnglishWord = "http://localhost:5000/api/englishword";

        public ILogger Logger { get; }
        public ISendMessageCommand SendMessageCommand { get; }
        public ICommand ShowAllCommand { get; }        
        public ITelegramBotClient Bot { get; }
        public IWebClient WebClient { get; }
        public IUpdate TextMessageUpdate { get; }
        public IUpdate CallBackQueryUpdate { get; }
        public IUpdate UnknownMessageUpdate { get; }
        public Dictionary<string, INamedCommand> ListCommand { get; }
        public List<IUniqueChatId> UniqueChatIds { get; }

        public Configuration()
        {
            Logger = LogManager.GetCurrentClassLogger();

            Bot = CreateTelegramBot(BotToken);
            WebClient = CreateWebClient(Logger);

            SendMessageCommand = CreateSendMessageCommand(this);
            ShowAllCommand = CreateShowAllCommand(this);            

            ListCommand = new Dictionary<string, INamedCommand>
            {
                { "/word", new RandomWordCommand("/word", "show random english word", this) },
                { "/newword", new NewWordCommand("/newword", "add new english word", this) },
                { "/editword", new EditWordCommand("/editword", "edit word by id", this) },
                { "/categories", new ShowCategoryCommand("/categories", "show categories", this) }                
            };

            UniqueChatIds = GetListUniqueChatId(ListCommand);

            TextMessageUpdate = CreateTextMessageUpdate(this);
            CallBackQueryUpdate = CreateCallBackQueryUpdate(this);
            UnknownMessageUpdate = CreateUnknownMessageUpdate(this);
        }

        private ITelegramBotClient CreateTelegramBot(string token)
        {
            return new TelegramBotClient(token);
        }

        private IWebClient CreateWebClient(ILogger logger)
        {
            return new WebClient(logger);
        }

        private ISendMessageCommand CreateSendMessageCommand(IConfiguration configuration)
        {
            return new SendMessageCommand(configuration);
        }

        private ICommand CreateShowAllCommand(IConfiguration configuration)
        {
            return new ShowAllCommand(configuration);
        }

        private IUpdate CreateTextMessageUpdate(IConfiguration configuration)
        {
            return new TextMessageUpdate(configuration);
        }

        private IUpdate CreateCallBackQueryUpdate(IConfiguration configuration)
        {
            return new CallBackQueryUpdate(configuration);
        }

        private IUpdate CreateUnknownMessageUpdate(IConfiguration configuration)
        {
            return new UnknownUpdate(configuration);
        }        

        private List<IUniqueChatId> GetListUniqueChatId(Dictionary<string, INamedCommand> listCommand)
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
