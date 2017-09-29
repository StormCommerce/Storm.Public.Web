using Enferno.Public.Web.Repositories;
using Enferno.StormApiClient;
using Enferno.StormApiClient.Applications;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Enferno.Public.Web.Test.Repositories
{
    [TestClass]
    public class ApplicationRepositoriesTests : RepositoriesBaseTests
    {
        [TestMethod, TestCategory("UnitTest")]
        public void ListStoresTest()
        {
            // Arrange
            var service = new Mock<ApplicationService>();
            service.Setup(m => m.ListStores2(null)).Returns(new StoreList());

            var client = new Mock<IAccessClient>();
            client.Setup(m => m.ApplicationProxy).Returns(service.Object);

            // Act
            var repo = new ApplicationRepository(client.Object);
            var result = repo.ListStores(null);

            // Assert
            Assert.AreEqual(0, result.Count);
        }
    }
}
