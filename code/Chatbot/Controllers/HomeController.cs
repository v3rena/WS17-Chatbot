using Chatbot.Interfaces;
using System.Web;
using System.Web.Mvc;

namespace Chatbot.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {

        }

        public ActionResult Index()
        {
#if STAGING
            System.Web.Optimization.Scripts.DefaultTagFormat = "<script src=\".{0}\"></script>";
            System.Web.Optimization.Styles.DefaultTagFormat = "<link href=\".{0}\" rel=\"stylesheet\"/>";

            ViewBag.BasePath = "/staging/";
#else
            ViewBag.BasePath = "/";
#endif

            return View();
        }
    }
}
