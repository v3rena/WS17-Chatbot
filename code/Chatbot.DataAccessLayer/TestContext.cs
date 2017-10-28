using Chatbot.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Chatbot
{
    public class TestContext : DbContext
    {
        public TestContext() : base("Chatbot")
        {

        }

        public DbSet<Test> Test { get; set; }
    }
}