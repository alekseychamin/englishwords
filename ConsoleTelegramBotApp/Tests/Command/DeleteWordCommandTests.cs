using ConsoleTelegramBot;
using ConsoleTelegramBot.Command;
using ConsoleTelegramBot.Operations;
using ConsoleTelegramBot.States;
using Moq;
using NLog;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Tests
{
    public class DeleteWordCommandTests
    {
        private Mock<ILogger> loggerMock;
        private Mock<IOperation> operationMock;
        private IConfiguration configuration;
        private Mock<ISendMessageCommand> sendMessageCommandMock;

        public DeleteWordCommandTests()
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
            var inputWordIdStateMock = new Mock<IState>();
            var deleteWordCommand = new DeleteWordCommand("/test", "test", configuration,
                                                              startState: inputWordIdStateMock.Object);

            // Act
            await deleteWordCommand.Execute(chatId);

            // Assert
            Assert.AreEqual(1, deleteWordCommand.ListChatId.Count);
            Assert.That(deleteWordCommand.ListChatId.Contains(chatId));

            Assert.AreEqual(1, deleteWordCommand.State.Count);
            Assert.That(deleteWordCommand.State.ContainsKey(chatId));

            inputWordIdStateMock.Verify(x => x.Initialize(), Times.Once);
        }

        [TestCase(1)]
        public async Task RemoveChatIdTest(long chatId)
        {
            // Assign
            var inputWordIdStateMock = new Mock<IState>();
            var deleteWordCommand = new DeleteWordCommand("/test", "test", configuration,
                                                          startState: inputWordIdStateMock.Object);

            // Act
            await deleteWordCommand.Execute(chatId);
            deleteWordCommand.RemoveChatId(chatId);

            // Assert
            Assert.AreEqual(0, deleteWordCommand.ListChatId.Count);
            Assert.That(deleteWordCommand.ListChatId.Contains(chatId) == false);

            Assert.AreEqual(0, deleteWordCommand.State.Count);
            Assert.That(deleteWordCommand.State.ContainsKey(chatId) == false);
        }

        [TestCase(1, "test")]
        public async Task SaveInfoFromUser_StateNotNull_Test(long chatId, string message)
        {
            // Assign
            var inputWordIdStateMock = new Mock<IState>();
            var deleteWordCommand = new DeleteWordCommand("/test", "test", configuration,
                                                          startState: inputWordIdStateMock.Object);

            // Act
            await deleteWordCommand.Execute(chatId);
            await deleteWordCommand.SaveInfoFromUser(chatId, message);

            // Assert
            inputWordIdStateMock.Verify(x => x.Initialize(), Times.Once);            
            inputWordIdStateMock.Verify(x => x.ChangeState(It.Is<IUniqueChatId>(x => x.Equals(deleteWordCommand)), 
                                                           It.IsAny<string>()), Times.Once);
        }

        [TestCase(1, "test")]
        public async Task SaveInfoFromUser_StateNull_Test(long chatId, string message)
        {
            // Assign
            var inputWordIdStateMock = new Mock<IState>();
            
            var deleteWordCommand = new DeleteWordCommand("/test", "test", configuration,
                                                              startState: inputWordIdStateMock.Object);

            inputWordIdStateMock.Setup(x => x.ChangeState(It.IsAny<IUniqueChatId>(), It.IsAny<string>()))
                                .Callback(() => { deleteWordCommand.State[chatId] = null; });

            // Act
            await deleteWordCommand.Execute(chatId);
            await deleteWordCommand.SaveInfoFromUser(chatId, message);

            // Assert
            inputWordIdStateMock.Verify(x => x.Initialize(), Times.Once);
            inputWordIdStateMock.Verify(x => x.ChangeState(It.Is<IUniqueChatId>(x => x.Equals(deleteWordCommand)),
                                                           It.IsAny<string>()), Times.Once);
            
            operationMock.Verify(x => x.DeleteEnglishWord(It.IsAny<long>(), It.IsAny<int>(), It.IsAny<IConfiguration>()), Times.Once);
        }
    }
}