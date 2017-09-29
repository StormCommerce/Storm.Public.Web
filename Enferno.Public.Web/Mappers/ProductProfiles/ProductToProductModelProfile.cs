using AutoMapper;
using Enferno.Public.Web.Mappers.Resolvers;
using Enferno.Public.Web.Models;
using Enferno.StormApiClient.Products;

namespace Enferno.Public.Web.Mappers.ProductProfiles
{
    public class ProductToProductModelProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Product, ProductModel>()
                .ForMember(
                    to => to.Category,
                    opts =>
                        opts.ResolveUsing(
                            product =>
                            {
                                var model = new ProductCategoryModel {Name = product.CategoryName};
                                if (product.CategoryId.HasValue)
                                    model.Id = product.CategoryId.Value;
                                return model;
                            }))
                .ForMember(to => to.Flags, opts => opts.ResolveUsing<ProductFlagsResolver>())
                .ForMember(to => to.Files, opts => opts.ResolveUsing<ProductFilesResolver>())
                .ForMember(to => to.OnHandStatus, opts => opts.ResolveUsing<ProductOnHandStatusResolver>())
                .ForMember(to=> to.Price, opts=> opts.ResolveUsing<ProductPriceResolver>())
                .ForMember(to => to.UnitOfMeasurement, opts => opts.MapFrom(from => from.Uom))
                .ForMember(to => to.Families, opts => opts.Ignore()) //mapped in the builder
                .ForMember(to => to.Products, opts => opts.Ignore()) //mapped in the builder
                .ForMember(to => to.Variants, opts => opts.Ignore()) //mapped in the builder
                .ForMember(to => to.VariantParametrics, opts => opts.Ignore()) //mapped in the builder
                .ForMember(to => to.Parametrics, opts => opts.Ignore()) //mapped in the builder
                .ForMember(to => to.SelectedVariant, opts => opts.Ignore()); //not applicable?

        }

        public override string ProfileName => GetType().Name;
    }
}
