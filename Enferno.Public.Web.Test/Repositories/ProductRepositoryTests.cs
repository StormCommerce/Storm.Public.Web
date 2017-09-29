using Enferno.Public.Web.Repositories;
using Enferno.StormApiClient.Expose;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Enferno.Public.Web.Test.Repositories
{
    [TestClass]
    public class ProductRepositoryTests : RepositoriesBaseTests
    {
        [TestMethod, TestCategory("UnitTest")]
        public void GetProductRequestWithOptionalCultureCodeTest()
        {
            // Act
            var repo = new ProductRepository();
            var request = repo.GetProductRequest("unique-name", cultureCode: "nb-NO") as GetProductByUniqueNameRequest;

            // Assert
            Assert.IsNotNull(request);
            Assert.AreEqual("nb-NO", request.CultureCode);
        }

        [TestMethod, TestCategory("UnitTest")]
        public void GetProductRequestWithDefaultCultureCodeFromStormContextTest()
        {
            // Act
            var repo = new ProductRepository();
            var request = repo.GetProductRequest("unique-name") as GetProductByUniqueNameRequest;

            // Assert
            Assert.IsNotNull(request);
            Assert.AreEqual("sv-SE", request.CultureCode);
        }

        [TestMethod, TestCategory("UnitTest")]
        public void GetProductRequestWithNullCultureCodeTest()
        {
            // Act
            var repo = new ProductRepository();
            var request = repo.GetProductRequest("unique-name", cultureCode: null) as GetProductByUniqueNameRequest;

            // Assert
            Assert.IsNotNull(request);
            Assert.IsNull(request.CultureCode);
        }
    }
}
