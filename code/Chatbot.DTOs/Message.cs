using Chatbot.Interfaces.DTOs;

namespace Chatbot.DTOs
{
    public class Message : IMessage
    {
        public string Content { get; set; }

        public Message(string content)
        {
            Content = content;
        }
    }
}