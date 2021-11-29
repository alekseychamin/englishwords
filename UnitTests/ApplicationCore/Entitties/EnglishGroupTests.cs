using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.ApplicationCore.Entitties
{
    public class EnglishGroupTests
    {
        [Fact]
        public void EnglishGroupCreateThrowsArgumentNullExeption()
        {
            Assert.Throws<ArgumentNullException>(() => new EnglishGroup(null));
        }

        [Fact]
        public void EnglishGroupCreateThrowsArgumentExeption()
        {
            Assert.Throws<ArgumentException>(() => new EnglishGroup(string.Empty));
        }

        [Fact]
        public void EnglishGroupUpdateThrowsArgumentNullException()
        {
            var englishGroup = new EnglishGroup("Group1");

            Assert.Throws<ArgumentNullException>(() => englishGroup.Update(null));
        }

        [Fact]
        public void EnglishGroupUpdateThrowsArgumentException()
        {
            var englishGroup = new EnglishGroup("Group1");

            Assert.Throws<ArgumentException>(() => englishGroup.Update(string.Empty));
        }
    }
}
