using ApplicationCore.Entities;
using ApplicationCore.Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.ApplicationCore.Entitties
{
    public class EnglishWordTests
    {
        #region Tests with success results
        [Theory]
        [InlineData(null)]
        [InlineData("incorrect date")]
        public void EnglishWordCreateSuccessWithNullOrIncorrectInputCreateDate(string createDate)
        {
            var word = new EnglishWord(new EnglishWordCoreDto() { Phrase = "Word", CreateDate = createDate });
            
            Assert.NotNull(word);
            Assert.Equal(word.CreateDate, DateTime.Today);
        }

        [Fact]
        public void EnglishWordCreateSuccessWithCorrectInputCreateDate()
        {
            var createWordDto = new EnglishWordCoreDto() { Phrase = "Word", CreateDate = "11/12/2021" };
            var word = new EnglishWord(createWordDto);

            Assert.NotNull(word);
            Assert.Equal(word.CreateDate, DateTime.Parse(createWordDto.CreateDate));
        }

        [Fact]
        public void EnglishWordUpdateSuccessWithCorrectInputCreateDate()
        {
            // Arrange
            var createWordDto = new EnglishWordCoreDto() { Phrase = "Word", CreateDate = "11/12/2021" };
            var word = new EnglishWord(createWordDto);

            // Act
            var updateWordDto = new EnglishWordCoreDto() { Phrase = "Updated Word", CreateDate = "12/12/2021" };
            word.Update(updateWordDto);

            // Assert
            Assert.Equal(word.Phrase, updateWordDto.Phrase);
            Assert.Equal(word.CreateDate, DateTime.Parse(updateWordDto.CreateDate));
        }

        [Fact]
        public void EnglishWordUpdateSuccessWithNullOrIncorrectInputCreateDate()
        {
            // Arrange
            var createWordDto = new EnglishWordCoreDto() { Phrase = "Word", CreateDate = "11/12/2021" };
            var word = new EnglishWord(createWordDto);

            // Act
            var updateWordDto = new EnglishWordCoreDto() { Phrase = "Updated Word", CreateDate = "incorrect date" };
            word.Update(updateWordDto);

            // Assert
            Assert.Equal(word.Phrase, updateWordDto.Phrase);
            Assert.Equal(word.CreateDate, DateTime.Parse(createWordDto.CreateDate));
        }
        #endregion

        #region Tests with error results
        [Fact]
        public void EnglishWordCreateWithThrowsArgumentNullExeption()
        {
            Assert.Throws<ArgumentNullException>(() => new EnglishWord(new EnglishWordCoreDto() { Phrase = null }));
        }

        [Fact]
        public void EnglishWordCreateWithThrowsArgumentExeption()
        {
            Assert.Throws<ArgumentException>(() => new EnglishWord(new EnglishWordCoreDto() { Phrase = string.Empty }));
        }

        [Fact]
        public void EnlishWordUpdateThrowsArgumentNullException()
        {
            var word = new EnglishWord(new EnglishWordCoreDto(){ Phrase = "Word" });

            Assert.Throws<ArgumentNullException>(() => word.Update(new EnglishWordCoreDto() { Phrase = null }));
        }

        [Fact]
        public void EnlishWordUpdateThrowsArgumentException()
        {
            var word = new EnglishWord(new EnglishWordCoreDto() { Phrase = "Word" });

            Assert.Throws<ArgumentException>(() => word.Update(new EnglishWordCoreDto() { Phrase = string.Empty }));
        }
        #endregion
    }
}
