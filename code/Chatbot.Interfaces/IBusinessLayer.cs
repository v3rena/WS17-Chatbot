using Chatbot.Interfaces.Models;

namespace Chatbot.Interfaces
{
    public interface IBusinessLayer
    {
        IMessage ProcessMessage(IMessage message);
    }
}