using Chatbot.Interfaces;
using Chatbot.Interfaces.Models;

namespace Chatbot.BusinessLayer
{
    public class BusinessLayer : IBusinessLayer
    {
        private readonly IPluginManager _pluginManager;
        private readonly IDataAccessLayer _dal;

        public BusinessLayer(IDataAccessLayer dal, IPluginManager pluginManager)
        {
            _dal = dal;
            _pluginManager = pluginManager;
        }

        public IMessage ProcessMessage(IMessage message)
        {
            IPlugin chosenPlugin = _pluginManager.ChoosePlugin(message);
            return chosenPlugin.Handle(message);
        }
    }
}