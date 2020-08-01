using ConsoleTelegramBot;
using ConsoleTelegramBot.Command;
using ConsoleTelegramBot.Model;
using ConsoleTelegramBot.Operations;
using ConsoleTelegramBot.States;
using Moq;
using NLog;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Tests.Command
{
    public class SendMessageCommandTests
    {
        private Mock<ILogger> loggerMock;
        private Mock<IOperation> operationMock;
        private IConfiguration configuration;
        private Mock<ISendMessageCommand> sendMessageCommandMock;

        [SetUp]
        public void Initialize()
        {
            loggerMock = new Mock<ILogger>();
            operationMock = new Mock<IOperation>();
            sendMessageCommandMock = new Mock<ISendMessageCommand>();

            configuration = new Configuration(loggerMock.Object, operationMock.Object);
            configuration.SendMessageCommand = sendMessageCommandMock.Object;            
        }

        [TestCase(1)]
        public async Task ExecuteCommandTest(long chatId)
        {
            // Assign
            var configuration = new Mock<IConfiguration>();
            configuration.Setup(x => x.Bot = new Mock<ITelegramBotClient>());
            var command = new SendMessageCommand(configuration.Object);

            // Act
            await command.Execute(chatId, "test", ParseMode.Html, new ReplyKeyboardRemove());

            // Assert
            configuration.Verify(x => x.Bot.SendTextMessageAsync(It.IsAny<ChatId>(), It.IsAny<string>(), 
                                                                 It.IsAny<ParseMode>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<int>(),
                                                                 It.IsAny<IReplyMarkup>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}