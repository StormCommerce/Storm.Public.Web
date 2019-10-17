
using System.Collections.Generic;
using Enferno.Public.Web.Models;
using Enferno.StormApiClient;
using Enferno.StormApiClient.Expose;
using Enferno.StormApiClient.Products;

namespace Enferno.Public.Web.Repositories
{
    public class ProductRepository : RepositoryBase, IProductRepository
    {
        public ProductRepository() : this(null)
        { }

        public ProductRepository(IAccessClient client = null)
        {
            MyClient = client;
        }

        public Product GetProduct(int id, string statusSeed = Ignored, string divisionSeed = Ignored, string priceListSeed = Ignored, int? customerId = InvalidId, int? companyId = InvalidId, string cultureCode = Ignored, int? currencyId = InvalidId, bool useCache = true)
        {
            using (var client = GetBatch())
            {
                client.UseCache = useCache;
                return client.ProductProxy.GetProduct(id, StatusSeed(statusSeed), DivisionSeed(divisionSeed), PricelistSeed(priceListSeed), Customer(customerId), Company(companyId), CultureCode(cultureCode), Currency(currencyId));
            }
        }

        public ProductOnHand GetProductOnHand(int id, IEnumerable<Warehouse> warehouses, string statusSeed = Ignored,
            string priceListSeed = Ignored, int? customerId = InvalidId, int? companyId = InvalidId, string cultureCode = Ignored,
            int? currencyId = InvalidId, bool useCache = true)
        {
            var warehouseList = new WarehouseList();
            warehouseList.AddRange(warehouses);

            return GetProductOnHand(id, warehouseList, statusSeed, priceListSeed, customerId, companyId, cultureCode, currencyId, useCache);
        }

        public ProductOnHand GetProductOnHand(string partNo, IEnumerable<Warehouse> warehouses, string statusSeed = Ignored,
            string priceListSeed = Ignored, int? customerId = InvalidId, int? companyId = InvalidId, string cultureCode = Ignored,
            int? currencyId = InvalidId, bool useCache = true)
        {
            var warehouseList = new WarehouseList();
            warehouseList.AddRange(warehouses);

            return GetProductOnHand(partNo, warehouseList, statusSeed, priceListSeed, customerId, companyId, cultureCode, currencyId, useCache);
        }

        public ProductOnHand GetProductExternalOnHand(int id, IEnumerable<Warehouse> warehouses, string statusSeed = Ignored,
            string priceListSeed = Ignored, int? customerId = InvalidId, int? companyId = InvalidId, string cultureCode = Ignored,
            int? currencyId = InvalidId, bool useCache = true)
        {
            var warehouseList = new WarehouseList();
            warehouseList.AddRange(warehouses);

            return GetProductOnHand(id, warehouseList, statusSeed, priceListSeed, customerId, companyId, cultureCode, currencyId, true, useCache);
        }

        public ProductOnHand GetProductExternalOnHand(string partNo, IEnumerable<Warehouse> warehouses,
            string statusSeed = Ignored,
            string priceListSeed = Ignored, int? customerId = InvalidId, int? companyId = InvalidId, string cultureCode = Ignored,
            int? currencyId = InvalidId, bool useCache = true)
        {
            var warehouseList = new WarehouseList();
            warehouseList.AddRange(warehouses);

            return GetProductOnHand(partNo, warehouseList, statusSeed, priceListSeed, customerId, companyId, cultureCode, currencyId, true, useCache);
        }

        private ProductOnHand GetProductOnHand(int id, WarehouseList warehouses, string statusSeed = Ignored,
            string priceListSeed = Ignored, int? customerId = InvalidId, int? companyId = InvalidId, string cultureCode = Ignored,
            int? currencyId = InvalidId, bool external = false, bool useCache = true)
        {
            using (var client = GetBatch())
            {
                client.UseCache = useCache;
                if (external)
                {
                    return client.ProductProxy.GetExternalProductOnHandByProduct(id, warehouses, StatusSeed(statusSeed),
                        PricelistSeed(priceListSeed), Customer(customerId), Company(companyId), CultureCode(cultureCode),
                        Currency(currencyId));
                }

                return client.ProductProxy.GetProductOnHandByProduct(id, warehouses, StatusSeed(statusSeed),
                    PricelistSeed(priceListSeed), Customer(customerId), Company(companyId), CultureCode(cultureCode),
                    Currency(currencyId));
            }
        }

