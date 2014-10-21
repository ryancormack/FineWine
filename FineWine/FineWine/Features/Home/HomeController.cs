using System.Web.Mvc;
using FineWine.Domain.Services;

namespace FineWine.Features.Home
{
    public class HomeController : Controller
    {
        private readonly IWineService _wineService;

        public HomeController(IWineService wineService)
        {
            _wineService = wineService;
        }


        public ActionResult Index()
        {
            var model = _wineService.GetLatestRioja();
            return View("Index", model);
        }
    }
}