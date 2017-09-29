
using System.Web;
using Enferno.Public.Logging;

namespace Enferno.Public.Web
{
    public static class Logger
    {
        /// <summary>
        /// LogApplicationError is used to log errors on Application level.
        /// </summary>
        public static void LogApplicationError()
        {
            var ex = HttpContext.Current.AllErrors != null && HttpContext.Current.AllErrors.Length > 0 ? HttpContext.Current.AllErrors[0] : null;
            if (ex == null  || ex is System.Threading.ThreadAbortException) return;

            var log = Log.LogEntry.Categories(CategoryFlags.Alert | CategoryFlags.Debug)
                .Categories(HttpContext.Current.Request.Url.Host)
                .Exceptions(ex)
                .Property("HttpContext", HttpContext.Current.Dump())
                .Property("Request", HttpContext.Current.Request.Dump())
                .Message("Error caught in {0}", HttpContext.Current.Request.Url);

            if (ex is HttpException) log.WriteWarning();
            else log.WriteError();
        }
    }
}
