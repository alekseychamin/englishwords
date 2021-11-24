using ApplicationCore.Entities;
using ApplicationCore.Specifications;
using ApplicationCore.Specifications.Filter;
using Ardalis.Specification.EntityFrameworkCore;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace UnitTests.ApplicationCore.Specifications
{
    public class EnglishGroupWithFilterTests
    {
        [Fact]
        public void ReturnsAllEnglishGroups()
        {
            // Arrange
            var spec = new EnglishGroupWithFilter(new EnglishGroupFilter());

            // Act
            var criterias = spec.SearchCriterias.ToList();

            // Assert
            criterias.Should().BeEmpty();
        }

        [Fact]
        public void Returns2EnglishGroups()
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
