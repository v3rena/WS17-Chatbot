﻿using Chatbot.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Chatbot.Context
{
    public class MessageContext : DbContext
    {
        public MessageContext() : base("Chatbot")
        {

        }

        public DbSet<Message> Message { get; set; }

    }
}