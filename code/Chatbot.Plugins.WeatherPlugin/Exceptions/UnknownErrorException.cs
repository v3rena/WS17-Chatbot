using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatbot.Plugins.WeatherPlugin.Exceptions
{
    class UnknownErrorException : Exception
    {
        public int ErrorCode { get; }

        public UnknownErrorException(int errorCode, string message) : base (message)
        {
            ErrorCode = errorCode;
        }
    }
}
