using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationsTests.Services
{
    public class EnglishGroupServiceTests
    {
        [Fact]
        public async Task AddAsyncSuccessfully()
        {
            // Arrange
            using var service = new TestService();
            var entity = TestService.GetEnglishGroup();

            // Act
            var result = await service.EnglishGroupService.AddAsync(entity);

            // Assert
            Assert.Equal(1, await service.GroupRepository.CountAsync());
            Assert.Equal(entity, result);
        }

        [Fact]
        public async Task GetByIdAsyncSuccessfully()
        {
            // Arrange
            int id = 1;
            using var service = new TestService();
            var entity = TestService.GetEnglishGroup();

            // Act
            await service.EnglishGroupService.AddAsync(entity);
            var result = await service.EnglishGroupService.GetByIdAsync(id);

            // Assert
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task GetByIdAsyncThrowsKeyNotFoundException()
        {
            // Arrange
            int id = 2;
            using var service = new TestService();
            var entity = TestService.GetEnglishGroup();

            // Act
            await service.EnglishGroupService.AddAsync(entity);
            Func<Task> func = () => service.EnglishGroupService.GetByIdAsync(id);

            // Assert
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(func);
            Assert.Equal(string.Format(service.GroupRepository.GroupNotFoundMessage, id), exception.Message);
        }
    }
}
