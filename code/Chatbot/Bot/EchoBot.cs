using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chatbot.Bot
{
    public class EchoBot : IBot
    {
        public string GetName()
        {
            return "EchoBot";
        }

        public string ProcessMessage(string message)
        {
            return message;
        }
    }
}