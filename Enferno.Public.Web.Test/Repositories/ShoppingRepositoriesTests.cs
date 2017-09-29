using Enferno.Public.Web.Repositories;
using Enferno.StormApiClient;
using Enferno.StormApiClient.Applications;
using Enferno.StormApiClient.Shopping;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Enferno.Public.Web.Test.Repositories
{
    [TestClass]
    public class ShoppingRepositoriesTests : RepositoriesBaseTests
    {
        [TestMethod, TestCategory("UnitTest")]
        public void GetBasketDefaultTest()
        {
            // Arrange
            var service = new Mock<ShoppingService>();
            service.Setup(m => m.GetBasket(1, null, "sv-SE", "2")).Returns(new Basket());

            var client = new Mock<IAccessClient>();
            client.Setup(m => m.ShoppingProxy).Returns(service.Object);

            // Act
            var repo = new ShoppingRepository(client.Object);
            var result = repo.GetBasket(1);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod, TestCategory("UnitTest")]
        public void GetBasketNullParamsTest()
        {
            // Arrange
            var service = new Mock<ShoppingService>();
            service.Setup(m => m.GetBasket(1, null, null, null)).Returns(new Basket());

            var client = new Mock<IAccessClient>();
            client.Setup(m => m.ShoppingProxy).Returns(service.Object);

            // Act
            var repo = new ShoppingRepository(client.Object);
            var result = repo.GetBasket(1, null, null, null);

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
