using Chatbot.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chatbot.Services
{
    public class BusinessLayer<T> : IBusinessLayer<T>
    {
        private IDataAccessLayer<T> _dal;

        public BusinessLayer(IDataAccessLayer<T> dal)
        {
            _dal = dal;
        }

        public string GetName()
        {
            return _dal.GetName();
        }

        public string GetTest(int id)
        {
            return _dal.GetTest(id);
        }

        public T SelectFirst(Func<T, bool> condition)
        {
            return _dal.SelectFirst(condition);
        }
    }
}