using Chatbot.DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatbot.DataAccessLayer.Entities
{
    public class SessionKey : IEntity
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
