using AutoMapper;
using Chatbot.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chatbot.BusinessLayer
{
    public class BusinessLayer : IBusinessLayer
    {
        private readonly IPluginManager _pluginManager;
        private readonly IDataAccessLayer _dal;
        private readonly IMapper _mapper;

        public BusinessLayer(IDataAccessLayer dal, IPluginManager pluginManager, IMapper mapper)
        {
            _dal = dal;
            _pluginManager = pluginManager;
            _mapper = mapper;
        }

        public Interfaces.DTOs.IMessage ProcessMessage(Interfaces.DTOs.IMessage message)
        {
            Interfaces.Models.IMessage messageModel = _mapper.Map<Interfaces.Models.IMessage>(message);
            IPlugin chosenPlugin = _pluginManager.ChoosePlugin(messageModel);
            return _mapper.Map<Interfaces.DTOs.IMessage>(chosenPlugin.Handle(messageModel));
        }
    }
}