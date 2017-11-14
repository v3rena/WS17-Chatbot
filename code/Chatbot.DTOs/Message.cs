namespace Chatbot.DTOs
{
    public class Message
    {
        public string Content { get; set; }

        public Message()
        {
        }

        public Message(string content)
        {
            Content = content;
        }
    }
}