namespace Chatbot.Services
{
    public interface IBusinessLayer
    {
        string GetName();

        string ProcessMessage(string message);
    }
}