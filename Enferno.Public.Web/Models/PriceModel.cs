

using System;

namespace Enferno.Public.Web.Models
{
    /// <summary>
    /// Prices are calulated and shown as inc or ex vat depending on StormContext.
    /// </summary>
    [Serializable]
    public class PriceModel
    {
        public decimal Display { get; set; }
        public decimal Catalog { get; set; }
        public decimal Recommended { get; set; }
        public decimal Original { get; set; }
        public decimal VatRate { get; set; }
        public int PricelistId { get; set; }

        /// <summary>
        /// Indicates if variants for a product has different prices. Can be used by front-end to indicate a "from price".
        /// </summary>
        public bool IsFromPrice { get; set; }
    }
}
