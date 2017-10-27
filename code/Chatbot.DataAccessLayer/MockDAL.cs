using Autofac;
using Chatbot.Interfaces;

namespace Chatbot.DataAccessLayer
{
    public class MockDAL : IDataAccessLayer
    {
        public string Name => "MockDAL";

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