using Enferno.StormApiClient.Customers;
using Enferno.Web.StormUtils;

namespace Enferno.Public.Web.Repositories
{
    public class CustomerRepository : RepositoryBase, ICustomerRepository
    {
        public Customer GetExternalAddress(string ssn, bool useCache = true)
        {
            if (string.IsNullOrWhiteSpace(ssn)) return null;
            using (var client = GetBatch())
            {
                var customer = new Customer {SSN = ssn};
                client.UseCache = useCache;
                customer = client.CustomerProxy.GetExternalCustomerAddress2(customer, StormContext.CultureCode);
                return customer;
            }
        }

        public Customer Login(string userName, string password, bool useCache = true)
        {
            using (var client = GetBatch())
            {
                client.UseCache = useCache;
                var customer = client.CustomerProxy.Login(userName, password, StormContext.CultureCode);
                return customer;
            }
        }
    }
}
