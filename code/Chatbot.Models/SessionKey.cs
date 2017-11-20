﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatbot.Models
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
