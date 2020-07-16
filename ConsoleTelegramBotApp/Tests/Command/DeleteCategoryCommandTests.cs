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
    public class DeleteCategoryCommandTests
    {
        private Mock<ILogger> loggerMock;
        private Mock<IOperation> operationMock;
        private IConfiguration configuration;
        private Mock<ISendMessageCommand> sendMessageCommandMock;

        public DeleteCategoryCommandTests()
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
            var inputCategoryIdStateMock = new Mock<IState>();
            var deleteCategoryCommand = new DeleteCategoryCommand("/test", "test", configuration,
                                                                  startState: inputCategoryIdStateMock.Object);

            // Act
            await deleteCategoryCommand.Execute(chatId);

            // Assert
            Assert.AreEqual(1, deleteCategoryCommand.ListChatId.Count);
            Assert.That(deleteCategoryCommand.ListChatId.Contains(chatId));

            Assert.AreEqual(1, deleteCategoryCommand.State.Count);
            Assert.That(deleteCategoryCommand.State.ContainsKey(chatId));

            inputCategoryIdStateMock.Verify(x => x.Initialize(), Times.Once);
        }

        [TestCase(1)]
        public async Task RemoveChatIdTest(long chatId)
        {
            // Assign
            var inputCategoryIdStateMock = new Mock<IState>();
            var deleteCategoryCommand = new DeleteCategoryCommand("/test", "test", configuration,
                                                                  startState: inputCategoryIdStateMock.Object);

            // Act
            await deleteCategoryCommand.Execute(chatId);
            deleteCategoryCommand.RemoveChatId(chatId);

            // Assert
            Assert.AreEqual(0, deleteCategoryCommand.ListChatId.Count);
            Assert.That(deleteCategoryCommand.ListChatId.Contains(chatId) == false);

            Assert.AreEqual(0, deleteCategoryCommand.State.Count);
            Assert.That(deleteCategoryCommand.State.ContainsKey(chatId) == false);
        }

        [TestCase(1, "test")]
        public async Task SaveInfoFromUser_StateNotNull_Test(long chatId, string message)
        {
            // Assign
            var inputCategoryIdStateMock = new Mock<IState>();
            var deleteCategoryCommand = new DeleteCategoryCommand("/test", "test", configuration,
                                                                  startState: inputCategoryIdStateMock.Object);

            // Act
            await deleteCategoryCommand.Execute(chatId);
            await deleteCategoryCommand.SaveInfoFromUser(chatId, message);

            // Assert
            inputCategoryIdStateMock.Verify(x => x.Initialize(), Times.Once);            
            inputCategoryIdStateMock.Verify(x => x.ChangeState(It.Is<IUniqueChatId>(x => x.Equals(deleteCategoryCommand)), 
                                                               It.IsAny<string>()), Times.Once);
        }

        [TestCase(1, "test")]
        public async Task SaveInfoFromUser_StateNull_Test(long chatId, string message)
        {
            // Assign
            var inputCategoryIdStateMock = new Mock<IState>();
            
            var deleteCategoryCommand = new DeleteCategoryCommand("/test", "test", configuration,
                                                                  startState: inputCategoryIdStateMock.Object);

            inputCategoryIdStateMock.Setup(x => x.ChangeState(It.IsAny<IUniqueChatId>(), It.IsAny<string>()))
                                    .Callback(() => { deleteCategoryCommand.State[chatId] = null; });

            // Act
            await deleteCategoryCommand.Execute(chatId);
            await deleteCategoryCommand.SaveInfoFromUser(chatId, message);

            // Assert
            inputCategoryIdStateMock.Verify(x => x.Initialize(), Times.Once);
            inputCategoryIdStateMock.Verify(x => x.ChangeState(It.Is<IUniqueChatId>(x => x.Equals(deleteCategoryCommand)),
                                                               It.IsAny<string>()), Times.Once);
            
            operationMock.Verify(x => x.DeleteCategory(It.IsAny<long>(), It.IsAny<int>(), It.IsAny<IConfiguration>()), Times.Once);
        }
    }
}