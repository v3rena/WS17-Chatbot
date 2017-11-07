using AutoMapper;
using Chatbot.Interfaces;
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
        [ResponseType(typeof(DTOs.SessionKey))]
        public DTOs.SessionKey GetSessionKey()
        {
            return _mapper.Map<DTOs.SessionKey>(_bl.GenerateSession());
        }
    }
}
