using AutoMapper;
using Enferno.Public.Web.Models;
using Enferno.Public.Web.ViewModels;

namespace Enferno.Public.Web.Mappers.PromotionProfiles
{
    public class PromotionViewModelToPromotionModelProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<PromotionViewModel, PromotionModel>()
                .ForMember(to => to.AllowProductListing, opts => opts.Ignore())
                .ForMember(to => to.Images, opts => opts.Ignore())
                .ForMember(to => to.IsExcludedFromPriceCalculation, opts => opts.Ignore());
        }

        public override string ProfileName
        {
            get { return GetType().Name; }
        }
    }
}
