using Chatbot.BusinessLayer.Models;

namespace Chatbot.BusinessLayer.Interfaces
{
    public interface ISessionLogic
    {
        SessionKey GenerateSession();
    }
}