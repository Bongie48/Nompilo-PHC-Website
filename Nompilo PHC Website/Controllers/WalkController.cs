using Microsoft.AspNetCore.Mvc;
using Nompilo_PHC_Website.Data;
using Nompilo_PHC_Website.Models;

namespace Nompilo_PHC_Website.Controllers
{
    public class WalkController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        public WalkController(ApplicationDbContext dbC)
        {
            dbContext = dbC;
        }
        //[ValidateAntiForgeryToken]
        [HttpGet]
        
        public IActionResult Index()
        {
            IEnumerable<Booking> book = dbContext.Bookings;
            return View(book);
        } 
        public IActionResult Info()
        {
            
            return View();
        } 
      

    }
}
