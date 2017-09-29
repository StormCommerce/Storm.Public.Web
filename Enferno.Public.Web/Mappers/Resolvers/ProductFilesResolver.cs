using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using AutoMapper;
using Enferno.Public.Web.Models;
using Enferno.StormApiClient.Products;
using Enferno.Web.StormUtils;

namespace Enferno.Public.Web.Mappers.Resolvers
{
    public abstract class ProductFilesBaseResolver : ValueResolver<Product, IEnumerable<ProductFileModel>>
    {
        protected IEnumerable<ProductFileModel> ResolveBase(Product product, ProductFileType fileType)
        {
            var fileList = new List<ProductFileModel>();

            if (product.ImageKey.HasValue) fileList.Add(new ProductFileModel(fileType, Link.ImageUrl(product.ImageKey.ToString()), product.Name));

            return MapFiles(product, fileList);
        }  

        protected static IEnumerable<ProductFileModel> MapFiles(Product product, List<ProductFileModel> fileList)
        {
            if (product.Files == null || product.Files.Count <= 0) return fileList;

            foreach (var file in product.Files)
            {
                var url = file.Type == (int) FileType.Embedded
                    ? file.Path
                    : Link.ImageUrl(file.Key.ToString(), GetFileExtension(file.Type));

                if (!fileList.Exists(f => f.Url == url))
                {
                    fileList.Add(new ProductFileModel
                    {
                        Type = file.Id,
                        Url = url,
                        AltText = file.Name ?? product.Name
                    });
                }
            }

            return fileList;
        }

        private static string GetFileExtension(int fileType)
        {
            if (Enum.IsDefined(typeof(FileType), fileType))
            {
                return ((FileType)fileType).ToString().ToLower();
            }
            return Enum.IsDefined(typeof(ExternalFileType), fileType) ? ((ExternalFileType)fileType).ToString().ToLower() : "";
        }

    }

    public class ProductFilesResolver : ProductFilesBaseResolver
    {
        protected override IEnumerable<ProductFileModel> ResolveCore(Product product)
        {
            return ResolveBase(product, ProductFileType.DefaultImage);
        }     
    }

    public class VariantFilesResolver : ProductFilesBaseResolver
    {
        protected override IEnumerable<ProductFileModel> ResolveCore(Product product)
        {
            return ResolveBase(product, ProductFileType.VariantImage);
        }
    }

    public abstract class ProductItemFilesBaseResolver<T> : ValueResolver<T, IEnumerable<ProductFileModel>>
    {
        protected IEnumerable<ProductFileModel> ResolveBase(Guid? imageKey, string altName, string imageKeySeed, ProductFileType fileType)
        {
            var fileList = new List<ProductFileModel>();            
            if (imageKey.HasValue) fileList.Add(new ProductFileModel(fileType, Link.ImageUrl(imageKey.ToString()), altName));
            return MapFiles(altName, imageKeySeed, fileList);
        }

        protected static IEnumerable<ProductFileModel> MapFiles(string altText, string imageKeySeed, List<ProductFileModel> fileList)
        {
            if (string.IsNullOrWhiteSpace(imageKeySeed) || !imageKeySeed.Split(',').Any()) return fileList;
            foreach (var imageKey in imageKeySeed.Split(','))
            {
                var parts = imageKey.Split(':');
                var id = parts[0];
                var key = parts[1];
                var url = Link.ImageUrl(key, "jpg");
                if (!fileList.Exists(f => f.Url.Equals(url, StringComparison.CurrentCultureIgnoreCase) && f.Type.ToString(CultureInfo.InvariantCulture) == id))
                {
                    fileList.Add(new ProductFileModel(int.Parse(id), url, altText)); 
                }
            }
            return fileList;
        }   
    }

    public class ProductItemFilesResolver : ProductItemFilesBaseResolver<ProductItem>
    {
        protected override IEnumerable<ProductFileModel> ResolveCore(ProductItem productItem)
        {
            return ResolveBase(productItem.ImageKey, productItem.Name, productItem.AdditionalImageKeySeed, ProductFileType.DefaultImage);
        }
    }

    public class VariantItemFilesResolver : ProductItemFilesBaseResolver<ProductItem>
    {
        protected override IEnumerable<ProductFileModel> ResolveCore(ProductItem productItem)
        {
            return ResolveBase(productItem.VariantImageKey, productItem.VariantName, productItem.AdditionalImageKeySeed, ProductFileType.VariantImage);
        }
    }

    public class VariantItemVariantFilesResolver : ProductItemFilesBaseResolver<VariantItem>
    {
        protected override IEnumerable<ProductFileModel> ResolveCore(VariantItem variantItem)
        {
            return ResolveBase(variantItem.ImageKey, variantItem.Name, variantItem.AdditionalImageKeySeed, ProductFileType.VariantImage);
        }
    }  
}
