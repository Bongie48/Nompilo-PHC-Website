using Microsoft.AspNetCore.Mvc;

namespace Nompilo_PHC_Website.Controllers
{
    public class InformationController : Controller
    {
        public IActionResult BloodSample()
        {
            return View();
        }

        public IActionResult UrineSample()
        {
            return View();
        }
        public IActionResult BodySample()
        {
            return View();
        }
        public IActionResult StoolSample()
        {
            return View();
        }

        public IActionResult PatientRequest()
        {
            return View();
        }

        public IActionResult Outbreak()
        {
            return View();
        }

        public IActionResult AuthorityRequests()
        {
            return View();
        }
    }
}
