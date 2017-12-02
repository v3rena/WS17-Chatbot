﻿using Chatbot.BusinessLayer.Models;
using System.Collections.Generic;

namespace Chatbot.Common.Interfaces
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
        IEnumerable<PluginConfiguration> EnsureDefaultConfiguration(IList<PluginConfiguration> configuration);
    }
}
