using System;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class StatusController : ApiController
    {
        [HttpGet]
        [Route("api/Status")]
        public IHttpActionResult GetStatus()
        {
            var status = new
            {
                SytemTime = DateTime.Now,
                APIVersion = typeof (StatusController).Assembly.GetName().Version
            };

            return Ok(status);
        }

        [HttpGet]
        [Route("api/Status/ThrowError")]
        public IHttpActionResult ThrowError()
        {
            throw new ApplicationException($"Error Test at {DateTime.Now}");
        }
    }
}