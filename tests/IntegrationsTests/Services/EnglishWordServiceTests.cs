using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using ApplicationCore.Specifications.Filter;

namespace IntegrationsTests.Services
{
    public class EnglishWordServiceTests
    {
        #region AddAsync Tests
        [Fact]
        public async Task AddAsyncSuccessfully()
        {
            // Arrange
            using var service = new TestService();
            var entity = TestService.GetEnglishWord();

            // Act
            var result = await service.EnglishWordService.AddAsync(entity);

            // Assert
            Assert.Equal(1, await service.WordRepository.CountAsync());
            Assert.Equal(entity, result);
        }

        [Fact]
        public async Task AddAsyncThrowsKeyNotFoundException()
        {
            // Arrange
            using var service = new TestService();
            var entity = TestService.GetEnglishWord();
            entity.EnglishGroupId = 1;

            // Act
            Func<Task> func = () => service.EnglishWordService.AddAsync(entity);

            // Assert
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(func);
            Assert.Equal(string.Format(service.WordRepository.GroupNotFoundMessage, entity.EnglishGroupId), exception.Message);
        }
        #endregion

        #region GetByIdAsync Tests
        [Fact]
        public async Task GetByIdAsyncSuccessfully()
        {
            // Arrange
            int id = 1;
            using var service = new TestService();
            var entity = TestService.GetEnglishWord();

            // Act
            await service.EnglishWordService.AddAsync(entity);
            var result = await service.EnglishWordService.GetByIdAsync(id);

            // Assert
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task GetByIdAsyncThrowsKeyNotFoundException()
        {
            // Arrange
            int id = 2;
            using var service = new TestService();
            var entity = TestService.GetEnglishWord();

            // Act
            await service.EnglishWordService.AddAsync(entity);
            Func<Task> func = () => service.EnglishWordService.GetByIdAsync(id);

            // Assert
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(func);
            Assert.Equal(string.Format(service.WordRepository.WordNotFoundMessage, id), exception.Message);
        }
        #endregion

        #region UdpateAsync Tests
        [Fact]
        public async Task UpdateAsyncSuccessfully()
        {
            // Arrange
            using var service = new TestService();
            var entity = TestService.GetEnglishWord();

            // Act
            await service.EnglishWordService.AddAsync(entity);

            var phrase = "Update phrase";
            entity.Phrase = phrase;
            entity.PictureUri = null;

            await service.EnglishWordService.UpdateAsync(entity);
            var updatedEntity = await service.EnglishWordService.GetByIdAsync(entity.Id);

            // Assert
            updatedEntity.Should().BeEquivalentTo(entity);
            Assert.Equal(entity, updatedEntity);
        }

        [Fact]
        public async Task UpdateAsyncThrowsKeyNotFoundException()
        {
            // Arrange
            using var service = new TestService();
            var entity = TestService.GetEnglishWord();

            // Act
            await service.EnglishWordService.AddAsync(entity);

            var phrase = "Update phrase";

            var newEntity = TestService.GetEnglishWord();
            newEntity.Phrase = phrase;
            newEntity.PictureUri = null;

            Func<Task> func = async () => await service.EnglishWordService.UpdateAsync(newEntity);

            // Assert
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(func);
            Assert.Equal(string.Format(service.WordRepository.WordNotFoundMessage, newEntity.Id), exception.Message);
        }
        #endregion

        #region ListAsync Tests
        [Theory]
        [InlineData(5)]
        [InlineData(10)]
        [InlineData(15)]
        public async Task ListAsyncReturnsFixedCountEnglishWordsOnPage(int pageSize)
        {
            // Arrange
            using var service = new TestService();
            await service.SetEnglishWordCollection();

            // Act
            var filter = new EnglishWordFilter() { PageSize = pageSize };
            var result = await service.EnglishWordService.ListAsync(filter);

            // Assert
            result.Should().AllBeOfType(typeof(EnglishWord));
            result.Should().HaveCount(pageSize);
        }

        [Fact]
        public async Task ListAsyncReturnsEnglishWordsLikePhraseInFilter()
        {
            // Arrange
            using var service = new TestService();
            await service.SetEnglishWordCollection();

            // Act
            var filter = new EnglishWordFilter() { Phrase = "Phrase 1" };
            var result = await service.EnglishWordService.ListAsync(filter); // should contain Phrase 10, 11 .. 19

            // Assert
            result.Should().AllBeOfType(typeof(EnglishWord));
            result.Should().Contain(x => x.Phrase.Contains("Phrase 1"));
            result.Should().HaveCount(10);
        }
        #endregion
    }
}
