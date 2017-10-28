using Autofac;
using Chatbot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Migrations;


namespace Chatbot.DataAccessLayer
{
    public class DAL : IDataAccessLayer<Test>
    {
        public DAL()
        {
            Database.SetInitializer<TestContext>(new MigrateDatabaseToLatestVersion<TestContext, Migrations.Configuration>());
        }

        public string GetName()
        {
            return "DataAccessLayer";
        }

        public string GetTest(int id)
        {
            Test result;
            using (var context = new TestContext())
            {
                result = context.Test.FirstOrDefault();
            }
            return result.TestName;
        }

        public int Insert(Test item)
        {
            using (var context = new TestContext())
            {
                context.Test.Add(item);
                return context.SaveChanges();
            }
        }

        public IEnumerable<Test> Select(Func<Test, bool> condition)
        {
            IEnumerable<Test> result = null;
            using (var context = new TestContext())
            {
                result = context
                    .Test
                    .Where(condition)
                    .ToList();
            }
            return result;
        }

        public Test SelectFirst(Func<Test, bool> condition)
        {
            return Select(condition).FirstOrDefault();
        }

        public class Module : Autofac.Module
        {
            protected override void Load(ContainerBuilder builder)
            {
                //base.Load(builder);
                builder
                    .Register(component => new DAL())
                    .As<IDataAccessLayer<Test>>()
                    .InstancePerLifetimeScope();

                /*
                 builder.Register(c => new ValuesService(c.Resolve<ILogger<ValuesService>>()))
                        .As<IValuesService>()
                    .InstancePerLifetimeScope();
                 */
            }
        }

    }
}