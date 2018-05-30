using AutoMapper;
using Enferno.Public.Web.Models;
using Enferno.Public.Web.ViewModels;

namespace Enferno.Public.Web.Mappers.PromotionProfiles
{
    public class PromotionModelToPromotionViewModelProfile : Profile
    {
        public PromotionModelToPromotionViewModelProfile()
        {
            CreateMap<PromotionModel, PromotionViewModel>();
        }

        public override string ProfileName => GetType().Name;
    }
}
