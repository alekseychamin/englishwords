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
using System.Threading.Tasks;

namespace Tests.Command
{
    public class NewWordCommandTests
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
            var inputState = new Mock<IState>();
            
            var command = new NewWordCommand("/test", "test", configuration,
                                             startState: inputState.Object);            

            // Act
            await command.Execute(chatId);

            // Assert
            Assert.AreEqual(1, command.ListChatId.Count);
            Assert.That(command.ListChatId.Contains(chatId));

            Assert.AreEqual(1, command.State.Count);
            Assert.That(command.State.ContainsKey(chatId));

            inputState.Verify(x => x.Initialize(), Times.Once);

            operationMock.Verify(x =>
                x.SetStateChatIdConfig(It.IsAny<IState>(), It.IsAny<IState>(),
                    It.IsAny<long>(), It.IsAny<IConfiguration>()), Times.Once);
        }        

        [TestCase(1)]
        public async Task RemoveChatIdTest(long chatId)
        {
            // Assign
            var inputState = new Mock<IState>();
            var command = new NewWordCommand("/test", "test", configuration,
                                             startState: inputState.Object);

            // Act
            await command.Execute(chatId);
            command.RemoveChatId(chatId);

            // Assert
            Assert.AreEqual(0, command.ListChatId.Count);
            Assert.That(command.ListChatId.Contains(chatId) == false);

            Assert.AreEqual(0, command.State.Count);
            Assert.That(command.State.ContainsKey(chatId) == false);

            Assert.AreEqual(0, command.EnglishWordFromUser.Count);
            Assert.That(command.EnglishWordFromUser.ContainsKey(chatId) == false);            
        }

        [TestCase(1, "test")]
        public async Task SaveInfoFromUser_StateNotNull_Test(long chatId, string message)
        {
            // Assign
            var inputState = new Mock<IState>();

            var command = new NewWordCommand("/test", "test", configuration,
                                             startState: inputState.Object);
            
            // Act
            await command.Execute(chatId);
            await command.SaveInfoFromUser(chatId, message);

            // Assert
            inputState.Verify(x => x.Initialize(), Times.Once);
            inputState.Verify(x => x.ChangeState(It.Is<IUniqueChatId>(x => x.Equals(command)),
                                                           It.IsAny<string>()), Times.Once);
        }

        [TestCase(1, "test")]
        public async Task SaveInfoFromUser_StateNull_Test(long chatId, string message)
        {
            // Assign
            var inputState = new Mock<IState>();

            var command = new NewWordCommand("/test", "test", configuration,
                                             startState: inputState.Object);

            inputState.Setup(x => x.ChangeState(It.IsAny<IUniqueChatId>(), It.IsAny<string>()))
                                  .Callback(() => 
                                  {                                      
                                      command.State[chatId] = null; 
                                  });

            // Act
            await command.Execute(chatId);
            await command.SaveInfoFromUser(chatId, message);

            // Assert
            inputState.Verify(x => x.Initialize(), Times.Once);
            inputState.Verify(x => x.ChangeState(It.Is<IUniqueChatId>(x => x.Equals(command)),
                                                             It.IsAny<string>()), Times.Once);

            operationMock.Verify(x => x.CreateNewWord(It.IsAny<long>(), It.IsAny<NewEnglishWord>(), It.IsAny<IConfiguration>()), Times.Once);
        }
    }
}