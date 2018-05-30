
using AutoMapper;
using Enferno.Public.Web.Models;
using Enferno.StormApiClient.Shopping;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Enferno.Public.Web.Test.Mappers.BasketProfiles
{
    [TestClass]
    public class BasketItemModelToBasketItemProfileTests
    {
        [TestMethod, TestCategory("UnitTest")]
        public void MapTest()
        {
            // Arrange
            var source = new BasketItemModel
            {
                Id = 1
            };

            // Act
            var destination = Mapper.Map<BasketItem>(source);

            // Assert
            Assert.AreEqual(1, destination.Id);
        }
    }
}
