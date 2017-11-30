using Chatbot.Interfaces;
using Chatbot.Models;
using log4net;

namespace Chatbot.BusinessLayer
{
    public class BusinessLayer : IBusinessLayer
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(BusinessLayer));
        private readonly IPluginManager _pluginManager;
        //private readonly IDataAccessLayer _dal;

        public BusinessLayer(IDataAccessLayer dal, IPluginManager pluginManager)
        {
            //_dal = dal;
            _pluginManager = pluginManager;
        }

        public Message ProcessMessage(Message message)
        {
            IPlugin chosenPlugin = _pluginManager.ChoosePlugin(message);
            return chosenPlugin.Handle(message);
        }

        public SessionKey GenerateSession()
        {
            return new SessionKey();
        }

        public string GetSpeechAPIToken()
        {
            log.Debug("get speech api token");
            // TODO don't hardcode api key
            return (new SpeechAPIAuthentication("31993c62e9f146bbaec9f49bf2cdb0b3")).GetAccessToken();
        }
    }
}