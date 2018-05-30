using AutoMapper;
using Enferno.Public.Web.Mappers.Resolvers;
using Enferno.Public.Web.Models;
using Enferno.StormApiClient.Products;

namespace Enferno.Public.Web.Mappers
{
    public class WarehouseToWarehouseModelProfile : Profile
    {
        public WarehouseToWarehouseModelProfile()
        {
            CreateMap<Warehouse, WarehouseModel>()
                .ForMember(to => to.StoreId, opts => opts.MapFrom(from => from.StoreId))
                .ForMember(to => to.LocationId, opts => opts.MapFrom(from => from.LocationId))
                .ForMember(to => to.WarehouseId, opts => opts.MapFrom(from => from.WarehouseId))
                .ForMember(to => to.OnHand, opts => opts.ResolveUsing<WarehouseOnHandStatusResolver>());
        }

        public override string ProfileName => GetType().Name;
    }
}
