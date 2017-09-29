using AutoMapper;
using Enferno.Public.Web.Models;
using Enferno.Public.Web.ViewModels;

namespace Enferno.Public.Web.Mappers.ProductProfiles
{
    public class ParametricValueModelToParametricValueViewModelProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<ParametricValueModel, ParametricValueViewModel>();
        }

        public override string ProfileName => GetType().Name;
    }
}
