using ApplicationCore.Entities;
using ApplicationCore.Specifications;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace UnitTests.ApplicationCore.Specifications
{
    public class EnglishWordWithGroupTests
    {
        private readonly int _testEnglishWordId = 123;
        private readonly int _testEnglishGroupId = 231;

        [Fact]
        public void MatchesEnglishWordWithGivenEnglishWordId()
        {
            var spec = new EnglishWordWithGroup(_testEnglishWordId);

            var result = GetTestEnglishWordCollection()
                .AsQueryable()
                .Where(spec.WhereExpressions.FirstOrDefault())
                .FirstOrDefault();

            Assert.NotNull(result);
            Assert.Equal(_testEnglishWordId, result.Id);
            Assert.Equal(_testEnglishGroupId, result.EnglishGroup.Id);
        }

        public List<EnglishWord> GetTestEnglishWordCollection()
        {
            var mockEnglishGroup1 = new Mock<EnglishGroup>("Group1");
            mockEnglishGroup1.SetupGet(x => x.Id).Returns(1);

            var mockEnglishWord1 = new Mock<EnglishWord>("phrase1", "transcription1", "translation1",
                                                         "example1", "pictureUri1", mockEnglishGroup1.Object);
            mockEnglishWord1.SetupGet(x => x.Id).Returns(1);

            var mockEnglishGroup2 = new Mock<EnglishGroup>("Group2");
            mockEnglishGroup2.SetupGet(x => x.Id).Returns(_testEnglishGroupId);

            var mockEnglishWord2 = new Mock<EnglishWord>("phrase2", "transcription2", "translation2",
                                                         "example2", "pictureUri2", mockEnglishGroup2.Object);
            mockEnglishWord2.SetupGet(x => x.Id).Returns(_testEnglishWordId);

            return new List<EnglishWord>()
            {
                mockEnglishWord1.Object,
                mockEnglishWord2.Object
            };
        }
    }
}
