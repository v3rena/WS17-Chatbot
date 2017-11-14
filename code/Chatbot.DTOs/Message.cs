namespace Chatbot.DTOs
{
    public class Message
    {
        public string Content { get; set; }
        public SessionKey SessionKey { get; set; }

        public Message()
        {
        }

        public Message(string content)
        {
            Content = content;
        }
        public Message(string content, SessionKey sessionKey)
        {
            Content = content;
            SessionKey = sessionKey;
        }
    }
}