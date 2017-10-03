using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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


    }
}