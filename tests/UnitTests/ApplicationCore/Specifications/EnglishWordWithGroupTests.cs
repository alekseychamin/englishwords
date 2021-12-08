using ApplicationCore.Entities;
using ApplicationCore.Entities.Dto;
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
        public void FindEnglishWorWithGroupdWithGivenEnglishWordId()
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

        private List<EnglishWord> GetTestEnglishWordCollection()
        {
            var englishGroup1 = new EnglishGroup(new EnglishGroupCoreDto() { Id = 1, Name = "Group1" });

            var englishWord1 = new EnglishWord(
                new EnglishWordCoreDto()
                {
                    Id = 1,
                    Phrase = "phrase1",
                    Transcription = "transcription1",
                    Translation = "translation1",
                    Example = "example1",
                    PictureUri = "pictureUri1",
                    EnglishGroup = englishGroup1
                });

            var englishGroup2 = new EnglishGroup(new EnglishGroupCoreDto() { Id = _testEnglishGroupId, Name = "Group2" });

            var englishWord2 = new EnglishWord(
                new EnglishWordCoreDto()
                {
                    Id = _testEnglishWordId,
                    Phrase = "phrase2",
                    Transcription = "transcription2",
                    Translation = "translation2",
                    Example = "example2",
                    PictureUri = "pictureUri2",
                    EnglishGroup = englishGroup2
                });

            return new List<EnglishWord>()
            {
                englishWord1,
                englishWord2
            };
        }
    }
}