        private ProductOnHand GetProductOnHand(string partNo, WarehouseList warehouses, string statusSeed = Ignored,
            string priceListSeed = Ignored, int? customerId = InvalidId, int? companyId = InvalidId, string cultureCode = Ignored,
            int? currencyId = InvalidId, bool external = false, bool useCache = true)
        {
            using (var client = GetBatch())
            {
                client.UseCache = useCache;
                if (external)
                {
                    return client.ProductProxy.GetExternalProductOnHandByPartNo(partNo, warehouses,
                        StatusSeed(statusSeed),
                        PricelistSeed(priceListSeed), Customer(customerId), Company(companyId), CultureCode(cultureCode),
                        Currency(currencyId));
                }

                return client.ProductProxy.GetProductOnHandByPartNo(partNo, warehouses, StatusSeed(statusSeed),
                    PricelistSeed(priceListSeed), Customer(customerId), Company(companyId), CultureCode(cultureCode),
                    Currency(currencyId));
            }
        }

        public Request GetProductRequest(int id, string statusSeed = Ignored, string divisionSeed = Ignored, string priceListSeed = Ignored, int? customerId = InvalidId, int? companyId = InvalidId, string cultureCode = Ignored, int? currencyId = InvalidId)
        {
            return new GetProductRequest
            {
                Id = id,
                StatusSeed = StatusSeed(statusSeed),
                StoreSeed = DivisionSeed(divisionSeed),
                PricelistSeed = PricelistSeed(priceListSeed),
                CustomerId = Customer(customerId),
                CompanyId = Company(companyId),
                CultureCode = CultureCode(cultureCode),
                CurrencyId = Currency(currencyId)
            };
        }

        public Product GetProduct(string uniqueName, string statusSeed = Ignored, string divisionSeed = Ignored, string priceListSeed = Ignored, int? customerId = InvalidId, int? companyId = InvalidId, string cultureCode = Ignored, int? currencyId = InvalidId, bool useCache = true)
        {
            using (var client = GetBatch())
            {
                client.UseCache = useCache;
                return client.ProductProxy.GetProductByUniqueName(uniqueName, StatusSeed(statusSeed), DivisionSeed(divisionSeed), PricelistSeed(priceListSeed), Customer(customerId), Company(companyId), CultureCode(cultureCode), Currency(currencyId));
            }
        }

        public Request GetProductRequest(string uniqueName, string statusSeed = Ignored, string divisionSeed = Ignored, string priceListSeed = Ignored, int? customerId = InvalidId, int? companyId = InvalidId, string cultureCode = Ignored, int? currencyId = InvalidId)
        {
            return new GetProductByUniqueNameRequest
            {
                UniqueName = uniqueName,
                StatusSeed = StatusSeed(statusSeed),
                StoreSeed = DivisionSeed(divisionSeed),
                PricelistSeed = PricelistSeed(priceListSeed),
                CustomerId = Customer(customerId),
                CompanyId = Company(companyId),
                CultureCode = CultureCode(cultureCode),
                CurrencyId = Currency(currencyId)
            };
        }

        public Product GetProductByPartNo(string partNo, string statusSeed = Ignored, string divisionSeed = Ignored, string priceListSeed = Ignored, int? customerId = InvalidId, int? companyId = InvalidId, string cultureCode = Ignored, int? currencyId = InvalidId, bool useCache = true)
        {
            using (var client = GetBatch())
            {
                client.UseCache = useCache;
                return client.ProductProxy.GetProductByPartNo(partNo, StatusSeed(statusSeed), DivisionSeed(divisionSeed), PricelistSeed(priceListSeed), Customer(customerId), Company(companyId), CultureCode(cultureCode), Currency(currencyId));
            }
        }

        public Request GetProductByPartNoRequest(string partNo, string statusSeed = Ignored, string divisionSeed = Ignored, string priceListSeed = Ignored, int? customerId = InvalidId, int? companyId = InvalidId, string cultureCode = Ignored, int? currencyId = InvalidId)
        {
            return new GetProductByPartNoRequest
            {
                PartNo = partNo,
                StatusSeed = StatusSeed(statusSeed),
                StoreSeed = DivisionSeed(divisionSeed),
                PricelistSeed = PricelistSeed(priceListSeed),
                CustomerId = Customer(customerId),
                CompanyId = Company(companyId),
                CultureCode = CultureCode(cultureCode),
                CurrencyId = Currency(currencyId)
            };
        }

