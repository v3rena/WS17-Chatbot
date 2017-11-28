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
        private readonly IBusinessLayer _bl;
        private readonly IMapper _mapper;

        public SessionController(IBusinessLayer bl, IMapper mapper)
        {
            _bl = bl;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        [ResponseType(typeof(SessionKey))]
        public SessionKey GetSessionKey()
        {
            return _mapper.Map<SessionKey>(_bl.GenerateSession());
        }
    }
}
