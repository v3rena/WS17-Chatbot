using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chatbot.BusinessLayer.Interfaces;

namespace Chatbot.BusinessLayer
{
    public class SpeechAPITokenLogic : ISpeechAPITokenLogic
    {
        public string GetSpeechAPIToken()
        {
            return (new SpeechAPIAuthentication("31993c62e9f146bbaec9f49bf2cdb0b3")).GetAccessToken();
        }
    }
}
