using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chatbot.Models
{
    public class Test : ITest
    {
        public Test()
        {

        }
        public int TestID { get; set; }
        public string TestName { get; set; }
    }
}