        public IdValues ListCategories(string cultureCode = Ignored, bool useCache = true)
        {
            using (var client = GetBatch())
            {
                client.UseCache = useCache;
                return client.ProductProxy.ListCategories(CultureCode(cultureCode));
            }
        }

        public CategoryItemList ListCategoryItems(string cultureCode = Ignored, bool useCache = true)
        {
            using (var client = GetBatch())
            {
                client.UseCache = useCache;
                return client.ProductProxy.ListCategoryItems(null, null, null, CultureCode(cultureCode));
            }
        }

        public Manufacturer GetManufacturer(int id, string cultureCode = Ignored, bool useCache = true)
        {
            using (var client = GetBatch())
            {
                client.UseCache = useCache;
                return client.ProductProxy.GetManufacturer(id, CultureCode(cultureCode));
            }
        }

        public Manufacturer GetManufacturer(string uniqueName, string cultureCode = Ignored, bool useCache = true)
        {
            using (var client = GetBatch())
            {
                client.UseCache = useCache;
                return client.ProductProxy.GetManufacturerByUniqueName(uniqueName, CultureCode(cultureCode));
            }
        }

        public ManufacturerItemPagedList ListManufacturers(string searchString = null, int? pageNo = null, int? pageSize = null, string cultureCode = Ignored, bool useCache = true)
        {
            using (var client = GetBatch())
            {
                client.UseCache = useCache;
                return client.ProductProxy.ListManufacturers(searchString, GetNullableInt(pageNo), GetNullableInt(pageSize), CultureCode(cultureCode));
            }
        }

        public ParametricInfoList ListParametricInfo(string cultureCode = Ignored, bool useCache = true)
        {
            using (var client = GetBatch())
            {
                client.UseCache = useCache;
                return client.ProductProxy.ListParametricInfo(CultureCode(cultureCode));
            }
        }

        public ParametricValueList ListParametricValues(string cultureCode = Ignored, bool useCache = true)
        {
            using (var client = GetBatch())
            {
                client.UseCache = useCache;
                return client.ProductProxy.ListParametricValues2(CultureCode(cultureCode));
            }
        }

        public PricelistList ListPricelists(string cultureCode = Ignored, bool useCache = true)
        {
            using (var client = GetBatch())
            {
                client.UseCache = useCache;
                return client.ProductProxy.ListPricelists(CultureCode(cultureCode));
            }
        }

        public FilterList ListProductFilters(bool asVariants, string searchString = null, string categorySeed = null, string manufacturerSeed = null, string flagSeed = null, string statusSeed = Ignored, string assortmentSeed = Ignored, IList<ProductListModel.ListParametric> parametrics = null, string divisionSeed = Ignored, string priceListSeed = Ignored, int? customerId = InvalidId, int? companyId = InvalidId, string sort = null, string filter = null, string cultureCode = Ignored, int? currencyId = InvalidId, bool useCache = true)
        {
            using (var client = GetBatch())
            {
                client.UseCache = useCache;
                return client.ProductProxy.ListProductFilters2(searchString, categorySeed, manufacturerSeed, flagSeed, StatusSeed(statusSeed), AssortmentSeed(assortmentSeed), Parametrics(parametrics, cultureCode), DivisionSeed(divisionSeed), PricelistSeed(priceListSeed), Customer(customerId), Company(companyId), sort, filter, CultureCode(cultureCode), Currency(currencyId), asVariants.ToString());
            }
        }

        public Request ListProductFiltersRequest(bool asVariants, string searchString = null, string categorySeed = null, string manufacturerSeed = null, string flagSeed = null, string statusSeed = Ignored, string assortmentSeed = Ignored, IList<ProductListModel.ListParametric> parametrics = null, string divisionSeed = Ignored, string priceListSeed = Ignored, int? customerId = InvalidId, int? companyId = InvalidId, string sort = null, string filter = null, string cultureCode = Ignored, int? currencyId = InvalidId)
        {
            return new ListProductFilters2Request
            {
                SearchString = searchString,
                CategorySeed = categorySeed,
                ManufacturerSeed = manufacturerSeed,
                FlagSeed = flagSeed,
                StatusSeed = StatusSeed(statusSeed),
                AssortmentSeed = AssortmentSeed(assortmentSeed),
                Parametrics = Parametrics(parametrics, cultureCode),
                StoreSeed = DivisionSeed(divisionSeed),
                PricelistSeed = PricelistSeed(priceListSeed),
                CustomerId = Customer(customerId),
                CompanyId = Company(companyId),
                Sort = sort,
                Filter = filter,
                CultureCode = CultureCode(cultureCode),
                CurrencyId = Currency(currencyId),
                AsVariants = asVariants.ToString()
            };
        }

