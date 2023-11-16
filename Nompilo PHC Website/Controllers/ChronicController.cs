using Microsoft.AspNetCore.Mvc;
using Nompilo_PHC_Website.Data;
using Nompilo_PHC_Website.EmailSender;
using Nompilo_PHC_Website.Models;

namespace Nompilo_PHC_Website.Controllers
{
    public class ChronicController : Controller
    {
        private readonly ApplicationDbContext _Booking;
        private readonly ApplicationDbContext _Reminder;
        private readonly ApplicationDbContext _Preport;
        private readonly ApplicationDbContext _Cpatients;
        private readonly IEmailSender _sender;
        public ChronicController(ApplicationDbContext booking, IEmailSender emailsender)
        {
            _Booking = booking;
            _Reminder = booking;
            _Preport = booking;
            _Cpatients = booking;
            this._sender = emailsender;
        }
        public IActionResult Chronic()
        {
            return View();
        }
        public IActionResult Consultations()
        {
            IEnumerable<Booking> bookings = _Booking.Bookings;
            return View(bookings);
        }
        public IActionResult Prescription()
        {
            IEnumerable<Chronicpatients> chronicpatients = _Cpatients.Chronicpatients;
            return View(chronicpatients);
        }
        public IActionResult DoctorDash()
        {
            return View();
        }
        public IActionResult ProgressStats()
        {
            IEnumerable<Rvalues> values = _Preport.Rvalues;
            return View(values);
        }
        public async Task<IActionResult> Sendemail(Reminder reminder)
        {
            if (ModelState.IsValid)
            {
                string days = reminder.Days;
                string time = reminder.Time;
                string email = reminder.email;
                _Reminder.Reminder.Add(reminder);
                _Reminder.SaveChanges();
                string msg = $"This email is to conform that you have opted in to get medication collection/delivery reminders.\n\nYou have set the reminder to notify {days} before your collection/delivery date at {time}\n\nHave a wonderful day\n\n\n\n\n\nYours Sincerely\nEnompilo Health Care Team";
                var rec = email.ToLower();
                var subject = "Medication collection/delivery reminder";
                var message = msg ;

                await _sender.SendEmailAsync(rec, subject, message);
                return RedirectToAction("ViewReminder");
            }
            return View("ReminderOptin");
        }
        public IActionResult ReminderOptin()
        {
            bool rem = false;
            IEnumerable<Reminder> reminder = _Reminder.Reminder;
            foreach(var item in reminder)
            {
                if(TempData["Email"].ToString()==item.email)
                {
                    rem = true;
                }
            }
            if(rem)
            {
                return RedirectToAction("ViewReminderSet");
            }
            else
            {
                var reminders = new Reminder();
                return View(reminders);
            }
            
        }
        public IActionResult ViewReminder()
        {
            IEnumerable<Reminder> reminder = _Reminder.Reminder;
            return View(reminder);
        }
        public IActionResult ViewReminderSet()
        {
            IEnumerable<Reminder> reminder = _Reminder.Reminder;
            return View(reminder);
        }
        public IActionResult UpdateReminder(int? ID)
        {
            if (ID == null || ID == 0)
            {
                return NotFound();
            }
            var obj = _Reminder.Reminder.Find(ID);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        public async Task<IActionResult> Updaterem(Reminder reminder)
        {
            if (ModelState.IsValid)
            {
                string days = reminder.Days;
                string time = reminder.Time;
                string email = reminder.email;
                _Reminder.Reminder.Update(reminder);
                _Reminder.SaveChanges();
                string msg = $"This email is to conform that you have updated the medication collection/delivery reminder notification settings.\n\nYou have set the reminder to notify {days} before your collection/delivery date at {time}\n\nHave a wonderful day\n\n\n\n\n\nYours Sincerely\nEnompilo Health Care Team";
                var rec = email.ToLower();
                var subject = "Medication collection/delivery reminder";
                var message = msg;

                await _sender.SendEmailAsync(rec, subject, message);
                return RedirectToAction("ViewReminder");
            }
            return View();
        }
        public IActionResult Patientreport()
        {
            var report = new Rvalues();
            return View(report);
        }
        public IActionResult Preport(Rvalues values)
        {
            if (ModelState.IsValid)
            {
                _Preport.Rvalues.Add(values);
                _Preport.SaveChanges();
                return RedirectToAction("DoctorDash");
            }
            return RedirectToAction("Patientreport");
        }
        public IActionResult Patientonlineconsultations()
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
        public IActionResult Updatestatus(Booking book)
        {
            _Booking.Bookings.Update(book);
            _Booking.SaveChanges();
            return RedirectToAction("Patientonlineconsultations");
        }
        public IActionResult AddCPatient()
        {
            var patients = new Chronicpatients();
            return View(patients);
        }
        public IActionResult AddC(Chronicpatients cpatients)
        {
            if (ModelState.IsValid)
            {
                _Cpatients.Chronicpatients.Add(cpatients);
                _Cpatients.SaveChanges();
                return RedirectToAction("DisplayPatients");
            }
            return RedirectToAction("AddCPatient");
        }
        public IActionResult DisplayPatients()
        {
            IEnumerable<Chronicpatients> chronicpatients = _Cpatients.Chronicpatients;
            return View(chronicpatients);
        }
        private static List<QueryReport> Rep = new List<QueryReport>();
        public IActionResult Report()
        {
            var patients = new QueryReport();
            IEnumerable<Chronicpatients> chronicpatients = _Cpatients.Chronicpatients;
            IEnumerable<Rvalues> values = _Preport.Rvalues;
            IEnumerable<Reminder> reminder = _Reminder.Reminder;
            var report = new ChronicReport()
            {
                queryReport = patients,
                reports = Rep,
                Rem = reminder,
                Chrcpatients = chronicpatients,
                Scores = values

            };
            return View(report);
        }
        public IActionResult GetReport()
        {
            var patients = new QueryReport();
            return View(patients);
        }
        public IActionResult SearchReport(QueryReport qr)
        {
            Rep.Clear();
            Rep.Add(qr);
            return RedirectToAction("Report");
        }
    }
}
