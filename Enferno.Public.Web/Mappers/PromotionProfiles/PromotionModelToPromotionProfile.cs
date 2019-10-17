using AutoMapper;
using Enferno.Public.Web.Models;
using Enferno.StormApiClient.Shopping;

namespace Enferno.Public.Web.Mappers.PromotionProfiles
{
    public class PromotionModelToPromotionProfile : Profile
    {
        public PromotionModelToPromotionProfile()
        {
            CreateMap<PromotionModel, Promotion>()
                .ForMember(to => to.Images, opts => opts.Ignore()) //ignore for now.
                .ForMember(to => to.ImageKey, opts => opts.Ignore())
                .ForMember(to => to.ExtensionData, opts => opts.Ignore())
                .ForMember(to => to.AppliedAmount, opts => opts.Ignore())
                .ForMember(to => to.ProductFilters, opts => opts.Ignore())
                .ForMember(to => to.EffectSeed, opts => opts.Ignore())
                .ForMember(to => to.FreightDiscountPct, opts => opts.Ignore())
                .ForMember(to => to.IsStackable, opts => opts.Ignore())
                .ForMember(to => to.AppliedAmountIncVat, opts => opts.Ignore());
        }

        public override string ProfileName => GetType().Name;
    }
}
