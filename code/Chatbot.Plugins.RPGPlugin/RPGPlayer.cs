using Chatbot.Plugins.RPGPlugin.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatbot.Plugins.RPGPlugin
{
    public class RPGPlayer
    {
        public IList<IRPGItem> inventory = new List<IRPGItem>();

        public string PlayerInventoryInfo()
        {
            string result = "";
            if (inventory.Count == 0)
            {
                result += "Ihr tragt keine Gegenstände";
            }
            else if (inventory.Count == 1)
            {
                result += "Ihr habt 1 " + inventory[0].GetName();
            }
            else
            {
                result += "Ihr tragt " + inventory.Count + " Gegenstände: ";
                foreach (IRPGItem item in inventory)
                {
                    result += "<br/>" + item.GetName();
                }
            }
            return result;
        }
    }
}