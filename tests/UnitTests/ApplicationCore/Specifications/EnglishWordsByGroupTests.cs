using ApplicationCore.Entities;
using ApplicationCore.Specifications;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.ApplicationCore.Specifications
{
    public class EnglishWordsByGroupTests
    {
        private readonly int _testEnglishGroupId = 12;

        [Fact]
        public void FindEnglishWordsByGivenEnglishGroupId()
        {
            // Arrange
            var spec = new EnglishWordsByGroup(_testEnglishGroupId);

            // Act            
            var result = GetTestEnglishWordCollection()
                .AsQueryable()
                .Where(spec.WhereExpressions.FirstOrDefault().Filter)
                .ToList();

            // Assert
            result.Should().HaveCount(2);
            result.Should().OnlyContain(x => x.EnglishGroupId == _testEnglishGroupId);
        }

        private List<EnglishWord> GetTestEnglishWordCollection()
        {
            var englishGroup1 = new EnglishGroup() { Id = 1, Name = "Group1" };

            var englishWord1 = new EnglishWord()
            {
                Id = 1,
                Phrase = "phrase1",
                Transcription = "transcription1",
                Translation = "translation1",
                Example = "example1",
                PictureUri = "pictureUri1",
                EnglishGroupId = englishGroup1.Id,
                EnglishGroup = englishGroup1
            };

            var englishGroup2 = new EnglishGroup() { Id = _testEnglishGroupId, Name = "Group2" };

            var englishWord2 = new EnglishWord()
            {
                Id = 2,
                Phrase = "phrase2",
                Transcription = "transcription2",
                Translation = "translation2",
                Example = "example2",
                PictureUri = "pictureUri2",
                EnglishGroupId = englishGroup2.Id,
                EnglishGroup = englishGroup2
            };

            var englishWord3 = new EnglishWord()
            {
                Id = 3,
                Phrase = "phrase3",
                Transcription = "transcription3",
                Translation = "translation3",
                Example = "example3",
                PictureUri = "pictureUri3",
                EnglishGroupId = englishGroup2.Id,
                EnglishGroup = englishGroup2
            };

            return new List<EnglishWord>()
            {
                englishWord1,
                englishWord2,
                englishWord3
            };
        }
    }
}
