using Chatbot.BusinessLayer.Interfaces;
using System.Web.Http;
using System.Web.Http.Description;
using log4net;

namespace Chatbot.Controllers
{
    [RoutePrefix("api/token")]
    public class SpeechAPITokenController : ApiController
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(SpeechAPITokenController));
        private readonly ISpeechAPITokenLogic speechAPITokenLogic;

        public SpeechAPITokenController(ISpeechAPITokenLogic speechAPITokenLogic)
        {
            this.speechAPITokenLogic = speechAPITokenLogic;
        }

        [HttpGet]
        [Route("")]
        [ResponseType(typeof(string))]
        public string GetSpeechAPIToken()
        {
            log.Debug("getting speech api token");
            return speechAPITokenLogic.GetSpeechAPIToken();
        }
    }
}
