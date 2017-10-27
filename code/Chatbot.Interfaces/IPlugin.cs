using Chatbot.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chatbot.Interfaces
{
    public interface IPlugin
    {
        string Name { get; }

        float CanHandle(IMessage message);

        IMessage Handle(IMessage message);
    }
}
