using AutoMapper;
using Enferno.Public.Web.Models;
using Enferno.Public.Web.ViewModels;

namespace Enferno.Public.Web.Mappers.ProductProfiles
{
    public class ParametricValueModelToParametricValueViewModelProfile : Profile
    {
        public ParametricValueModelToParametricValueViewModelProfile()
        {
            CreateMap<ParametricValueModel, ParametricValueViewModel>();
        }

        public override string ProfileName => GetType().Name;
    }
}