        public ProductItemPagedList ListProducts(bool asVariants, string searchString = null, string categorySeed = null, string manufacturerSeed = null, string flagSeed = null, string statusSeed = Ignored, string assortmentSeed = Ignored, IList<ProductListModel.ListParametric> parametrics = null, string divisionSeed = Ignored, string priceListSeed = Ignored, int? customerId = InvalidId, int? companyId = InvalidId, string sort = null, int? pageNo = null, int? pageSize = null, string filter = null, string cultureCode = Ignored, int? currencyId = InvalidId, bool useCache = true)
        {
            using (var client = GetBatch())
            {
                client.UseCache = useCache;
                return client.ProductProxy.ListProducts2(searchString, categorySeed, manufacturerSeed, flagSeed, StatusSeed(statusSeed), AssortmentSeed(assortmentSeed), Parametrics(parametrics, cultureCode), DivisionSeed(divisionSeed), PricelistSeed(priceListSeed), Customer(customerId), Company(companyId), sort, GetNullableInt(pageNo), GetNullableInt(pageSize), filter, CultureCode(cultureCode), Currency(currencyId), asVariants.ToString());
            }
        }

        public ProductItemPagedList ListProducts(bool asVariants, string idSeed, string statusSeed = Ignored, string divisionSeed = Ignored, string priceListSeed = Ignored, int? customerId = InvalidId, int? companyId = InvalidId, string sort = null, int? pageNo = null, int? pageSize = null, string filter = null, string cultureCode = Ignored, int? currencyId = InvalidId, bool? returnAllVariants = null, bool useCache = true)
        {
            using (var client = GetBatch())
            {
                client.UseCache = useCache;
                return client.ProductProxy.ListProductsByIds2(idSeed, StatusSeed(statusSeed), DivisionSeed(divisionSeed), PricelistSeed(priceListSeed), Customer(customerId), Company(companyId), sort, CultureCode(cultureCode), Currency(currencyId), asVariants.ToString(), GetNullableBool(returnAllVariants));
            }
        }

        public ProductItemPagedList ListPopularProducts(bool asVariants, string searchString = null, string categorySeed = null, string manufacturerSeed = null, string flagSeed = null, string statusSeed = Ignored, string assortmentSeed = Ignored, IList<ProductListModel.ListParametric> parametrics = null, string divisionSeed = Ignored, string priceListSeed = Ignored, int? customerId = InvalidId, int? companyId = InvalidId, string sort = null, int? size = null, string filter = null, string cultureCode = Ignored, int? currencyId = InvalidId, bool useCache = true)
        {
            using (var client = GetBatch())
            {
                client.UseCache = useCache;
                return client.ProductProxy.ListPopularProducts2(searchString, categorySeed, manufacturerSeed, flagSeed, StatusSeed(statusSeed), AssortmentSeed(assortmentSeed), Parametrics(parametrics, cultureCode), DivisionSeed(divisionSeed), PricelistSeed(priceListSeed), Customer(customerId), Company(companyId), sort, GetNullableInt(size), CultureCode(cultureCode), Currency(currencyId), asVariants.ToString());
            }
        }

        public ProductIdNameList ListProductsLite(bool asVariants, string searchString = null, string categorySeed = null, string manufacturerSeed = null, string flagSeed = null, string statusSeed = Ignored, string assortmentSeed = Ignored, IList<ProductListModel.ListParametric> parametrics = null, string divisionSeed = Ignored, string priceListSeed = Ignored, int? customerId = InvalidId, int? companyId = InvalidId, string sort = null, int? size = null, string filter = null, string cultureCode = Ignored, int? currencyId = InvalidId, bool useCache = true)
        {
            using (var client = GetBatch())
            {
                client.UseCache = useCache;
                return client.ProductProxy.SearchProductsLite2(searchString, StatusSeed(statusSeed), AssortmentSeed(assortmentSeed), PricelistSeed(priceListSeed), Customer(customerId), Company(companyId), GetNullableInt(size), CultureCode(cultureCode), Currency(currencyId));
            }
        }

