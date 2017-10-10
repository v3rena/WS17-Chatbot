using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chatbot.Models
{
    public class Message
    {
        public Message() { }

        public int MessageID { get; set; }
        public DateTime Timestamp { get; set; }
        public string Text { get; set; } // Chat-Nachricht
        public string GUID { get; set; } // Eindeutige Session-ID
        public bool Usermessage { get; set; } // Zur Unterscheidung, ob die Nachricht vom User kommt oder nicht
    }
}