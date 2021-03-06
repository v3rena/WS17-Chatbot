﻿using Chatbot.DataAccessLayer.Interfaces;

namespace Chatbot.DataAccessLayer.Entities
{
    public class Message : IEntity
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public SessionKey SessionKey { get; set; }
        public bool IsUserMessage { get; set; } // Zur Unterscheidung, ob die Nachricht vom User kommt oder nicht

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
