using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;

namespace Chatbot.DataAccessLayer
{
    public class MockDAL : IDataAccessLayer
    {
        public MockDAL()
        {
        }

        public string GetName()
        {
            return "MockDAL";
        }

        public class Module : Autofac.Module
        {
            protected override void Load(ContainerBuilder builder)
            {
                base.Load(builder);
                builder.RegisterType<MockDAL>().As<IDataAccessLayer>().InstancePerRequest();
            }
        }
    }
}