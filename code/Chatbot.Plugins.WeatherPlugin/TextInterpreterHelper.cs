using Chatbot.BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatbot.Plugins.WeatherPlugin
{
    static class TextInterpreterHelper
    {
        static public string HelperMethodReadCity(Message message)
        {
            var split = message.Content.Split(null); //splits by whitespace
            for (int i = 0; i < split.Count(); i++)
            {
                if (split[i] == "in" && i + 1 < split.Count())
                {
                    if (split[i + 1].StartsWith("'"))
                    {
                        StringBuilder stringBuilder = new StringBuilder();
                        for (int j = i + 1; j < split.Count(); j++)
                        {
                            stringBuilder.AppendFormat(split[j]);
                            if (split[j].EndsWith("'"))
                            {
                                return stringBuilder.ToString(1, stringBuilder.Length - 2);
                            }
                            stringBuilder.AppendFormat(" ");
                        }
                    }
                    return split[i + 1];
                }
            }
            return null;
        }
    }
}
