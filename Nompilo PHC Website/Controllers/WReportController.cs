using Microsoft.AspNetCore.Mvc;
using Nompilo_PHC_Website.Data;
using Nompilo_PHC_Website.Models;

namespace Nompilo_PHC_Website.Controllers
{
    public class WReportController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        public WReportController(ApplicationDbContext dbC)
        {
            dbContext = dbC;
        }
        public IActionResult Index()
        {
            IEnumerable<Report_Wlk> gtrpt = dbContext.rprt;
            return View(gtrpt);
        }
    }
}
