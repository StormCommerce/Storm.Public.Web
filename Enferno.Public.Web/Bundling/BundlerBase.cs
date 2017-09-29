using System.Web.Optimization;

namespace Enferno.Public.Web.Bundling
{
    public abstract class BundlerBase
    {
        public string BundlePath { get; protected set; }

        protected IBundleTransform Jstransformer;
        protected IBundleTransform Csstransformer;

#if DEBUG
        protected static bool Debug = true;
#else
        protected static bool Debug = false;
#endif

        protected BundlerBase(string bundlePath)
        {
            BundlePath = bundlePath;

            if (Debug)
            {
                Jstransformer = new NoTransform("text/javascript");
                Csstransformer = new NoTransform("text/css");
            }
            else
            {
                Jstransformer = new JsMinify();
                Csstransformer = new CssMinify();                
            }
        }

        public Bundle GetBundle()
        {
            var bundle = new Bundle(BundlePath);
            return SetupBundle(bundle);
        }

        protected abstract Bundle SetupBundle(Bundle bundle);
    }
}
