using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Chatbot
{
    public class TestInitializer : DropCreateDatabaseIfModelChanges<TestContext>
    {
        protected override void Seed(TestContext context)
        {
            GetTests().ForEach(c => context.Test.Add(c));
        }

        private static List<Test> GetTests()
        {
            var tests = new List<Test>
            {
                new Test
                {
                    TestName = "it worked"
                }
            };
            return tests;
        }

    }
}