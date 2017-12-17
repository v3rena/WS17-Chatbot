using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatbot.Plugins.WeatherPlugin.Exceptions
{
    class APIErrorException : Exception
    {
        public int ErrorCode { get; }

        public APIErrorException(int errorCode, string message) : base (message)
        {
            ErrorCode = errorCode;
        }
    }
}
