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
        public DateTime Timestamp { get; set; }
        public string GUID { get; set; } // Eindeutige Session-ID
        public bool Usermessage { get; set; } // Zur Unterscheidung, ob die Nachricht vom User kommt oder nicht

        public Message()
        {

        }

        public Message(string content)
        {
            Content = content;
            Timestamp = DateTime.Now;
        }

    }
}
