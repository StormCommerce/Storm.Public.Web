
namespace Enferno.Public.Web.Repositories
{
    public class Repository : RepositoryBase, IRepository
    {
        public IApplicationRepository Application => new ApplicationRepository();

        public ICustomerRepository Customers => new CustomerRepository();

        public IProductRepository Products => new ProductRepository();

        public IShoppingRepository Shopping => new ShoppingRepository();
    }
}
