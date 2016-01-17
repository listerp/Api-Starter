using System;

namespace WebApi.Models
{
    public class HttpRequestLog
    {
        public HttpRequestLog(Guid id)
        {
            Id = id;
        }

        public HttpRequestLog()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string UserIpAddress { get; set; }
        public string HttpAction { get; set; }
        public string RequestUrl { get; set; }
        public string RequestHeader { get; set; }
        public string RequestBody { get; set; }
        public int Response { get; set; }
        public string ResponseBody { get; set; }
        public string UserAgent { get; set; }
        public string DeviceInfo { get; set; }
        public string BrowserInfo { get; set; }
        public bool IsAnonymous { get; set; }
        public bool IsAuthenticated { get; set; }
        public bool IsGuest { get; set; }
        public bool IsSystem { get; set; }
        public DateTime RequestTimeStamp { get; set; }
        public DateTime? ResposneTimeStamp { get; set; }
        public TimeSpan? RequestTotalTime { get; set; }
    }
}