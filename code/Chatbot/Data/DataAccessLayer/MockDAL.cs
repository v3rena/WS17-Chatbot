using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;

namespace Chatbot.DataAccessLayer
{
    public class MockDAL : IDataAccessLayer<Test>
    {
        public MockDAL()
        {
        }

        public string GetName()
        {
            return "MockDAL";
        }

        public string GetTest(int id)
        {
            throw new NotImplementedException();
        }

        public int Insert(Test item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Test> Select(Func<Test, bool> condition)
        {
            throw new NotImplementedException();
        }

        public Test SelectFirst(Func<Test, bool> condition)
        {
            throw new NotImplementedException();
        }

        public class Module : Autofac.Module
        {
            protected override void Load(ContainerBuilder builder)
            {
                base.Load(builder);
                builder.RegisterType<MockDAL>().As<IDataAccessLayer<Test>>().InstancePerRequest();
            }
        }
    }
}