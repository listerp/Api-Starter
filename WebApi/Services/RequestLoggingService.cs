using System;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UAParser;
using WebApi.Helpers;
using WebApi.Models;
using WebApi.Repositories;

namespace WebApi.Services
{
    public class RequestLoggingService : DelegatingHandler
    {
        private readonly ApiRepository _repo;

        public RequestLoggingService()
        {
            _repo = new ApiRepository();
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var requestInfo = $"{request.Method} {request.RequestUri}";

            var req = ((dynamic) request.Properties["MS_HttpContext"]).Request;

            var requestMessage = await request.Content.ReadAsByteArrayAsync();

            var requestTime = DateTime.Now;

            var user = string.Empty;

            if (req.LogonUserIdentity.IsAuthenticated)
            {
                var principal = (ClaimsPrincipal) Thread.CurrentPrincipal;
                user = RequestHelpers.GetUserId(principal);
            }

            var headers = new StringBuilder();
            if (request.Headers != null)
            {
                foreach (var header in request.Headers)
                {
                    var val = new StringBuilder();
                    foreach (var keyValue in header.Value)
                    {
                        val.Append($"{keyValue},");
                    }

                    headers.AppendLine($"{header.Key} | {val}");
                }
            }

            ClientInfo ua = null;
            if (req != null && req.UserAgent != null)
            {
                ua = Parser.GetDefault().Parse(req.UserAgent);
            }

            var httpRequestLog = new HttpRequestLog
            {
                UserName = user,
                UserIpAddress = req.UserHostAddress,
                UserAgent = ua == null ? "" : ua.UserAgent.ToString(),
                DeviceInfo = ua == null ? "" : ua.Device.Family,
                BrowserInfo = ua == null ? "" : ua.UserAgent.Family,
                IsAnonymous = req.LogonUserIdentity.IsAnonymous,
                IsAuthenticated = req.LogonUserIdentity.IsAuthenticated,
                IsGuest = req.LogonUserIdentity.IsGuest,
                IsSystem = req.LogonUserIdentity.IsSystem,
                HttpAction = request.Method.ToString(),
                RequestUrl = request.RequestUri.ToString(),
                RequestHeader = headers.ToString(),
                RequestBody = RequestHelpers.GetRequestBody(request),
                RequestTimeStamp = requestTime
            };

            await _repo.InsertHttpRequestLogAsync(httpRequestLog);

            var response = await base.SendAsync(request, cancellationToken);

            byte[] responseMessage;

            if (response.IsSuccessStatusCode)
                responseMessage = await response.Content.ReadAsByteArrayAsync();
            else
                responseMessage = Encoding.UTF8.GetBytes(response.ReasonPhrase);

            var responseTime = DateTime.Now;

            httpRequestLog.Response = Convert.ToInt32(response.StatusCode);
            httpRequestLog.ResponseBody = Encoding.UTF8.GetString(responseMessage);
            httpRequestLog.ResposneTimeStamp = responseTime;
            httpRequestLog.RequestTotalTime = responseTime - requestTime;

            await _repo.FinalizeRequestLogAsync(httpRequestLog);

            return response;
        }
    }
}