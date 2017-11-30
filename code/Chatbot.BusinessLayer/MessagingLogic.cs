using Chatbot.BusinessLayer.Interfaces;
using Chatbot.BusinessLayer.Models;
using Chatbot.Common.Interfaces;

namespace Chatbot.BusinessLayer
{
    public class MessagingLogic : IMessagingLogic
    {
        private readonly IPluginManager pluginManager;

        public MessagingLogic(IPluginManager pluginManager)
        {
            this.pluginManager = pluginManager;
        }

        public Message ProcessMessage(Message message)
        {
            IPlugin chosenPlugin = pluginManager.ChoosePlugin(message);
            return chosenPlugin.Handle(message);
        }
    }
}
