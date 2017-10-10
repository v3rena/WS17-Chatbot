using Chatbot.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;

namespace Chatbot.Controllers
{
    public class MessageController : ApiController
    {
        [HttpPost]
        public IHttpActionResult PostMessage(MessageViewModel message)
        {
            return Ok(new MessageViewModel("test"));
        }
    }
}