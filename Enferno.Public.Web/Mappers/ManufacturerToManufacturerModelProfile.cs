using AutoMapper;
using Enferno.Public.Web.Models;
using Enferno.StormApiClient.Products;
using Enferno.Web.StormUtils;

namespace Enferno.Public.Web.Mappers
{
    public class ManufacturerToManufacturerModelProfile : Profile
    {
        public ManufacturerToManufacturerModelProfile()
        {
            CreateMap<Manufacturer, ManufacturerModel>()
                .ForMember(to => to.ImageUrl, opts => opts.MapFrom(from => Link.ImageUrl(from.LogoKey, null)));
        }

        public override string ProfileName => GetType().Name;
    }
}
