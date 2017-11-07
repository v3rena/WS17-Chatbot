using Chatbot.Interfaces.DTOs;

namespace Chatbot.DTOs
{
    public class Message : IMessage
    {
        public string Content { get; set; }
        public string Guid { get; set; }

        public Message()
        {

        }

        public Message(string content)
        {
            Content = content;
        }

        public Message(string content, string guid)
        {
            Content = content;
            Guid = guid;
        }
    }
}