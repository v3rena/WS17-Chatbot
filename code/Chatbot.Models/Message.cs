using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatbot.Models
{
    public class Message 
    {
        public int MessageID { get; set; }
        public string Content { get; set; }

        public Message()
        {

        }

        public Message(string content)
        {
            Content = content;
        }
    }
}
