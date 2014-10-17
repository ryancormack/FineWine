using System.Web.Mvc;

namespace FineWine.Features.Home
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View("Index");
        }
    }
}