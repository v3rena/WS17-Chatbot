using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chatbot.Plugins.EchoBot
{
    public class EchoBotTable : IEchoBotTable
    {
        public EchoBotTable()
        {

        }
        public int EchoBotTableID { get; set; }
        public string EchoBotName { get; set; }
    }
}