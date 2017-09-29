using Enferno.Web.StormUtils;

namespace Enferno.Public.Web
{
    public class PriceCalulator
    {
        private static bool ShowIncVat
        {
            get
            {
                return StormContext.ShowPricesIncVat.GetValueOrDefault(true);
            }
        }

        public static decimal Price(decimal? price, decimal vatRate)
        {
            if (!price.HasValue) return 0;
            if (ShowIncVat) return price.GetValueOrDefault(0) * vatRate;
            return price.GetValueOrDefault(0);
        }
    }
}
