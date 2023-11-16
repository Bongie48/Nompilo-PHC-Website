using Microsoft.AspNetCore.Mvc;

namespace Nompilo_PHC_Website.Controllers
{
    public class LabController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ScienceDash()
        {
            return View();
        }
    }
}
