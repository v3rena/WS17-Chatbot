using Chatbot.Bot;
using Chatbot.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chatbot.Services
{
    public class BusinessLayer : IBusinessLayer
    {
        private readonly IBot _bot;
        private readonly IDataAccessLayer _dal;

        public BusinessLayer(IBot bot, IDataAccessLayer dal)
        {
            _bot = bot;
            _dal = dal;
        }

        public string GetName()
        {
            return _dal.GetName();
        }

        public string ProcessMessage(string message)
        {
            return _bot.ProcessMessage(message);
        }
    }
}