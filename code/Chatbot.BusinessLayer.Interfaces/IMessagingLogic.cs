using Chatbot.BusinessLayer.Models;

namespace Chatbot.BusinessLayer.Interfaces
{
    public interface IMessagingLogic
    {
        Message ProcessMessage(Message message);
    }
}