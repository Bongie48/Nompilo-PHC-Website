using iText.Commons.Actions.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using Nompilo_PHC_Website.Data;
using Nompilo_PHC_Website.Models;

namespace Nompilo_PHC_Website.Controllers
{
    public class BookingController : Controller
    {
        private readonly ApplicationDbContext _Booking;
        private readonly UserManager<DataGeeksUser> _userMananger;
        private readonly IConfiguration _Configuration;

        public BookingController(ApplicationDbContext booking, UserManager<DataGeeksUser> userManager)
        {
            _Booking = booking;
            _userMananger = userManager;
        }

        public IActionResult Booking()
        {
            var Bokking = new Booking();
            return View(Bokking);
        }
        public IActionResult Dashboard()
        {
            IEnumerable<Booking> bookings = _Booking.Bookings;
            return View(bookings);
        }
        [HttpPost]
        public IActionResult Book(Booking book)
        {

            if (ModelState.IsValid)
            {
                var UserId = _userMananger.GetUserId(User);
                var patient = _Booking.Users.Where(p=> p.Id == UserId).FirstOrDefault();
                if (patient == null)
                {
                    return View("NotLogin");
                }
                book.DataGeeksUserId = patient.Id;
                _Booking.Bookings.Add(book);
                _Booking.SaveChanges();
                return RedirectToAction("ViewApp");
            }
            return RedirectToAction("Booking");
        }

        public IActionResult ViewApp()
        {
            IEnumerable<Booking> bookings = _Booking.Bookings;
            return View(bookings);
        }
        public IActionResult Update(int? ID)
        {
            if (ID == null || ID == 0)
            {
                return NotFound();
            }
            var obj = _Booking.Bookings.Find(ID);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateSub(Booking book)
        {
            _Booking.Bookings.Update(book);
            _Booking.SaveChanges();
            return RedirectToAction("ViewApp");
        }

        public IActionResult Delete(int? ID)
        {
            var obj = _Booking.Bookings.Find(ID);
            if (obj == null)
            {
                return NotFound();
            }
            _Booking.Bookings.Remove(obj);
            _Booking.SaveChanges();
            Thread.Sleep(10000);
            return RedirectToAction("ViewApp");
        }
        // GET: Booking
        public async Task<IActionResult> ViewBookings(string sortOrder, string currentFilter, string searchString, int? page)
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
            var patient = _Booking.Users.Where(p => p.Id == UserId).FirstOrDefault();
            if (patient == null)
            {
                return View("NotLogin");
            }
            var bookings = from s in  _Booking.Bookings.Where(p=> p.DataGeeksUserId == patient.Id) select s;
           
            if (!String.IsNullOrEmpty(searchString))
            {
                bookings = bookings.Where(s => s.services.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    bookings = bookings.OrderByDescending(s => s.services);
                    break;
                case "Date":
                    bookings = bookings.OrderBy(s => s.datetimes);
                    break;
                case "date_desc":
                    bookings = bookings.OrderByDescending(s => s.datetimes);
                    break;
                default:  // Name ascending 
                    bookings = bookings.OrderBy(s => s.services);
                    break;
            }

            int pageSize = 10;
            return View(await PaginatedList<Booking>.CreateAsync(bookings.AsNoTracking(), page ?? 1, pageSize));
           
        }
        // CheckupController.cs

        public IActionResult CheckupDetailsForBooking(int Id)
        {
            var checkup = _Booking.Checkups
                .Where(c => c.BookingId == Id)
                .OrderByDescending(c => c.CheckupDate)
                .FirstOrDefault();

            if (checkup == null)
            {
                return View("ViewBookings");
            }

            return View(checkup);
        }


        // GET: Booking
        public async Task<IActionResult> ViewActiveBookings(string sortOrder, string currentFilter, string searchString, int? page)
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
            
            var bookings = from s in _Booking.Bookings.Where(p => p.DataGeeksUserId != null && p.services == "Pre-NatalCare") select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                bookings = bookings.Where(s => s.services.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    bookings = bookings.OrderByDescending(s => s.services);
                    break;
                case "Date":
                    bookings = bookings.OrderBy(s => s.datetimes);
                    break;
                case "date_desc":
                    bookings = bookings.OrderByDescending(s => s.datetimes);
                    break;
                default:  // Name ascending 
                    bookings = bookings.OrderBy(s => s.services);
                    break;
            }

            int pageSize = 10;
            return View(await PaginatedList<Booking>.CreateAsync(bookings.AsNoTracking(), page ?? 1, pageSize));

        }
    }
}
