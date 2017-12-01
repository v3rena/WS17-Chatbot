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
        private readonly IMessagingLogic messagingLogic;
        private readonly IMapper mapper;

        public MessageController(IMessagingLogic messagingLogic, IMapper mapper)
        {
            this.messagingLogic = messagingLogic;
            this.mapper = mapper;
        }

        // POST api/message/
        // { "Content" : "" }
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(Message))]
        public Message PostMessage([FromBody]Message message)
        {
            //TODO check if message is valid (e.g. has SessionKey)
            return mapper.Map<Message>(messagingLogic.ProcessMessage(mapper.Map<BusinessLayer.Models.Message>(message)));
        }
    }
}
