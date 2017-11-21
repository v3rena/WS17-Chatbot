﻿using Chatbot.Models;
using System.Collections.Generic;

namespace Chatbot.Interfaces
{
    public interface IPluginManager
    {
        IEnumerable<IPlugin> Plugins { get; }

        void Add(IPlugin plugin);

        void Add(string plugin);

        void Clear();

        IPlugin ChoosePlugin(Message message);
    }
}