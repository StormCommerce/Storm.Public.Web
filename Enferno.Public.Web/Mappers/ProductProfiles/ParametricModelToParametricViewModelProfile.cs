
using AutoMapper;
using Enferno.Public.Web.Models;
using Enferno.Public.Web.ViewModels;

namespace Enferno.Public.Web.Mappers.ProductProfiles
{
    public class ParametricModelToParametricViewModelProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<ParametricModel, ParametricViewModel>()
                .ForMember(to => to.UnitOfMeasurement, opts => opts.MapFrom(from => from.Uom));
        }

        public override string ProfileName => GetType().Name;
    }
}
