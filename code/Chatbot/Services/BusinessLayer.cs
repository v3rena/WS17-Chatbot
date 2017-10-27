using Chatbot.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chatbot.Services
{
    public class BusinessLayer : IBusinessLayer
    {
        private IDataAccessLayer _dal;

        public BusinessLayer(IDataAccessLayer dal)
        {
            _dal = dal;
        }

        public string GetName()
        {
            return _dal.GetName();
        }
    }
}