using Chatbot.BusinessLayer.Models;
using Chatbot.Common.Interfaces;
using Chatbot.DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Chatbot.Common.PluginManager
{
    public class PluginManager : IPluginManager
    {
        private List<IPlugin> plugins;
        private readonly IRepository<DataAccessLayer.Entities.PluginConfiguration> pluginConfigurationRepository;

        public PluginManager(IRepository<DataAccessLayer.Entities.PluginConfiguration> pluginConfigurationRepository)
        {
            this.pluginConfigurationRepository = pluginConfigurationRepository;
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

        public void NotifyChanges(IPlugin plugin)
        {
            plugin.RefreshConfiguration(pluginConfigurationRepository.Read(i => i.Name == plugin.Name).ToDictionary(i => i.Key, i => i.Value));
        }

        public void NotifyChanges(string pluginName)
        {
            plugins
                .Where(i => i.Name == pluginName)
                .Single()
                .RefreshConfiguration(pluginConfigurationRepository.Read(i => i.Name == pluginName).ToDictionary(i => i.Key, i => i.Value));
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
                            var oldConfig = pluginConfigurationRepository.Read(i => i.Name == p.Name).ToDictionary(i => i.Key, i => i.Value);
                            var newConfig = p.EnsureDefaultConfiguration(oldConfig);
                            var listConfig = newConfig.Select(i => new DataAccessLayer.Entities.PluginConfiguration() { Name = p.Name, Key = i.Key, Value = i.Value });
                            pluginConfigurationRepository.Delete(i => i.Name == p.Name);
                            //TODO do that at once (no new db transaction for every element) 
                            foreach (DataAccessLayer.Entities.PluginConfiguration pluginConfiguration in listConfig)
                            {
                                pluginConfigurationRepository.Save(pluginConfiguration);
                            }
                            Add(p);
                        }
                    }
                }
            }
        }
    }
}
