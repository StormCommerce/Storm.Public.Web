
using Enferno.StormApiClient;
using Enferno.StormApiClient.Applications;

namespace Enferno.Public.Web.Repositories
{
    public class ApplicationRepository : RepositoryBase, IApplicationRepository
    {
        public ApplicationRepository() : this(null)
        { }

        public ApplicationRepository(IAccessClient client)
        {
            MyClient = client;
        }

        public StoreList ListStores(string cultureCode = Ignored, bool useCache = true)
        {
            using (var client = GetBatch())
            {
                client.UseCache = useCache;
                return client.ApplicationProxy.ListStores2(CultureCode(cultureCode));
            }
        }
    }
}
