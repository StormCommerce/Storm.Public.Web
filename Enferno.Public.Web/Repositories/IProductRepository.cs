
using System.Collections.Generic;
using Enferno.Public.Web.Models;
using Enferno.StormApiClient.Expose;
using Enferno.StormApiClient.Products;

namespace Enferno.Public.Web.Repositories
{
    public interface IProductRepository
    {
        #region Methods

        Product GetProduct(int id, string statusSeed = RepositoryBase.Ignored, string divisionSeed = RepositoryBase.Ignored, string pricelistSeed = RepositoryBase.Ignored, int? customerId = RepositoryBase.InvalidId, int? companyId = RepositoryBase.InvalidId, string cultureCode = RepositoryBase.Ignored, int? currencyId = RepositoryBase.InvalidId, bool useCache = true);
        Product GetProduct(string uniqueName, string statusSeed = RepositoryBase.Ignored, string divisionSeed = RepositoryBase.Ignored, string pricelistSeed = RepositoryBase.Ignored, int? customerId = RepositoryBase.InvalidId, int? companyId = RepositoryBase.InvalidId, string cultureCode = RepositoryBase.Ignored, int? currencyId = RepositoryBase.InvalidId, bool useCache = true);
        Product GetProductByPartNo(string partNo, string statusSeed = RepositoryBase.Ignored, string divisionSeed = RepositoryBase.Ignored, string pricelistSeed = RepositoryBase.Ignored, int? customerId = RepositoryBase.InvalidId, int? companyId = RepositoryBase.InvalidId, string cultureCode = RepositoryBase.Ignored, int? currencyId = RepositoryBase.InvalidId, bool useCache = true);

        ProductOnHand GetProductOnHand(int id, IEnumerable<Warehouse> warehouses, string statusSeed = RepositoryBase.Ignored, string pricelistSeed = RepositoryBase.Ignored, int? customerId = RepositoryBase.InvalidId, int? companyId = RepositoryBase.InvalidId, string cultureCode = RepositoryBase.Ignored, int? currencyId = RepositoryBase.InvalidId, bool useCache = true);
        ProductOnHand GetProductOnHand(string partNo, IEnumerable<Warehouse> warehouses, string statusSeed = RepositoryBase.Ignored, string pricelistSeed = RepositoryBase.Ignored, int? customerId = RepositoryBase.InvalidId, int? companyId = RepositoryBase.InvalidId, string cultureCode = RepositoryBase.Ignored, int? currencyId = RepositoryBase.InvalidId, bool useCache = true);
        ProductOnHand GetProductExternalOnHand(int id, IEnumerable<Warehouse> warehouses, string statusSeed = RepositoryBase.Ignored, string pricelistSeed = RepositoryBase.Ignored, int? customerId = RepositoryBase.InvalidId, int? companyId = RepositoryBase.InvalidId, string cultureCode = RepositoryBase.Ignored, int? currencyId = RepositoryBase.InvalidId, bool useCache = true);
        ProductOnHand GetProductExternalOnHand(string partNo, IEnumerable<Warehouse> warehouses, string statusSeed = RepositoryBase.Ignored, string pricelistSeed = RepositoryBase.Ignored, int? customerId = RepositoryBase.InvalidId, int? companyId = RepositoryBase.InvalidId, string cultureCode = RepositoryBase.Ignored, int? currencyId = RepositoryBase.InvalidId, bool useCache = true);

        IdValues ListCategories(string cultureCode = RepositoryBase.Ignored, bool useCache = true);
        CategoryItemList ListCategoryItems(string cultureCode = RepositoryBase.Ignored, bool useCache = true);

        Manufacturer GetManufacturer(int id, string cultureCode = RepositoryBase.Ignored, bool useCache = true);
        Manufacturer GetManufacturer(string uniqueName, string cultureCode = RepositoryBase.Ignored, bool useCache = true);
        ManufacturerItemPagedList ListManufacturers(string searchString = null, int? pageNo = null, int? pageSize = null, string cultureCode = RepositoryBase.Ignored, bool useCache = true);

