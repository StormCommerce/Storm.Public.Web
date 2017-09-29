using AutoMapper;
using Enferno.Public.InversionOfControl;
using Enferno.Public.Web.Builders;
using Enferno.Public.Web.Models;
using Enferno.Public.Web.Repositories;
using Enferno.Public.Web.ViewModels;

namespace Enferno.Public.Web.Managers
{
    public class ProductManager : IProductManager
    {
        protected readonly IProductRepository ProductRepository;
        protected readonly ProductBuilder ProductBuilder;

        public ProductManager()
        {
            ProductRepository = IoC.Resolve<IProductRepository>();
            ProductBuilder = new ProductBuilder();
        }

        public ProductViewModel GetProduct(int id)
        {
            var product = ProductRepository.GetProduct(id);
            var productModel = ProductBuilder.BuildProductModel(product);
            return Mapper.Map<ProductModel, ProductViewModel>(productModel);
        }
    }
}
