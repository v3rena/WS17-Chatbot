using Chatbot.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Chatbot.Data.Setup
{
    public class ContextInitializer
    {
        public ContextInitializer()
        {
            Database.SetInitializer<TestContext>(new MigrateDatabaseToLatestVersion<TestContext, Migrations.Configuration>());
        }

    }
}