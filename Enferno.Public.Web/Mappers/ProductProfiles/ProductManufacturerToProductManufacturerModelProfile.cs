using AutoMapper;
using Enferno.Public.Web.Models;
using Enferno.StormApiClient.Products;
using Enferno.Web.StormUtils;

namespace Enferno.Public.Web.Mappers.ProductProfiles
{
    public class ProductManufacturerToProductManufacturerModelProfile : Profile
    {
        public ProductManufacturerToProductManufacturerModelProfile()
        {
            CreateMap<ProductManufacturer, ProductManufacturerModel>()
                .ForMember(to => to.ImageUrl,
                    opts => opts.ResolveUsing(productManufacturer => Link.ImageUrl(productManufacturer.LogoKey, null)));

        }

        public override string ProfileName => GetType().Name;
    }
}
