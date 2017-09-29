using AutoMapper;
using Enferno.Public.Web.Mappers.Resolvers;
using Enferno.Public.Web.Models;
using Enferno.StormApiClient.Products;

namespace Enferno.Public.Web.Mappers
{
    public class WarehouseModelToWarehouseProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<WarehouseModel, Warehouse>()
                .ForMember(to => to.StoreId, opts => opts.MapFrom(from => from.StoreId))
                .ForMember(to => to.LocationId, opts => opts.MapFrom(from => from.LocationId))
                .ForMember(to => to.WarehouseId, opts => opts.MapFrom(from => from.WarehouseId))
                .ForMember(to => to.OnHand, opts => opts.Ignore());
        }

        public override string ProfileName
        {
            get { return GetType().Name; }
        }
    }
}
