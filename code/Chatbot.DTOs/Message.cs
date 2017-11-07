namespace Chatbot.DTOs
{
    public class Message
    {
        public string Content { get; set; }
        public string SessionKey { get; set; }

        public Message()
        {
        }


        public Message(string content)
        {
            Content = content;
        }

        public Message(string content, string sessionKey)
        {
            Content = content;
            SessionKey = sessionKey;
        }
    }
}