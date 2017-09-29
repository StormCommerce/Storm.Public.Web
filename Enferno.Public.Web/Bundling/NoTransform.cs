using System.Web.Optimization;

namespace Enferno.Public.Web.Bundling
{
    public class NoTransform : IBundleTransform
    {
        private readonly string _contentType;

        public NoTransform(string contentType)
        {
            _contentType = contentType;
        }

        public void Process(BundleContext context, BundleResponse response)
        {
            response.ContentType = _contentType;
        }
    }
}
