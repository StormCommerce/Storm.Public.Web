
using System.Collections;
using System.Linq;
using System.Text;
using System.Web;

namespace Enferno.Public.Web
{
    public static class Extensions
    {
        /// <summary>
        /// Dumps the HttpContext.Items for debug tracing and logging.
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns>Returns all Key/Value-pairs separated by CRLF.</returns>
        public static string Dump(this HttpContext ctx)
        {
            var buff = new StringBuilder();
            foreach (DictionaryEntry item in ctx.Items)
            {
                buff.AppendFormat("{0}:{1}\r\n", item.Key, item.Value);
            }
            return buff.ToString();
        }

        /// <summary>
        /// Dumps the HttpRequest.Params for debug tracing and logging.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Returns all Key/Value-pairs separated by CRLF.</returns>
        public static string Dump(this HttpRequest request)
        {
            var buff = new StringBuilder();
            foreach (var key in request.Params.Cast<string>().Where(key => !string.IsNullOrWhiteSpace(request.Params[key])))
            {
                buff.AppendFormat("{0}:{1}\r\n", key, request.Params[key]);
            }
            return buff.ToString();
        }
    }
}
