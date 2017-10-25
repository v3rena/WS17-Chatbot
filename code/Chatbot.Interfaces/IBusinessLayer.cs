using Chatbot.Interfaces.DTOs;

namespace Chatbot.Interfaces
{
    public interface IBusinessLayer
    {
        IMessage ProcessMessage(IMessage message);
    }
}