using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Web;
using Enferno.StormApiClient.Applications;
using Enferno.Web.StormUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Enferno.Public.Web.Test.Repositories
{
    public class RepositoriesBaseTests
    {
        [TestInitialize]
        public void InitializeTest()
        {
            var stormUtilsRepo = new Mock<Enferno.Web.StormUtils.InternalRepository.IRepository>();

            var application = new Application
            {
                Currencies = new Currencies { Default = new Currency { Id = 2 } },
                Cultures = new Cultures { Default = new Culture { Code = "sv-SE" } },
                SalesAreas = new SalesAreas { Default = new SalesArea { Id = 1 } },
            };

            var httpContextWrapper = new NoHttpContext(application);
            var ctx = new StormContext(stormUtilsRepo.Object, httpContextWrapper);
            StormContext.SetInstance(ctx);
        }
    }

    internal class NoHttpContext : IHttpContextWrapper
    {
        private readonly HttpContext context = new HttpContext(new HttpRequest("", "http://tempuri.org", ""), new HttpResponse(new StringWriter()));
        private readonly Dictionary<string, object> items = new Dictionary<string, object>();

        public IDictionary Items => items;

        public HttpCookieCollection RequestCookies => context.Request.Cookies;

        public HttpCookieCollection ResponseCookies => context.Response.Cookies;

        public NoHttpContext(Application application)
        {
            items.Add("StormSession", new SessionCookie());
            items.Add("StormPersisted", new PersistedCookie(application));
        }
        public bool IsSsl => false;
    }
}
