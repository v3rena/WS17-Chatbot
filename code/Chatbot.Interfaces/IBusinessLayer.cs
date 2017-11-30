using Chatbot.Models;

namespace Chatbot.Interfaces
{
    public interface IBusinessLayer
    {
        Message ProcessMessage(Message message);

        SessionKey GenerateSession();

        string GetSpeechAPIToken();
    }
}