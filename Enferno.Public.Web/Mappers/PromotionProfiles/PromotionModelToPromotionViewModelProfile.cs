using AutoMapper;
using Enferno.Public.Web.Models;
using Enferno.Public.Web.ViewModels;

namespace Enferno.Public.Web.Mappers.PromotionProfiles
{
    public class PromotionModelToPromotionViewModelProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<PromotionModel, PromotionViewModel>();
        }

        public override string ProfileName
        {
            get { return GetType().Name; }
        }
    }
}
