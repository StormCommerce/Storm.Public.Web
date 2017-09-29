using Enferno.StormApiClient;

namespace Enferno.Public.Web.Repositories
{
    public interface IRepository
    {
        IAccessClient GetBatch();

        IApplicationRepository Application { get; }
        ICustomerRepository Customers { get; }
        IProductRepository Products { get; }
        IShoppingRepository Shopping { get; }
    }
}
