
using AutoMapper;
using Enferno.Public.Web.Models;
using Enferno.StormApiClient.Products;

namespace Enferno.Public.Web.Mappers
{
    public class WarehouseModelToWarehouseProfile : Profile
    {
        public WarehouseModelToWarehouseProfile()
        {
            CreateMap<WarehouseModel, Warehouse>()
                .ForMember(to => to.StoreId, opts => opts.MapFrom(from => from.StoreId))
                .ForMember(to => to.LocationId, opts => opts.MapFrom(from => from.LocationId))
                .ForMember(to => to.WarehouseId, opts => opts.MapFrom(from => from.WarehouseId))
                .ForMember(to => to.ExtensionData, opts => opts.Ignore());                
        }

        public override string ProfileName => GetType().Name;
    }
}
