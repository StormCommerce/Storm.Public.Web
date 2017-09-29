using Enferno.StormApiClient.Customers;

namespace Enferno.Public.Web.Repositories
{
    public interface ICustomerRepository
    {
        Customer GetExternalAddress(string ssn, bool useCache = true);
        Customer Login(string userName, string password, bool useCache = true);
    }
}
