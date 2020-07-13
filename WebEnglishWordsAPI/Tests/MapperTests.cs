using AutoMapper;
using BusinessLogic.Model;
using BusinessLogic.Profiles;
using DataAccess.Model;
using NUnit.Framework;
using WebAPI.Profiles;

namespace Tests
{
    public class MapperTests
    {
        private IMapper mapper;
        
        [Test]
        public void Map_EnglishWordBLNotNullCategory_To_EnglishWord()
        {

            // Assign
            mapper = Helper.GetMockMapper(new BLProfile(), new WebProfile());

            var itemBL = new EnglishWordBL 
            { 
                Category = new CategoryBL { EnglishWords = null, Id = 0, Name = "Test" },
                CategoryId = 0
            };

            // Act
            var itemDAL = mapper.Map<EnglishWord>(itemBL);

            // Assert
            Assert.AreNotEqual(null, itemDAL.Category);
        }

        [Test]
        public void Map_EnglishWordBLNullCategory_To_EnglishWord()
        {

            // Assign
            mapper = Helper.GetMockMapper(new BLProfile(), new WebProfile());

            var itemBL = new EnglishWordBL
            {
                Category = null
            };

            // Act            
            var itemDAL = new EnglishWord
            {
                Category = new Category { EnglishWords = null, Id = 0, Name = "Test" }
            };

            mapper.Map(itemBL, itemDAL);

            // Assert
            Assert.AreNotEqual(null, itemDAL.Category);
        }

        [Test]
        public void Map_EnglishWordBLCategoryIdNotEqual_To_EnglishWord()
        {

            // Assign
            mapper = Helper.GetMockMapper(new BLProfile(), new WebProfile());

            var itemBL = new EnglishWordBL
            {
                Category = new CategoryBL { EnglishWords = null, Id = 1, Name = "Test" },
                CategoryId = 2
            };

            // Act            
            var itemDAL = new EnglishWord
            {
                Category = new Category { EnglishWords = null, Id = 1, Name = "Test" },
                CategoryId = 1
            };

            mapper.Map(itemBL, itemDAL);

            // Assert
            Assert.AreEqual(null, itemDAL.Category);
            Assert.AreEqual(2, itemDAL.CategoryId);
        }
    }
}