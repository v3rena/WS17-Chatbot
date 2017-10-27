﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Chatbot.Interfaces;
using Chatbot.Interfaces.Models;
using System.Web;

namespace Chatbot.PluginManager
{
    public class PluginManager : IPluginManager
    {
        private List<IPlugin> _plugins;

        public PluginManager()
        {
            Initialize();
        }

        public IEnumerable<IPlugin> Plugins => _plugins;

        public void Add(string plugin)
        {
            Type t = Type.GetType(plugin);
            if (t != null)
            {
                if (t.GetInterfaces().Contains(typeof(IPlugin)))
                {
                    Add((IPlugin)Activator.CreateInstance(t));
                }
                else
                {
                    throw new ArgumentException("Invalid Plugin type.");
                }
            }
            else
            {
                throw new ArgumentException("Plugin not found.");
            }
        }

        public void Add(IPlugin plugin)
        {
            if (!_plugins.Contains(plugin))
            {
                _plugins.Add(plugin);
            }
        }

        public void Clear()
        {
            _plugins.Clear();
        }

        public IPlugin ChoosePlugin(IMessage message)
        {
            return _plugins.OrderByDescending(p => p.CanHandle(message)).First();
        }

        private void Initialize()
        {
            _plugins = new List<IPlugin>();

#if DEBUG
            string path = HttpContext.Current.Server.MapPath("~") + @"\bin\debug\plugins";
#else
            string path = HttpContext.Current.Server.MapPath("~") + @"\bin\release\plugins";
#endif
            foreach (string file in Directory.GetFiles(path))
            {
                if (Path.GetExtension(file) == ".dll")
                {
                    Assembly dll = Assembly.LoadFile(Path.Combine(path, file));
                    foreach (Type type in dll.GetExportedTypes())
                    {
                        if (type.GetInterface("IPlugin") == typeof(IPlugin))
                        {
                            Add((IPlugin)Activator.CreateInstance(type));
                        }
                    }
                }
            }
        }
    }
}
