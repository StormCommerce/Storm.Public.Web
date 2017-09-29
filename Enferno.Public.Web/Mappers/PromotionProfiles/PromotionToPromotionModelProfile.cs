using AutoMapper;
using Enferno.Public.Web.Models;
using Enferno.StormApiClient.Shopping;
using Enferno.Web.StormUtils;

namespace Enferno.Public.Web.Mappers.PromotionProfiles
{
    public class PromotionToPromotionModelProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Promotion, PromotionModel>()
                .ForMember(to => to.Images, opts => opts.Ignore()) //ignore for now.
                .ForMember(to => to.ImageUrl,
                    opts =>
                        opts.MapFrom(from => from.ImageKey.HasValue ? Link.ImageUrl(from.ImageKey.ToString()) : null));
        }

        public override string ProfileName
        {
            get { return GetType().Name; }
        }
    }
}