        ParametricInfoList ListParametricInfo(string cultureCode = RepositoryBase.Ignored, bool useCache = true);
        ParametricValueList ListParametricValues(string cultureCode = RepositoryBase.Ignored, bool useCache = true);
        PricelistList ListPricelists(string cultureCode = RepositoryBase.Ignored, bool useCache = true);

        FilterList ListProductFilters(bool asVariants, string searchString = null, string categorySeed = null, string manufacturerSeed = null, string flagSeed = null, string statusSeed = RepositoryBase.Ignored, string assortmentSeed = RepositoryBase.Ignored, IList<ProductListModel.ListParametric> parametrics = null, string divisionSeed = RepositoryBase.Ignored, string pricelistSeed = RepositoryBase.Ignored, int? customerId = RepositoryBase.InvalidId, int? companyId = RepositoryBase.InvalidId, string sort = null, string filter = null, string cultureCode = RepositoryBase.Ignored, int? currencyId = RepositoryBase.InvalidId, bool useCache = true);
        FlagList ListProductFlags(string cultureCode = RepositoryBase.Ignored, bool useCache = true);
        ProductItemPagedList ListProducts(bool asVariants, string searchString = null, string categorySeed = null, string manufacturerSeed = null, string flagSeed = null, string statusSeed = RepositoryBase.Ignored, string assortmentSeed = RepositoryBase.Ignored, IList<ProductListModel.ListParametric> parametrics = null, string divisionSeed = RepositoryBase.Ignored, string pricelistSeed = RepositoryBase.Ignored, int? customerId = RepositoryBase.InvalidId, int? companyId = RepositoryBase.InvalidId, string sort = null, int? pageNo = null, int? pageSize = null, string filter = null, string cultureCode = RepositoryBase.Ignored, int? currencyId = RepositoryBase.InvalidId, bool useCache = true);
        ProductItemPagedList ListProducts(bool asVariants, string idSeed, string statusSeed = RepositoryBase.Ignored, string divisionSeed = RepositoryBase.Ignored, string pricelistSeed = RepositoryBase.Ignored, int? customerId = RepositoryBase.InvalidId, int? companyId = RepositoryBase.InvalidId, string sort = null, int? pageNo = null, int? pageSize = null, string filter = null, string cultureCode = RepositoryBase.Ignored, int? currencyId = RepositoryBase.InvalidId, bool? returnAllVariants = null, bool useCache = true);
        ProductItemPagedList ListProductAccessories(int productId, bool asVariants, AccessoryType[] accessoryTypes = null, string statusSeed = RepositoryBase.Ignored, string assortmentSeed = RepositoryBase.Ignored, string divisionSeed = RepositoryBase.Ignored, string pricelistSeed = RepositoryBase.Ignored, int? customerId = RepositoryBase.InvalidId, int? companyId = RepositoryBase.InvalidId, string sort = null, int? size = null, string cultureCode = RepositoryBase.Ignored, int? currencyId = RepositoryBase.InvalidId, bool useCache = true);
        ProductItemPagedList ListPopularProducts(bool asVariants, string searchString = null, string categorySeed = null, string manufacturerSeed = null, string flagSeed = null, string statusSeed = RepositoryBase.Ignored, string assortmentSeed = RepositoryBase.Ignored, IList<ProductListModel.ListParametric> parametrics = null, string divisionSeed = RepositoryBase.Ignored, string pricelistSeed = RepositoryBase.Ignored, int? customerId = RepositoryBase.InvalidId, int? companyId = RepositoryBase.InvalidId, string sort = null, int? size = null, string filter = null, string cultureCode = RepositoryBase.Ignored, int? currencyId = RepositoryBase.InvalidId, bool useCache = true);
        ProductIdNameList ListProductsLite(bool asVariants, string searchString = null, string categorySeed = null, string manufacturerSeed = null, string flagSeed = null, string statusSeed = RepositoryBase.Ignored, string assortmentSeed = RepositoryBase.Ignored, IList<ProductListModel.ListParametric> parametrics = null, string divisionSeed = RepositoryBase.Ignored, string pricelistSeed = RepositoryBase.Ignored, int? customerId = RepositoryBase.InvalidId, int? companyId = RepositoryBase.InvalidId, string sort = null, int? size = null, string filter = null, string cultureCode = RepositoryBase.Ignored, int? currencyId = RepositoryBase.InvalidId, bool useCache = true);

