using ApplicationCore.Specifications;
using ApplicationCore.Specifications.Filter;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.ApplicationCore.Specifications
{
    public class EnglishWordWithFilterTests
    {
        [Fact]
        public void ReturnsEmptyCriteriaEnglishWordWithFilter()
        {
            // Arrange
            var spec = new EnglishWordWithFilter(new EnglishWordFilter());

            // Act
            var criterias = spec.SearchCriterias.ToList();

            // Assert
            criterias.Should().BeEmpty();
        }

        [Fact]
        public void Returns4CriteriasEnglishWordWithFilter()
        {
            // Arrange
            var spec = new EnglishWordWithFilter(new EnglishWordFilter() 
            { 
                Phrase = "Phrase",
                Transcription = "Transcription",
                Translation = "Translation",
                Example = "Example"
            });

            // Act
            var criterias = spec.SearchCriterias.ToList();

            // Arrange
            criterias.Should().HaveCount(4);
            criterias.Should().Contain(x => x.SearchTerm.Equals("%Phrase%"));
            criterias.Should().Contain(x => x.SearchTerm.Equals("%Transcription%"));
            criterias.Should().Contain(x => x.SearchTerm.Equals("%Translation%"));
            criterias.Should().Contain(x => x.SearchTerm.Equals("%Example%"));
        }
    }
}
