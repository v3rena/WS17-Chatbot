using Chatbot.BusinessLayer.Models;
using Chatbot.Common.Interfaces;
using Chatbot.DataAccessLayer.Interfaces;
using Chatbot.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using Chatbot.BusinessLayer;
using Chatbot.BusinessLayer.Interfaces;

namespace Chatbot.Common.PluginManager
{
    public class PluginManager : IPluginManager
    {
        private List<IPlugin> plugins;
        private IDataAccessLayer dataAccessLayer;
        private readonly IPluginConfigurationLogic pluginConfigurationLogic;

        public PluginManager(IDataAccessLayer dataAccessLayer, IPluginConfigurationLogic pluginConfigurationLogic)
        {
            this.dataAccessLayer = dataAccessLayer;
            this.pluginConfigurationLogic = pluginConfigurationLogic;
            LoadAllPlugins();
        }

        public IEnumerable<IPlugin> Plugins => plugins;

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
            if (!plugins.Contains(plugin))
            {
                plugins.Add(plugin);
            }
        }

        public void Clear()
        {
            plugins.Clear();
        }

        public IPlugin ChoosePlugin(Message message)
        {
            return plugins.OrderByDescending(p => p.CanHandle(message)).First();
        }

        private void LoadAllPlugins()
        {
            plugins = new List<IPlugin>();

#if DEBUG
            string path = HttpContext.Current.Server.MapPath("~") + @"bin\debug\plugins";
#else
            string path = HttpContext.Current.Server.MapPath("~") + @"bin\release\plugins";
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
                            IPlugin p = (IPlugin)Activator.CreateInstance(type);
                            pluginConfigurationLogic.SavePluginConfigurations(p.EnsureDefaultConfiguration(pluginConfigurationLogic.GetPluginConfigurations(p)));
                            Add(p);
                        }
                    }
                }
            }
        }
    }
}
