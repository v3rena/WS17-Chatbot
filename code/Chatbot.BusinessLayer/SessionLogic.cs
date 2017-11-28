using Chatbot.BusinessLayer.Interfaces;
using Chatbot.BusinessLayer.Models;

namespace Chatbot.BusinessLayer
{
    public class SessionLogic : ISessionLogic
    {
        public SessionKey GenerateSession()
        {
            return new SessionKey();
        }
    }
}
