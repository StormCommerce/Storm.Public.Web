﻿
using AutoMapper;
using Enferno.Public.Web.Models;
using Enferno.Public.Web.ViewModels;

namespace Enferno.Public.Web.Mappers.ProductProfiles
{
    public class ProductFileModelToFileViewModelProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<ProductFileModel, FileViewModel>()
                .ForMember(to => to.Type, opts => opts.MapFrom(from => from.Type));
        }

        public override string ProfileName => GetType().Name;
    }
}
