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

        /// <summary>
        /// Presents the plugin the current configuration at startup and offers it the possibility to add its default configuration if not set yet.
        /// </summary>
        /// <param name="configuration">The current configuration.</param>
        /// <returns>The updated configuration.</returns>
        Dictionary<string, string> EnsureDefaultConfiguration(Dictionary<string, string> configuration);
    }
}
