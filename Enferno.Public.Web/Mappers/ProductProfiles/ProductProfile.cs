using AutoMapper;

namespace Enferno.Public.Web.Mappers.ProductProfiles
{
    public class ProductProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.Configuration.AddProfile<ProductManufacturerToProductManufacturerModelProfile>();
            Mapper.Configuration.AddProfile<ProductFileToProductFileModelProfile>();
            Mapper.Configuration.AddProfile<ProductItemToProductItemModelProfile>();
            Mapper.Configuration.AddProfile<ProductItemToVariantModelProfile>();
            Mapper.Configuration.AddProfile<VariantItemToVariantModelProfile>();
            Mapper.Configuration.AddProfile<ProductToVariantModelProfile>();
            Mapper.Configuration.AddProfile<ProductToProductModelProfile>();

            Mapper.Configuration.AddProfile<ManufacturerToManufacturerModelProfile>();

            Mapper.Configuration.AddProfile<ProductModelToProductViewModelProfile>();
            Mapper.Configuration.AddProfile<ProductFileModelToFileViewModelProfile>();
            Mapper.Configuration.AddProfile<ParametricModelToParametricViewModelProfile>();
            Mapper.Configuration.AddProfile<ParametricValueModelToParametricValueViewModelProfile>();
            Mapper.Configuration.AddProfile<VariantModelToVariantViewModelProfile>();
        }
    }
}
