using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nompilo_PHC_Website.Data;
using Nompilo_PHC_Website.Models;
using System.Threading.Tasks;

namespace Nompilo_PHC_Website.Controllers
{

    public class CheckupController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<DataGeeksUser> _userMananger;

        public CheckupController(ApplicationDbContext context, UserManager<DataGeeksUser> userManager)
        {
            _context = context;
            _userMananger = userManager;
        }

        // GET: Checkup
        public async Task<IActionResult> CheckUp(string sortOrder, string currentFilter, string searchString, int? page,int bookingid)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var UserId = _userMananger.GetUserId(User);
            var patient = _context.Users.Where(p => p.Id == UserId).FirstOrDefault();
            if (patient == null)
            {
                return View("NotLogin");
            }
            var bookings = from s in _context.Checkups.Where(p => p.BookingId == bookingid || p.PatientId== patient.Id) select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                bookings = bookings.Where(s => s.Results.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    bookings = bookings.OrderByDescending(s => s.Results);
                    break;
                case "Date":
                    bookings = bookings.OrderBy(s => s.CheckupDate);
                    break;
                case "date_desc":
                    bookings = bookings.OrderByDescending(s => s.CheckupDate);
                    break;
                default:  // Name ascending 
                    bookings = bookings.OrderBy(s => s.Results);
                    break;
            }

            int pageSize = 10;
            return View(await PaginatedList<Checkup>.CreateAsync(bookings.AsNoTracking(), page ?? 1, pageSize));
            
        }


        // GET: Checkup
        public async Task<IActionResult> CheckUpList(string sortOrder, string currentFilter, string searchString, int? page, int bookingid)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var UserId = _userMananger.GetUserId(User);
            var patient = _context.Users.Where(p => p.Id == UserId).FirstOrDefault();
            if (patient == null)
            {
                return View("NotLogin");
            }

            var bookings = from s in _context.Checkups.Where(p => p.DoctorId == patient.Id)
                           select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                bookings = bookings.Where(s => s.Results.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    bookings = bookings.OrderByDescending(s => s.Results);
                    break;
                case "Date":
                    bookings = bookings.OrderBy(s => s.CheckupDate);
                    break;
                case "date_desc":
                    bookings = bookings.OrderByDescending(s => s.CheckupDate);
                    break;
                default:  // Name ascending 
                    bookings = bookings.OrderBy(s => s.Results);
                    break;
            }

            int pageSize = 10;
            return View(await PaginatedList<Checkup>.CreateAsync(bookings.AsNoTracking(), page ?? 1, pageSize));

        }

        // GET: Checkup/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checkup = await _context.Checkups
                .FirstOrDefaultAsync(m => m.Id == id);

            if (checkup == null)
            {
                return NotFound();
            }

            return View(checkup);
        }
        // GET: Checkup/Create
        public IActionResult Create(int BookingId)
        {
            var booking = _context.Bookings.Find(BookingId);
            if (booking == null )
            {
                return NotFound();
            }
            var patient = _context.Users.SingleOrDefault(p => p.Id == booking.DataGeeksUserId);
            if (patient == null)
            {
                return NotFound(); // Handle the case when the patient is not found
            }

            ViewBag.BookingId = BookingId;
            ViewBag.PatientId = patient.Id;

            return View();
        }



        // POST: Checkup/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Checkup checkup)
        {
            
                var UserId = _userMananger.GetUserId(User);
                var patient = _context.Users.Where(p => p.Id == UserId).FirstOrDefault();
                if (patient == null )
                {
                    return NotFound();
                }
                
                checkup.CheckupDate = DateTime.Now;
                checkup.DoctorId = patient.Id;
                _context.Add(checkup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(CheckUpList));
            
        }

        // GET: Checkup/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checkup = await _context.Checkups.FindAsync(id);

            if (checkup == null)
            {
                return NotFound();
            }

            return View(checkup);
        }

        // POST: Checkup/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CheckupDate,Results,Notes")] Checkup checkup)
        {
            if (id != checkup.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(checkup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CheckupExists(checkup.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index)) ;
            }

            return View(checkup);
        }

        // GET: Checkup/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checkup = await _context.Checkups
                .FirstOrDefaultAsync(m => m.Id == id);

            if (checkup == null)
            {
                return NotFound();
            }

            return View(checkup);
        }

        // POST: Checkup/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var checkup = await _context.Checkups.FindAsync(id);

            if (checkup == null)
            {
                return NotFound();
            }

            _context.Checkups.Remove(checkup);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CheckupExists(int id)
        {
            return _context.Checkups.Any(e => e.Id == id);
        }

        public IActionResult ViewByMonth(int prenatalMonth)
        {
            switch (prenatalMonth)
            {
                case 1:
                    return RedirectToAction("Month1");
                case 2:
                    return RedirectToAction("Month2");
                case 3:
                    return RedirectToAction("Month3");
                case 4:
                    return RedirectToAction("Month4");
                case 5:
                    return RedirectToAction("Month5");
                case 6:
                    return RedirectToAction("Month6");
                case 7:
                    return RedirectToAction("Month7");
                case 8:
                    return RedirectToAction("Month8");
                case 9:
                    return RedirectToAction("Month9");
                default:
                    return RedirectToAction("Index"); // Redirect to the Index view by default
            }
        }
        public IActionResult Month1()
        {
            // Handle logic for the 1st prenatal month
            return View();
        }

        public IActionResult Month2()
        {
            // Handle logic for the 2nd prenatal month
            return View();
        }

        public IActionResult Month3()
        {
            // Handle logic for the 3rd prenatal month
            return View();
        }

        public IActionResult Month4()
        {
            // Handle logic for the 4th prenatal month
            return View();
        }

        public IActionResult Month5()
        {
            // Handle logic for the 5th prenatal month
            return View();
        }

        public IActionResult Month6()
        {
            // Handle logic for the 6th prenatal month
            return View();
        }

        public IActionResult Month7()
        {
            // Handle logic for the 7th prenatal month
            return View();
        }

        public IActionResult Month8()
        {
            // Handle logic for the 8th prenatal month
            return View();
        }

        public IActionResult Month9()
        {
            // Handle logic for the 9th prenatal month
            return View();
        }
        public IActionResult Month10()
        {
            // Handle logic for the 9th prenatal month
            return View();
        }
    }

}
