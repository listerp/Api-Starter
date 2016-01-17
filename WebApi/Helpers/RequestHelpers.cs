using System;
using System.IO;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web;

namespace WebApi.Helpers
{
    public class RequestHelpers
    {
        public static string GetUserId(ClaimsPrincipal principal)
        {
            var userId = string.Empty;

            try
            {
                userId =
                    principal.FindFirst(ClaimTypes.NameIdentifier).Value;
            }
            catch (Exception)
            {
#if DEBUG
                userId = "auth0|56262b38fa9ddfa52a21efa6";
#else
                throw;
#endif
            }

            return userId;
        }

        public static string GetRequestBody(HttpRequestMessage request)
        {
            string requestBody;

            using (var stream = new MemoryStream())
            {
                var context = (HttpContextBase) request.Properties["MS_HttpContext"];
                context.Request.InputStream.Seek(0, SeekOrigin.Begin);
                context.Request.InputStream.CopyTo(stream);
                requestBody = Encoding.UTF8.GetString(stream.ToArray());
            }

            return requestBody;
        }
    }
}