using System;

namespace Chatbot.BusinessLayer.Models
{
    public class SessionKey
    { 
        public Guid Key { get; set; }

        public DateTime Timestamp { get; set; }

        public SessionKey()
        {
            Key = Guid.NewGuid();
            Timestamp = DateTime.Now;
        }
    }
}
