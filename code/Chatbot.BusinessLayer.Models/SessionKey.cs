using System;

namespace Chatbot.BusinessLayer.Models
{
    public class SessionKey
    {
        public int Id { get; set; }

        public Guid Key { get; set; }

        public DateTime Timestamp { get; set; }

        public SessionKey()
        {
            Key = Guid.NewGuid();
            Timestamp = DateTime.Now;
        }
    }
}
