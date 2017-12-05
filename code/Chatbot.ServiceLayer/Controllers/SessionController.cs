using AutoMapper;
using Chatbot.BusinessLayer.Interfaces;
using Chatbot.ServiceLayer.DTOs;
using System.Web.Http;
using System.Web.Http.Description;

namespace Chatbot.Controllers
{
    [RoutePrefix("api/session")]
    public class SessionController : ApiController
    {
        private readonly ISessionLogic sessionLogic;
        private readonly IMapper mapper;

        public SessionController(ISessionLogic sessionLogic, IMapper mapper)
        {
            this.sessionLogic = sessionLogic;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        [ResponseType(typeof(SessionKey))]
        public SessionKey GetSessionKey()
        {
            return mapper.Map<SessionKey>(sessionLogic.GenerateSession());
        }
    }
}
