using Chatbot.Models;
using Chatbot.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Chatbot.Controllers
{
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
        public string Index([FromBody] Message m)
        {
            return _bl.ProcessMessage(m.Content);
        }
    }
}
