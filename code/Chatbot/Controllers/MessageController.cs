using AutoMapper;
using Chatbot.Interfaces;
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
        [ResponseType(typeof(Interfaces.DTOs.IMessage))]
        public Interfaces.DTOs.IMessage PostMessage([FromBody]DTOs.Message message)
        {
            return _mapper.Map<Interfaces.DTOs.IMessage>(_bl.ProcessMessage(_mapper.Map<Interfaces.Models.IMessage>(message)));
        }
    }
}
