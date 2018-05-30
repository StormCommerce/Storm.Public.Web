using System.Collections.Generic;
using AutoMapper;
using Enferno.Public.Web.Models;
using Enferno.Public.Web.ViewModels;

namespace Enferno.Public.Web.Mappers.ProductProfiles
{
    public class ProductModelToProductViewModelProfile : Profile
    {
        public ProductModelToProductViewModelProfile()
        {
            CreateMap<ProductModel, ProductViewModel>()
                .ForMember(to=> to.OnHandStatusText1, opts=> opts.MapFrom(from=> from.OnHandStatus.Text1))
                .ForMember(to => to.OnHandStatusText2, opts => opts.MapFrom(from => from.OnHandStatus.Text2))
                .ForMember(to => to.OnHandStatus, opts => opts.MapFrom(from => from.OnHandStatus.Status))
                .ForMember(to => to.OnHandStatusCount, opts => opts.MapFrom(from => from.OnHandStatus.Count))
                .ForMember(to => to.DisplayPrice, opts => opts.MapFrom(from => from.Price.Display))
                .ForMember(to => to.CatalogPrice, opts => opts.MapFrom(from => from.Price.Catalog))
                .ForMember(to => to.VatRate, opts => opts.MapFrom(from => from.Price.VatRate))
                .ForMember(to => to.PriceListId, opts => opts.MapFrom(from => from.Price.PricelistId))
                .ForMember(to => to.Files,
                    opts =>
                        opts.ResolveUsing(from => Mapper.Map<IList<ProductFileModel>, IList<FileViewModel>>(from.Files)))
                .ForMember(to => to.VariantParametrics,
                    opts =>
                        opts.ResolveUsing(
                            from =>
                                Mapper.Map<IList<ParametricModel>, IList<ParametricViewModel>>(from.VariantParametrics)))
                .ForMember(to => to.Variants,
                    opts =>
                        opts.ResolveUsing(
                            from =>
                                Mapper.Map<IList<VariantModel>, IList<VariantViewModel>>(from.Variants)));

        }

        public override string ProfileName => GetType().Name;
    }
}
