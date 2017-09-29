
using AutoMapper;
using Enferno.Public.Web.Models;
using Enferno.StormApiClient.Products;
using Enferno.Web.StormUtils;

namespace Enferno.Public.Web.Mappers.ProductProfiles
{
    public class ProductFileToProductFileModelProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<ProductFile, ProductFileModel>()
                .ForMember(to => to.AltText, opts => opts.MapFrom(file => file.Description))
                .ForMember(to => to.Type, opts => opts.MapFrom(file => file.Id))
                .ForMember(to => to.Url, opts => opts.MapFrom(file => Link.ImageUrl(file.Key, file.Id)));
        }
    }
}
