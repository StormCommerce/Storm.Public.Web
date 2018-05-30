
using System.Collections.Generic;
using AutoMapper;
using Enferno.Public.Web.Models;
using Enferno.StormApiClient.Products;

namespace Enferno.Public.Web.Mappers.Resolvers
{
    public class ProductItemFlagsResolver : IValueResolver<ProductItem, ProductItemModel, List<int>>
    {
        public List<int> Resolve(ProductItem source, ProductItemModel destination, List<int> destMember, ResolutionContext context)
        {
            var flagList = new List<int>();

            if (string.IsNullOrWhiteSpace(source.FlagIdSeed)) return flagList;

            foreach (var flag in source.FlagIdSeed.Split(','))
            {
                int id;
                if (int.TryParse(flag.Trim(), out id) && !flagList.Contains(id)) flagList.Add(id);
            }

            return flagList;
        }
    }

    public class ProductItemVariantFlagsResolver<TDest> : IValueResolver<ProductItem, TDest, List<int>>
    {
        public List<int> Resolve(ProductItem source, TDest destination, List<int> destMember, ResolutionContext context)
        {
            var flagList = new List<int>();

            if (string.IsNullOrWhiteSpace(source.VariantFlagIdSeed)) return flagList;

            foreach (var flag in source.VariantFlagIdSeed.Split(','))
            {
                int id;
                if (int.TryParse(flag.Trim(), out id) && !flagList.Contains(id)) flagList.Add(id);
            }

            return flagList;
        }
    }

    public class VariantItemVariantFlagsResolver : IValueResolver<VariantItem, VariantModel, List<int>>
    {
        public List<int> Resolve(VariantItem source, VariantModel destination, List<int> destMember, ResolutionContext context)
        {
            var flagList = new List<int>();

            if (string.IsNullOrWhiteSpace(source.FlagIdSeed)) return flagList;

            foreach (var flag in source.FlagIdSeed.Split(','))
            {
                int id;
                if (int.TryParse(flag.Trim(), out id) && !flagList.Contains(id)) flagList.Add(id);
            }

            return flagList;
        }
    }

    public class ProductFlagsResolver<TDest> : IValueResolver<Product, TDest, List<int>>
    {
        public List<int> Resolve(Product source, TDest destination, List<int> destMember, ResolutionContext context)
        {
            var flagList = new List<int>();

            if (string.IsNullOrWhiteSpace(source.FlagIdSeed)) return flagList;

            foreach (var flag in source.FlagIdSeed.Split(','))
            {
                int id;
                if (int.TryParse(flag.Trim(), out id) && !flagList.Contains(id)) flagList.Add(id);
            }

            return flagList;
        }
    }
}
