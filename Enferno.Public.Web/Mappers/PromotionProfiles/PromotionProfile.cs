using AutoMapper;
using Enferno.Public.Web.Mappers.ProductProfiles;

namespace Enferno.Public.Web.Mappers.PromotionProfiles
{
    public class PromotionProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.Configuration.AddProfile<PromotionToPromotionModelProfile>();
            Mapper.Configuration.AddProfile<PromotionModelToPromotionProfile>();
            Mapper.Configuration.AddProfile<PromotionModelToPromotionViewModelProfile>();
            Mapper.Configuration.AddProfile<PromotionViewModelToPromotionModelProfile>();
        }
    }
}
