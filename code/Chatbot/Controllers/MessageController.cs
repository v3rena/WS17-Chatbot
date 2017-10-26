using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Chatbot.Interfaces;
using Chatbot.Interfaces.DTOs;
using System.Web.Http.Description;

namespace Chatbot.Controllers
{
    [RoutePrefix("api/message")]
    public class MessageController : ApiController
    {
        private readonly IBusinessLayer _bl;

        public MessageController(IBusinessLayer bl)
        {
            _bl = bl;
        }

        // POST api/message/
        // Request URL:http://localhost:65118/api/message/
        // Content-Type:application/x-www-form-urlencoded; charset=UTF-8
        //
        // Content=...

        [HttpPost]
        [Route("")]
        [ResponseType(typeof(IMessage))]
        public IMessage PostMessage([FromBody]IMessage message)
        {
            return _bl.ProcessMessage(message);
        }
    }
}
