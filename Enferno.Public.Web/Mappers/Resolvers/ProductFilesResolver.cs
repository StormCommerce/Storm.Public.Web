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
    public abstract class ProductFilesBaseResolver<TDest> : IValueResolver<Product, TDest, List<ProductFileModel>>
    {
        protected ProductFileType FileType;

        public List<ProductFileModel> Resolve(Product source, TDest destination, List<ProductFileModel> destMember, ResolutionContext context)
        {
            var fileList = new List<ProductFileModel>();

            if (source.ImageKey.HasValue) fileList.Add(new ProductFileModel(FileType, Link.ImageUrl(source.ImageKey.ToString()), source.Name));

            return MapFiles(source, fileList);
        }

        protected static List<ProductFileModel> MapFiles(Product product, List<ProductFileModel> fileList)
        {
            if (product.Files == null || product.Files.Count <= 0) return fileList;

            foreach (var file in product.Files)
            {
                var url = file.Type == (int) Web.FileType.Embedded
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

    public class ProductFilesResolver : ProductFilesBaseResolver<ProductModel>
    {
        public ProductFilesResolver()
        {
            FileType = ProductFileType.DefaultImage;
        }
    }

    public class VariantFilesResolver : ProductFilesBaseResolver<VariantModel>
    {
        public VariantFilesResolver()
        {
            FileType = ProductFileType.VariantImage;
        }
    }

    public abstract class ProductItemFilesBaseResolver
    {
        protected List<ProductFileModel> ResolveBase(Guid? imageKey, string altName, string imageKeySeed, ProductFileType fileType)
        {
            var fileList = new List<ProductFileModel>();
            if (imageKey.HasValue) fileList.Add(new ProductFileModel(fileType, Link.ImageUrl(imageKey.ToString()), altName));
            return MapFiles(altName, imageKeySeed, fileList);
        }

        protected static List<ProductFileModel> MapFiles(string altText, string imageKeySeed, List<ProductFileModel> fileList)
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

    public class ProductItemFilesResolver : ProductItemFilesBaseResolver, IValueResolver<ProductItem, ProductItemModel, List<ProductFileModel>>
    {
        public List<ProductFileModel> Resolve(ProductItem source, ProductItemModel destination, List<ProductFileModel> destMember, ResolutionContext context)
        {
            return ResolveBase(source.ImageKey, source.Name, source.AdditionalImageKeySeed, ProductFileType.DefaultImage);
        }
    }

    public class VariantItemFilesResolver : ProductItemFilesBaseResolver, IValueResolver<ProductItem, VariantModel, List<ProductFileModel>>
    {
        public List<ProductFileModel> Resolve(ProductItem source, VariantModel destination, List<ProductFileModel> destMember, ResolutionContext context)
        {
            return ResolveBase(source.VariantImageKey, source.VariantName, source.AdditionalImageKeySeed, ProductFileType.VariantImage);
        }
    }

    public class VariantItemVariantFilesResolver : ProductItemFilesBaseResolver, IValueResolver<VariantItem, VariantModel, List<ProductFileModel>>
    {
        public List<ProductFileModel> Resolve(VariantItem source, VariantModel destination, List<ProductFileModel> destMember, ResolutionContext context)
        {
            return ResolveBase(source.ImageKey, source.Name, source.AdditionalImageKeySeed, ProductFileType.VariantImage);
        }
    }  
}
