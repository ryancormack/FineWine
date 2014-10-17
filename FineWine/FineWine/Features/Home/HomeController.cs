using System.Web.Mvc;
using FineWine.Domain.Repositories;

namespace FineWine.Features.Home
{
    public class HomeController : Controller
    {
        public WineRepository context = new WineRepository();

        public ActionResult Index()
        {
            return View("Index", context.GetAllWines());
        }
    }
}