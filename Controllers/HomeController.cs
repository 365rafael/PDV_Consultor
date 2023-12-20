using Microsoft.AspNetCore.Mvc;

namespace PDV_Consultor.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
