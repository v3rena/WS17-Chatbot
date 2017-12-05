using Chatbot.Plugins.WeatherPlugin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatbot.Plugins.WeatherPlugin.Interfaces
{
    interface ICommand
    {
        string GetInformation(WeatherInformation weatherInformation);
    }
}
