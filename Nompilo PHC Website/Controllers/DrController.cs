using Microsoft.AspNetCore.Mvc;
using Nompilo_PHC_Website.Data;
using Nompilo_PHC_Website.Models;

namespace Nompilo_PHC_Website.Controllers
{
    public class DrController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        public DrController(ApplicationDbContext dbC)
        {
            dbContext = dbC;
        }
        public IActionResult Doctors()
        {
            IEnumerable<Doctors> GetDrs = dbContext.doctors;
            return View(GetDrs);
        }
    }
}
