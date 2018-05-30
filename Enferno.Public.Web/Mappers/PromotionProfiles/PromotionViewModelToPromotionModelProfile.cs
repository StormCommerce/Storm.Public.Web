using AutoMapper;
using Enferno.Public.Web.Models;
using Enferno.Public.Web.ViewModels;

namespace Enferno.Public.Web.Mappers.PromotionProfiles
{
    public class PromotionViewModelToPromotionModelProfile : Profile
    {
        public PromotionViewModelToPromotionModelProfile()
        {
            CreateMap<PromotionViewModel, PromotionModel>()
                .ForMember(to => to.AllowProductListing, opts => opts.Ignore())
                .ForMember(to => to.Images, opts => opts.Ignore())
                .ForMember(to => to.IsExcludedFromPriceCalculation, opts => opts.Ignore());
        }

        public override string ProfileName => GetType().Name;
    }
}
