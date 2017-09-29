using System.Collections.Generic;
using AutoMapper;
using Enferno.StormApiClient.Products;

namespace Enferno.Public.Web.Mappers.Resolvers
{
    public class ProductItemFlagsResolver : ValueResolver<ProductItem, IEnumerable<int>>
    {
        protected override IEnumerable<int> ResolveCore(ProductItem productItem)
        {
            var flagList = new List<int>();

            if (string.IsNullOrWhiteSpace(productItem.FlagIdSeed)) return flagList;

            

            foreach (var flag in productItem.FlagIdSeed.Split(','))
            {
                int id;
                if (int.TryParse(flag.Trim(), out id) && !flagList.Contains(id)) flagList.Add(id);
            }

            return flagList;
        }
    }

    public class ProductItemVariantFlagsResolver : ValueResolver<ProductItem, IEnumerable<int>>
    {
        protected override IEnumerable<int> ResolveCore(ProductItem productItem)
        {
            var flagList = new List<int>();

            if (string.IsNullOrWhiteSpace(productItem.VariantFlagIdSeed)) return flagList;



            foreach (var flag in productItem.VariantFlagIdSeed.Split(','))
            {
                int id;
                if (int.TryParse(flag.Trim(), out id) && !flagList.Contains(id)) flagList.Add(id);
            }

            return flagList;
        }
    }

    public class VariantItemVariantFlagsResolver : ValueResolver<VariantItem, IEnumerable<int>>
    {
        protected override IEnumerable<int> ResolveCore(VariantItem variantItem)
        {
            var flagList = new List<int>();

            if (string.IsNullOrWhiteSpace(variantItem.FlagIdSeed)) return flagList;



            foreach (var flag in variantItem.FlagIdSeed.Split(','))
            {
                int id;
                if (int.TryParse(flag.Trim(), out id) && !flagList.Contains(id)) flagList.Add(id);
            }

            return flagList;
        }
    }

    public class ProductFlagsResolver : ValueResolver<Product, IEnumerable<int>>
    {
        protected override IEnumerable<int> ResolveCore(Product product)
        {
            var flagList = new List<int>();

            if (string.IsNullOrWhiteSpace(product.FlagIdSeed)) return flagList;



            foreach (var flag in product.FlagIdSeed.Split(','))
            {
                int id;
                if (int.TryParse(flag.Trim(), out id) && !flagList.Contains(id)) flagList.Add(id);
            }

            return flagList;
        }
    }
}
