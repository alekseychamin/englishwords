using ConsoleTelegramBot.Command;
using ConsoleTelegramBot.Operations;
using ConsoleTelegramBot.States;
using ConsoleTelegramBot.Updates;
using NLog;
using NLog.Fluent;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading;
using Telegram.Bot;

namespace ConsoleTelegramBot
{
    public class Configuration : IConfiguration
    {
        private ISendMessageCommand sendMessageCommand;

        public string BotToken { get; private set; }
        public string UrlRandomWord { get; private set; }
        public string UrlCategory { get; private set; }
        public string UrlEnglishWord { get; private set; }

        public ILogger Logger { get; private set; }
        public ISendMessageCommand SendMessageCommand 
        {
            get 
            {
                if (sendMessageCommand is null)
                    throw new ArgumentNullException(nameof(sendMessageCommand));
                
                return sendMessageCommand;
            }
            set 
            {
                if (value is null)
                    throw new ArgumentNullException(nameof(sendMessageCommand));

                sendMessageCommand = value;
            } 
        }
        public ICommand ShowAllCommand { get; }        
        public ITelegramBotClient Bot { get; }
        public IWebClient WebClient { get; }
        public IUpdate TextMessageUpdate { get; }
        public IUpdate CallBackQueryUpdate { get; }
        public IUpdate UnknownMessageUpdate { get; }
        public Dictionary<string, INamedCommand> ListCommand { get; }
        public List<IUniqueChatId> UniqueChatIds { get; }

        public IOperation Operation { get; }

        public Configuration(ILogger logger, IOperation operation)
        {
            Logger = logger;

            Operation = operation;

            SetAppSettingsFromJsonFile("appsettings.json");

            Bot = CreateTelegramBot(BotToken);
            WebClient = CreateWebClient(Logger);

            //SendMessageCommand = sendMessageCommand;//CreateSendMessageCommand(this);
            ShowAllCommand = CreateShowAllCommand(this);

            ListCommand = new Dictionary<string, INamedCommand>
            {
                { "/w", new ShowRandomWordCommand("/w", "show random english word", this) },
                
                { "/wid", new ShowWordByIdCommand("/wid", "show english word by id", this, startState: new InputWordIdState(nextState: null)) },
                
                { "/nw", new NewWordCommand("/nw", "add new english word", this, startState: new InputWordNameState(nextState:
                                                                                             new InputTranscriptionState(nextState: 
                                                                                             new InputTranslateState(nextState:
                                                                                             new InputExampleState(nextState:
                                                                                             new InputCategoryIdState(nextState: null)))))) },
                
                { "/ew", new EditWordCommand("/ew", "edit word by id", this, startState: new InputWordIdState(nextState: 
                                                                                         new EditWordState(nextState:
                                                                                         new EditTranscriptionState(nextState:
                                                                                         new EditTranslateState(nextState:
                                                                                         new EditExampleState(nextState:
                                                                                         new EditCategoryIdState(nextState: null))))))) },                
                
                { "/dw", new DeleteWordCommand("/dw", "delete word by id", this, new InputWordIdState(nextState: null)) },
                
                { "/c", new ShowCategoryCommand("/c", "show categories", this) },
                
                { "/nc", new NewCategoryCommand("/nc", "add new category", this, startState: new InputCategoryNameState(nextState: null)) },
                
                { "/ec", new EditCategoryNameCommand("/ec", "edit name category", this, startState: new InputCategoryIdState(nextState:
                                                                                                    new InputCategoryNameState(nextState: null))) },
                
                { "/dc", new DeleteCategoryCommand("/dc", "delete category", this, new InputCategoryIdState(nextState: null)) },
                
                { "/sc", new SetCategoryIdCommand("/sc", "set category for showing words", this, startState: new InputCategoryIdState(nextState: null)) },
                
                { "/cc", new ClearCategoryIdCommand("/cc", "clear category for showing words", this) }
            };

            UniqueChatIds = GetListUniqueChatId(ListCommand);

            TextMessageUpdate = CreateTextMessageUpdate(this);
            CallBackQueryUpdate = CreateCallBackQueryUpdate(this);
            UnknownMessageUpdate = CreateUnknownMessageUpdate(this);
        }
       
        private void SetAppSettingsFromJsonFile(string fileName)
        {
            var fullFileName = $"{AppDomain.CurrentDomain.BaseDirectory}\\{fileName}";            

            try
            {
                var jsonStr = File.ReadAllText(fullFileName);

                var appSetting = JsonSerializer.Deserialize<SettingModel>(jsonStr);

                BotToken = appSetting.BotToken;
                UrlRandomWord = appSetting.UrlRandomWord;
                UrlCategory = appSetting.UrlCategory;
                UrlEnglishWord = appSetting.UrlEnglishWord;
            }
            catch (Exception ex)
            {
                Logger.Error($"Error ocuried during reading app settings: {ex}");
                throw;
            }
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
