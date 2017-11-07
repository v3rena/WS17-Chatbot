using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Chatbot.Controllers
{
    [RoutePrefix("api/session")]
    public class SessionController : ApiController
    {
        [HttpGet]
        [Route("")]
        public HttpResponseMessage Get()
        {
            return new HttpResponseMessage()
            {
                Content = new StringContent(
                    String.Format("{{ \"guid\" : \"{0}\" }}", Guid.NewGuid().ToString()),
                    Encoding.UTF8,
                    "application/json"
                )
            };
        }
    }
}
