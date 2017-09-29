
using Enferno.StormApiClient.Applications;

namespace Enferno.Public.Web.Repositories
{
    public interface IApplicationRepository
    {
        StoreList ListStores(string cultureCode = RepositoryBase.Ignored, bool useCache = true);
    }
}
