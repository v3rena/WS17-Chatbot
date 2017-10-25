using Chatbot.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatbot.Models
{
    public class Message : IMessage
    {
        public Message(string content)
        {
            Content = content;
        }

        public string Content { get; set; }
    }
}
