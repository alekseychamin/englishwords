using ApplicationCore.Specifications;
using ApplicationCore.Specifications.Filter;
using FluentAssertions;
using System.Linq;
using Xunit;

namespace UnitTests.ApplicationCore.Specifications
{
    public class EnglishGroupWithFilterTests
    {
        [Fact]
        public void ReturnsEmptyCriteriaEnglishGroupWithFilter()
        {
            // Arrange
            var spec = new EnglishGroupWithFilter(new EnglishGroupFilter());

            // Act
            var criterias = spec.SearchCriterias.ToList();

            // Assert
            criterias.Should().BeEmpty();
        }

        [Fact]
        public void Returns1CriteriaEnglishGroupWithFilter()
        {
            // Arrange
            var spec = new EnglishGroupWithFilter(new EnglishGroupFilter() { Name = "Group1" });

            // Act
            var criterias = spec.SearchCriterias.ToList();

            // Assert
            criterias.Should().ContainSingle();
            criterias.Single().SearchTerm.Should().Be("%Group1%");
        }
    }
}
