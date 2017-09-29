
using Enferno.Public.Web.Models;
using Enferno.StormApiClient.Products;
using Enferno.StormApiClient.Shopping;

namespace Enferno.Public.Web
{
    /// <summary>
    /// ISiteRules defines functions needed by the framework but that can only by implemented by the appliaction.
    /// Derive from DefaultSiteRules and override all functions that are applicable in the appliaction.
    /// </summary>
    public interface ISiteRules 
    {
        OnHandStatusModel GetOnHandStatus(Product product);
        OnHandStatusModel GetOnHandStatus(ProductItem productItem);
        OnHandStatusModel GetOnHandStatus(VariantItem variantItem);       
        OnHandStatusModel GetOnHandStatus(Basket basket);
        OnHandStatusModel GetOnHandStatus(Warehouse warehouse);

        string GetProductPageUrl(ProductItem product);
        string GetProductPageUrl(BasketItem basketItem);
        string GetPaymentSuccessUrl();
        string FormatProductPageUrl(string baseUrl, ProductItem product);
        string FormatProductPageUrl(string baseUrl, BasketItem basketItem);

        string GetManufacurerPageUrl(ManufacturerItem manufacturer);
        string FormatManufacturerPageUrl(string baseUrl, ManufacturerItem manufacturer);
    }

    /// <summary>
    /// DefaultSiteRules just implements a sample of what should be implemented at the application level.
    /// Derive and override.
    /// </summary>
    public class DefaultSiteRules : ISiteRules
    {
        public virtual OnHandStatusModel GetOnHandStatus(Product product)
        {
            return CreateDefaultOnHandStatus(product.OnHand.Value);
        }

        public virtual OnHandStatusModel GetOnHandStatus(ProductItem productItem)
        {
            return CreateDefaultOnHandStatus(productItem.OnHand.Value);
        }

        public virtual OnHandStatusModel GetOnHandStatus(VariantItem variantItem)
        {
            return CreateDefaultOnHandStatus(variantItem.OnHand?.Value ?? 0M);
        }

        public virtual OnHandStatusModel GetOnHandStatus(Basket basket)
        {
            return CreateDefaultOnHandStatus();
        }

        public virtual OnHandStatusModel GetOnHandStatus(Warehouse warehouse)
        {
            return CreateDefaultOnHandStatus();
        }

        public virtual string GetProductPageUrl(ProductItem product)
        {
            return FormatProductPageUrl("http://mysite/products/", product);
        }

        public string GetProductPageUrl(BasketItem basketItem)
        {
            return FormatProductPageUrl("http://mysite/products/", basketItem);
        }

        public string GetPaymentSuccessUrl()
        {
            return string.Empty;
        }

        public virtual string FormatProductPageUrl(string baseUrl, ProductItem product)
        {
            return $"{baseUrl}/{product.UniqueName}";
        }

        public virtual string FormatProductPageUrl(string baseUrl, BasketItem basketItem)
        {
            return $"{baseUrl}/{basketItem.UniqueName}";
        }

        public virtual string GetManufacurerPageUrl(ManufacturerItem manufacturer)
        {
            return FormatManufacturerPageUrl("http://mysite/manufacturers/", manufacturer);
        }

        public virtual string FormatManufacturerPageUrl(string baseUrl, ManufacturerItem manufacturer)
        {
            return $"{baseUrl}/{manufacturer.UniqueName}";
        }

        protected static OnHandStatusModel CreateDefaultOnHandStatus(decimal count = 0)
        {
            return new OnHandStatusModel
            {
                Count = count,
                Status = OnHandStatus.Ok,
                Text1 = "",
                Text2 = ""
            };
        }
    }
}
