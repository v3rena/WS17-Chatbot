using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatbot.Models
{
    public class SessionKey
    {
        public Guid Key { get; }

        public DateTime Timestamp { get; }

        public SessionKey()
        {
            Key = Guid.NewGuid();
            Timestamp = DateTime.Now;
        }
    }
}
