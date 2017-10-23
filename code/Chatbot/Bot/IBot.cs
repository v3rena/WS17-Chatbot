using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chatbot.Bot
{
    public interface IBot
    {
        string GetName();

        string ProcessMessage(string message);
    }
}