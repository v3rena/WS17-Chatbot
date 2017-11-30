using AutoMapper;
using Chatbot.BusinessLayer.Interfaces;
using Chatbot.ServiceLayer.DTOs;
using System.Web.Http;
using System.Web.Http.Description;

namespace Chatbot.Controllers
{
    [RoutePrefix("api/message")]
    public class MessageController : ApiController
    {
        private readonly IBusinessLayer _bl;
        private readonly IMapper _mapper;

        public MessageController(IBusinessLayer bl, IMapper mapper)
        {
            _bl = bl;
            _mapper = mapper;
        }

        // POST api/message/
        // { "Content" : "" }
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(Message))]
        public Message PostMessage([FromBody]Message message)
        {
            //TODO check if message is valid (e.g. has SessionKey)
            return _mapper.Map<Message>(_bl.ProcessMessage(_mapper.Map<BusinessLayer.Models.Message>(message)));
        }
    }
}
