using AutoMapper;
using Enferno.Public.InversionOfControl;
using Enferno.Public.Web.Models;
using Enferno.StormApiClient.Products;
using Enferno.Web.StormUtils;

namespace Enferno.Public.Web.Mappers
{
    public class ManufacturerToManufacturerModelProfile : Profile
    {
        protected override void Configure()
        {
            var siteRules = IoC.Resolve<ISiteRules>();
            Mapper.CreateMap<Manufacturer, ManufacturerModel>()
                .ForMember(to => to.ImageUrl, opts => opts.MapFrom(from => Link.ImageUrl(from.LogoKey, null)));
        }

        public override string ProfileName
        {
            get { return GetType().Name; }
        }
    }
}