        public void UpdateProductViewCount(int categoryId, int productId)
        {
            using (var client = GetBatch())
            {
                client.ProductProxy.UpdateProductViewCount(categoryId, productId);
            }
        }

        public Request ListProductsRequest(bool asVariants, string searchString = null, string categorySeed = null, string manufacturerSeed = null, string flagSeed = null, string statusSeed = Ignored, string assortmentSeed = Ignored, IList<ProductListModel.ListParametric> parametrics = null, string divisionSeed = Ignored, string priceListSeed = Ignored, int? customerId = InvalidId, int? companyId = InvalidId, string sort = null, int? pageNo = null, int? pageSize = null, string filter = null, string cultureCode = Ignored, int? currencyId = InvalidId)
        {
            return new ListProducts2Request
            {
                SearchString = searchString,
                CategorySeed = categorySeed,
                ManufacturerSeed = manufacturerSeed,
                FlagSeed = flagSeed,
                StatusSeed = StatusSeed(statusSeed),
                AssortmentSeed = AssortmentSeed(assortmentSeed),
                Parametrics = Parametrics(parametrics, cultureCode),
                StoreSeed = DivisionSeed(divisionSeed),
                PricelistSeed = PricelistSeed(priceListSeed),
                CustomerId = Customer(customerId),
                CompanyId = Company(companyId),
                Sort = sort,
                PageNo = GetNullableInt(pageNo),
                PageSize = GetNullableInt(pageSize),
                Filter = filter,
                CultureCode = CultureCode(cultureCode),
                CurrencyId = Currency(currencyId),
                AsVariants = asVariants.ToString()
            };
        }

        public ProductItemPagedList ListProductAccessories(int productId, bool asVariants, AccessoryType[] accessoryTypes = null, string statusSeed = Ignored, string assortmentSeed = Ignored, string divisionSeed = Ignored, string priceListSeed = Ignored, int? customerId = InvalidId, int? companyId = InvalidId, string sort = null, int? size = null, string cultureCode = Ignored, int? currencyId = InvalidId, bool useCache = true)
        {
            var accessoryTypeSeed = GetAccessoryTypeSeed(accessoryTypes);
            using (var client = GetBatch())
            {
                client.UseCache = useCache;
                return client.ProductProxy.ListProductAccessories3(productId, accessoryTypeSeed, StatusSeed(statusSeed), AssortmentSeed(assortmentSeed), DivisionSeed(divisionSeed), PricelistSeed(priceListSeed), Customer(customerId), Company(companyId), sort, GetNullableInt(size), CultureCode(cultureCode), Currency(currencyId), asVariants.ToString());
            }
        }

        public Request ListProductAccessoriesRequest(int productId, bool asVariants, AccessoryType[] accessoryTypes = null, string statusSeed = Ignored, string assortmentSeed = Ignored, string divisionSeed = Ignored, string priceListSeed = Ignored, int? customerId = InvalidId, int? companyId = InvalidId, string sort = null, int? size = null, string cultureCode = Ignored, int? currencyId = InvalidId)
        {
            var accessoryTypeSeed = GetAccessoryTypeSeed(accessoryTypes);
            return new ListProductAccessories3Request
            {
                ProductId = productId,
                AccessoryTypeSeed = accessoryTypeSeed,
                StatusSeed = StatusSeed(statusSeed),
                AssortmentSeed = AssortmentSeed(assortmentSeed),
                StoreSeed = DivisionSeed(divisionSeed),
                PricelistSeed = PricelistSeed(priceListSeed),
                CustomerId = Customer(customerId),
                CompanyId = Company(companyId),
                Sort = sort,
                Size = GetNullableInt(size),
                CultureCode = CultureCode(cultureCode),
                CurrencyId = Currency(currencyId),
                AsVariants = asVariants.ToString()
            };
        }

        public FlagList ListProductFlags(string cultureCode = Ignored, bool useCache = true)
        {
            using (var client = GetBatch())
            {
                client.UseCache = useCache; 
                return client.ProductProxy.ListProductFlags(CultureCode(cultureCode));
            }
        }

        public Request UpdateProductViewCountRequest(int categoryId, int productId)
        {
            return new UpdateProductViewCountRequest
            {
                CategoryId = categoryId,
                ProductId = productId
            };
        }
    }
}
