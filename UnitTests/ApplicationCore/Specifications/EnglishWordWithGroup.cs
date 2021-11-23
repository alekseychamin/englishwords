﻿using Ardalis.Specification.EntityFrameworkCore;
using ApplicationCore.Entities;
using ApplicationCore.Specifications;
using Moq;
using System.Collections.Generic;
using Xunit;
using System.Linq;

namespace UnitTests.ApplicationCore.Specifications
{
    public class EnglishWordWithGroup
    {
        private readonly int _testEnglishWordId = 123;
        private readonly int _testEnglishGroupId = 231;

        [Fact]
        public void MatchesEnglishWordWithGivenEnglishWordId()
        {
            var spec = new EnglishWordWithGroupSpecification(_testEnglishWordId);

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
            return new List<EnglishWord>()
            {
                new EnglishWord(){ Id = 1, EnglishGroup = new EnglishGroup() { Id = 1 }},
                new EnglishWord(){ Id = 2, EnglishGroup = new EnglishGroup() { Id = 2 }},
                new EnglishWord(){ Id = _testEnglishWordId, EnglishGroup = new EnglishGroup() { Id = _testEnglishGroupId }}
            };
        }
    }
}