namespace Chatbot.DTOs
{
    public class Message
    {
        public string Content { get; set; }
<<<<<<< HEAD
        public string Guid { get; set; }
=======
        public string SessionKey { get; set; }
>>>>>>> develop

        public Message()
        {
        }


        public Message(string content)
        {
            Content = content;
        }

<<<<<<< HEAD
        public Message(string content, string guid)
        {
            Content = content;
            Guid = guid;
=======
        public Message(string content, string sessionKey)
        {
            Content = content;
            SessionKey = sessionKey;
>>>>>>> develop
        }
    }
}