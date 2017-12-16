using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatbot.Plugins.WeatherPlugin.Exceptions
{
    class CityNotFoundException : Exception
    {
        public int ErrorCode { get; }

        public CityNotFoundException(int errorCode, string message) : base (message)
        {
            ErrorCode = errorCode;
        }
    }
}