        void UpdateProductViewCount(int categoryId, int productId);

        #endregion

        #region Request objects

        Request GetProductRequest(int id, string statusSeed = RepositoryBase.Ignored, string divisionSeed = RepositoryBase.Ignored, string pricelistSeed = RepositoryBase.Ignored, int? customerId = RepositoryBase.InvalidId, int? companyId = RepositoryBase.InvalidId, string cultureCode = RepositoryBase.Ignored, int? currencyId = RepositoryBase.InvalidId);
        Request GetProductRequest(string uniqueName, string statusSeed = RepositoryBase.Ignored, string divisionSeed = RepositoryBase.Ignored, string pricelistSeed = RepositoryBase.Ignored, int? customerId = RepositoryBase.InvalidId, int? companyId = RepositoryBase.InvalidId, string cultureCode = RepositoryBase.Ignored, int? currencyId = RepositoryBase.InvalidId);
        Request GetProductByPartNoRequest(string partNo, string statusSeed = RepositoryBase.Ignored, string divisionSeed = RepositoryBase.Ignored, string pricelistSeed = RepositoryBase.Ignored, int? customerId = RepositoryBase.InvalidId, int? companyId = RepositoryBase.InvalidId, string cultureCode = RepositoryBase.Ignored, int? currencyId = RepositoryBase.InvalidId);

        Request ListProductFiltersRequest(bool asVariants, string searchString = null, string categorySeed = null, string manufacturerSeed = null, string flagSeed = null, string statusSeed = RepositoryBase.Ignored, string assortmentSeed = RepositoryBase.Ignored, IList<ProductListModel.ListParametric> parametrics = null, string divisionSeed = RepositoryBase.Ignored, string pricelistSeed = RepositoryBase.Ignored, int? customerId = RepositoryBase.InvalidId, int? companyId = RepositoryBase.InvalidId, string sort = null, string filter = null, string cultureCode = RepositoryBase.Ignored, int? currencyId = RepositoryBase.InvalidId);
        Request ListProductsRequest(bool asVariants, string searchString = null, string categorySeed = null, string manufacturerSeed = null, string flagSeed = null, string statusSeed = RepositoryBase.Ignored, string assortmentSeed = RepositoryBase.Ignored, IList<ProductListModel.ListParametric> parametrics = null, string divisionSeed = RepositoryBase.Ignored, string pricelistSeed = RepositoryBase.Ignored, int? customerId = RepositoryBase.InvalidId, int? companyId = RepositoryBase.InvalidId, string sort = null, int? pageNo = null, int? pageSize = null, string filter = null, string cultureCode = RepositoryBase.Ignored, int? currencyId = RepositoryBase.InvalidId);
        Request ListProductAccessoriesRequest(int productId, bool asVariants, AccessoryType[] accessoryTypes = null, string statusSeed = RepositoryBase.Ignored, string assortmentSeed = RepositoryBase.Ignored, string divisionSeed = RepositoryBase.Ignored, string pricelistSeed = RepositoryBase.Ignored, int? customerId = RepositoryBase.InvalidId, int? companyId = RepositoryBase.InvalidId, string sort = null, int? size = null, string cultureCode = RepositoryBase.Ignored, int? currencyId = RepositoryBase.InvalidId);

        Request UpdateProductViewCountRequest(int categoryId, int productId);
        #endregion
    }
}
