using ApplicationCore.Entities;
using ApplicationCore.Entities.Dto;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.ApplicationCore.Entitties
{
    public class EnglishGroupTests
    {
        #region Tests with success results
        [Fact]
        public void EnglishGroupCreateSucccess()
        {
            Assert.NotNull(new EnglishGroup(new EnglishGroupCoreDto() { Name = "Group1" }));
        }

        [Fact]
        public void EnglishGroupUpdateSuccess()
        {
            var group = new EnglishGroup(new EnglishGroupCoreDto() { Name = "Group1" });
            
            var exception = Record.Exception(() => group.Update(new EnglishGroupCoreDto() { Name = "Group2" }));

            Assert.Null(exception);
        }

        [Fact]
        public void EnglishGroupAddItemSuccess()
        {
            // Arrange
            var group = new EnglishGroup(new EnglishGroupCoreDto() { Name = "Group" });
            var word = new EnglishWord(new EnglishWordCoreDto() { Phrase = "Word" });

            // Act
            group.AddItem(word);

            // Assert
            group.EnglishWords.Should().ContainSingle(x => x.Phrase == "Word");
            group.EnglishWords.Should().Equal(word);
        }
        #endregion

        #region Tests with error results
        [Fact]
        public void EnglishGroupCreateThrowsArgumentNullExeption()
        {
            Assert.Throws<ArgumentNullException>(() => new EnglishGroup(null));
        }

        [Fact]
        public void EnglishGroupCreateThrowsArgumentExeption()
        {
            Assert.Throws<ArgumentException>(() => new EnglishGroup(new EnglishGroupCoreDto() { Name = string.Empty }));
        }

        [Fact]
        public void EnglishGroupUpdateThrowsArgumentNullException()
        {
            var englishGroup = new EnglishGroup(new EnglishGroupCoreDto() { Name = "Group1" });

            Assert.Throws<ArgumentNullException>(() => englishGroup.Update(null));
        }

        [Fact]
        public void EnglishGroupUpdateThrowsArgumentException()
        {
            var englishGroup = new EnglishGroup(new EnglishGroupCoreDto() { Name = "Group1" });

            Assert.Throws<ArgumentException>(() => englishGroup.Update(new EnglishGroupCoreDto() { Name = string.Empty }));
        } 
        #endregion
    }
}
