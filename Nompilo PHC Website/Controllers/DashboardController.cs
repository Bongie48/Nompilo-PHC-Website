using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Nompilo_PHC_Website.Controllers
{
    public class DashboardController : Controller
    {
        
        public IActionResult Patient()
        {
            return View();
        }

        public IActionResult Doctor()
        {
            return View();
        }
        public IActionResult LabScientist()
        {
            return View();
        }
        public IActionResult Nurse()
        {
            return View();
        }
        
        public IActionResult Admin()
        {
            return View();
        }
    }
}
