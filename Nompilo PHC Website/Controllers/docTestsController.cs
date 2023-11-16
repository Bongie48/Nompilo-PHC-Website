using iText.Commons.Actions.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nompilo_PHC_Website.Data;
//using Nompilo_PHC_Website.Migrations;
using Nompilo_PHC_Website.Models;
using System.Linq;

namespace Nompilo_PHC_Website.Controllers
{
    public class docTestsController : Controller
    {
        private readonly ApplicationDbContext _Test;
        private readonly ApplicationDbContext _Results;

        private readonly UserManager<DataGeeksUser> _userMananger;

        public docTestsController(ApplicationDbContext docTests, UserManager<DataGeeksUser> userManager)
        {
            _Test = docTests;
            _Results = docTests;
            _userMananger = userManager;
        }

        [HttpGet]
        public IActionResult Test(int BookingId)
        {
            var UserId = _userMananger.GetUserId(User);
            var doctor = _Test.Users.Where(d => d.Id == UserId).FirstOrDefault();
            if (doctor == null)
            {
                return NotFound();
            }
            var booking = _Test.Bookings.Find(BookingId);
            if (booking == null)
            {
                return NotFound();
            }
            var patient = _Test.Users.Where(p => p.Id == booking.DataGeeksUserId).FirstOrDefault();
            if (patient == null)
            {
                return NotFound();
            }

            ViewBag.DoctorId = doctor.Id;
            ViewBag.PatientId = patient.Id;
            ViewBag.FullName = patient.FullName;
            ViewBag.Name = patient.Name;
            ViewBag.SurName = patient.Surname;
            ViewBag.Email = doctor.Email;
            return View();
        }

        [HttpPost]
        public IActionResult insertTest(Test test)
        {
            
            test.DateCreated = DateTime.Now;
            test.testStatus = "Not-Tested";
            _Test.Tests.Add(test);
            _Test.SaveChanges();
            return RedirectToAction("viewTests");
        }
   
        public IActionResult viewTests()
        {
            IEnumerable<TestResult> tests = _Test.TestResults;
            return View(tests);
        }


        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateLabTest(Test labTest, int BookingId)
        {
            var booking = _Test.Bookings.Find(BookingId);
            if (booking == null)
            {
                return NotFound();
            }
            var patient = _Test.Users.Where(p => p.Id == booking.DataGeeksUserId).FirstOrDefault();
            if (patient == null)
            {
                return NotFound();

            }
            var UserId = _userMananger.GetUserId(User);
            var doctor = _Test.Users.Where(d => d.Id == UserId).FirstOrDefault();
            if(doctor == null)
            { 
                return NotFound(); 
            }

            labTest.PatientId = patient.Id;
            labTest.DoctorId = doctor.Id;
            labTest.DateCreated = DateTime.Now;
            labTest.email = doctor.Email;
            labTest.pName = patient.Name;
            labTest.pSurname = patient.Surname;
            labTest.testfor = patient.FullName ;
            labTest.testStatus = "Not-Tested";
            _Test.Add(labTest);
            await _Test.SaveChangesAsync();

            return RedirectToAction("TestList");

        }
      

        // Action to display a list of lab tests
        public async Task<IActionResult> PLabTestList(string sortOrder, string currentFilter, string searchString, int? page, int bookingid)
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
            var patient = _Test.Users.Where(p => p.Id == UserId).FirstOrDefault();
            if (patient == null)
            {
                return View("NotLogin");
            }

            var bookings = from s in _Test.Tests.Where(p => p.PatientId == patient.Id)
                           select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                bookings = bookings.Where(s => s.testfor.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    bookings = bookings.OrderByDescending(s => s.TestMethods);
                    break;
                case "Date":
                    bookings = bookings.OrderBy(s => s.DateCreated);
                    break;
                case "date_desc":
                    bookings = bookings.OrderByDescending(s => s.DateCreated);
                    break;
                default:  // Name ascending 
                    bookings = bookings.OrderBy(s => s.TestMethods);
                    break;
            }

            int pageSize = 10;
            return View(await PaginatedList<Test>.CreateAsync(bookings.AsNoTracking(), page ?? 1, pageSize));
            
        }
        // Action to display a list of lab tests
        public async Task<IActionResult> DLabTestList(string sortOrder, string currentFilter, string searchString, int? page, int bookingid)
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
            var doctor = _Test.Users.Where(p => p.Id == UserId).FirstOrDefault();
            if (doctor == null)
            {
                return View("NotLogin");
            }


            var bookings = from s in _Test.Tests.Where(p => p.DoctorId == doctor.Id)
                           select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                bookings = bookings.Where(s => s.testfor.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    bookings = bookings.OrderByDescending(s => s.TestMethods.Select(tm => tm.methodId).FirstOrDefault());
                    break;
                case "Date":
                    bookings = bookings.OrderBy(s => s.DateCreated);
                    break;
                case "date_desc":
                    bookings = bookings.OrderByDescending(s => s.DateCreated);
                    break;
                default:
                    bookings = bookings.OrderBy(s => s.TestMethods.Select(tm => tm.methodId).FirstOrDefault());
                    break;
            }

            int pageSize = 10;
            return View(await PaginatedList<Test>.CreateAsync(bookings.AsNoTracking(), page ?? 1, pageSize));
        }

        public IActionResult PatientTestList()
        {
            var UserId = _userMananger.GetUserId(User);
            var patient = _Test.Users.Where(d => d.Id == UserId).FirstOrDefault();
            if (patient == null)
            {
                return NotFound();
            }
            var tests = _Test.Tests.Where(p=> p.PatientId == patient.Id).ToList(); 
            return View(tests);
        }
        public IActionResult TestListView()
        {
            var tests = _Test.Tests.ToList(); 
            return View(tests);
        }
        public IActionResult DoctorTestList()
        {

            var UserId = _userMananger.GetUserId(User);
            var doctor = _Test.Users.Where(d => d.Id == UserId).FirstOrDefault();
            if (doctor == null)
            {
                return NotFound();
            }
            var tests = _Test.Tests.Where(d => d.DoctorId == doctor.Id).ToList(); // Retrieve tests from your data source
            return View(tests);
        }
    }
}
