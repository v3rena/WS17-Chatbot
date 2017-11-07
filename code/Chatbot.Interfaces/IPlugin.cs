using Chatbot.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chatbot.Interfaces
{
    public interface IPlugin
    {
        string Name { get; }

        float CanHandle(Message message);

        Message Handle(Message message);
    }
}
