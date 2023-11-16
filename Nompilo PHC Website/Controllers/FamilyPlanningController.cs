using iText.Kernel.Pdf;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Layout;
using iText.Kernel.Events;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf.Canvas;
using iText.Layout.Borders;
using iText.IO.Font;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nompilo_PHC_Website.Data;
using Nompilo_PHC_Website.Models;
using iText.IO.Image;
using iText.Forms.Xfdf;

namespace Nompilo_PHC_Website.Controllers
{
    public class FamilyPlanningController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        
        public FamilyPlanningController(ApplicationDbContext dbD)
        {
            dbContext = dbD;
        }
        public IActionResult CycleSummary()
        {
            IEnumerable<ContraceptiveRecordN> objList2 = dbContext.ContraceptiveRecordN;
            return View(objList2);
        }
        public IActionResult FPatient()
        {
            
            return View();
        }
        public IActionResult LogSyms()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LogSyms(LogSyms model)
        {
            
            model.EmailAdd= User.Identity?.Name;
            dbContext.LogSyms.Add(model);
          
            dbContext.SaveChanges();
            TempData["AlertMessage"] = "Symptoms successfully logged!";
            return RedirectToAction("RecordTemp");
        }

        public IActionResult viewSymp()
        {
            DateTime ThisMon = DateTime.Now;
            var MonthOnly = dbContext.LogSyms.Where(b => b.Day.Month.Equals(ThisMon.Month));
            return View(MonthOnly);
        }
        
        public IActionResult NviewSymp()
        {
            DateTime ThisMon = DateTime.Now;
            var MonthOnly = dbContext.LogSyms.Where(b => b.Day.Month.Equals(ThisMon.Month));
            return View(MonthOnly);
        }
        [HttpGet]
        public async Task<IActionResult> NviewSymp(string recordSearh)
        {
            ViewData["GetSy"] = recordSearh;
            var record = from x in dbContext.LogSyms select x;
            if (!String.IsNullOrEmpty(recordSearh))
            {
                record = record.Where(x => x.EmailAdd.Contains(recordSearh));
               

            }

            return View(await record.AsNoTracking().ToListAsync());
        }
        public IActionResult FNurse()
        {
            return View();
        }
        public IActionResult RecordTemp()
        {

            IEnumerable<LogSyms> objList2 = dbContext.LogSyms;
            return View(objList2);
        }
        public IActionResult RecordSummary()
        {

            IEnumerable<EverithingPeriodsF> objList2 = dbContext.EverithingPeriodsF;
            return View(objList2);
        }
        [HttpGet]
        public async Task<IActionResult> RecordSummary(string recordSearh)
        {
            ViewData["GetEverithingPeriods"] = recordSearh;
            var record = from x in dbContext.EverithingPeriodsF select x;
            if (!String.IsNullOrEmpty(recordSearh))
            {
                record = record.Where(x => x.MonthYear.Contains(recordSearh) || x.Year.Contains(recordSearh) 
                || x.Symptoms.Contains(recordSearh));
                
            }
           
            return View(await record.AsNoTracking().ToListAsync());
        }

        public IActionResult LogPeriod()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LogPeriod(EverithingPeriodsF mod)
        {
            
            mod.End = mod.Start.AddDays(4);
            mod.PregnancyPrediction = mod.End.AddDays(1);
            mod.PregLastDay = mod.Ovulation1.AddDays(2);
            mod.NextPeriod = mod.Start.AddDays(28);
            mod.Ovulation1 = mod.Start.AddDays(14);
            mod.Symptoms = mod.Start.Month.ToString();
            mod.Year = mod.Start.Year.ToString();
            mod.MonthYear = mod.Symptoms + "/" + mod.Year;
            mod.DayDifference = mod.End.Day - mod.Start.Day;
            mod.EmailAdd = User.Identity?.Name;
            if (mod.DayDifference == 0)
            {
                mod.Abnormality = "Spotting";
            }
            if (mod.DayDifference > 8 || mod.DayDifference <= 0)
            {
                mod.Abnormality = "Abnormal.";
            }
            else
            {
                mod.Abnormality = "Normal";
            }
            dbContext.EverithingPeriodsF.Add(mod);
            dbContext.SaveChanges();
            TempData["AlertMessage"] = " Menstrual Cycle successfully logged!";
            return RedirectToAction("RecordSummary");

            
        }
        //Edit last period date
        public IActionResult LogUpdate(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var dbC = dbContext.EverithingPeriodsF.Find(Id);
            if (dbC == null)
            {
                return NotFound();
            }
            return View(dbC);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LogUpdate(EverithingPeriodsF record)
        {
            record.PregnancyPrediction = record.End.AddDays(1);
            record.DayDifference = record.End.Day - record.Start.Day;
            if (record.DayDifference > 8 || record.DayDifference <= 0)
            {
                record.Abnormality = "Abnormal";
            }
            else
            {
                record.Abnormality = "Normal";
            }
            dbContext.EverithingPeriodsF.Update(record);
            dbContext.SaveChanges();
            TempData["AlertMessage"] = " Menstrual Cycle log successfully updated!";
            return RedirectToAction("RecordSummary");
        }
        public IActionResult LogDelete(int? Id)
        {
            var dbC = dbContext.EverithingPeriodsF.Find(Id);
            if (dbC == null)
            {
                return NotFound();
            }
            dbContext.EverithingPeriodsF.Remove(dbC);
            dbContext.SaveChanges();
            TempData["AlertMessage"] = " You have successfully deleted menstrual cycle log!";
            return RedirectToAction("RecordSummary");
        }
        public IActionResult ConfD(int? Id)
        {
            var dbC = dbContext.Bookings.Find(Id);
            if (dbC == null)
            {
                return NotFound();
            }
            dbContext.Bookings.Remove(dbC);
            dbContext.SaveChanges();
            TempData["Alert23"] = " You have successfully confirmed to attend a patient";
            return RedirectToAction("PregRes");
        }
        public IActionResult PeriodPrediction()
        {
            IEnumerable< EverithingPeriodsF> MyLists = dbContext.EverithingPeriodsF;
            return View(MyLists);
        }
        [HttpGet]
        public async Task<IActionResult> PeriodPrediction(string recordSearh)
        {
            ViewData["GetEverithingPeriods"] = recordSearh;
            var record = from x in dbContext.EverithingPeriodsF select x;
            if (!String.IsNullOrEmpty(recordSearh))
            {
                record = record.Where(x => x.MonthYear.Contains(recordSearh) || x.Year.Contains(recordSearh) || x.Symptoms.Contains(recordSearh));
            }
            return View(await record.AsNoTracking().ToListAsync());
        }
        public IActionResult PeriodsReport()
        {
            IEnumerable<ContraceptiveRecordN> MyList = dbContext.ContraceptiveRecordN;
            return View(MyList);
        }
        [HttpGet]
        public async Task<IActionResult> PeriodsReport(string recordSearh)
        {
           
            ViewData["GetPatientRecord"] = recordSearh;
            
            var record = from x in dbContext.ContraceptiveRecordN select x;
            if (!String.IsNullOrEmpty(recordSearh))
            {
                record = record.Where(x => x.Year.Contains(recordSearh) || x.MonthYear.Contains(recordSearh));
                
            }

            if (record != null)
            {
                ViewData["GetPatientRecord"] = recordSearh;
                record = from x in dbContext.ContraceptiveRecordN select x;
                if (!String.IsNullOrEmpty(recordSearh))
                {
                    record = record.Where(x => x.Year.Contains(recordSearh) || x.MonthYear.Contains(recordSearh));
                    
                }
            }
            return View(await record.AsNoTracking().ToListAsync());
        }
        public IActionResult MethodReport()
        {
            IEnumerable<ContraceptiveRecordN> MyList = dbContext.ContraceptiveRecordN;
            return View(MyList);
        }
        [HttpGet]
        public async Task<IActionResult> MethodReport(string recordSearh)
        {

            ViewData["GetPatientRecord"] = recordSearh;

            var record = from x in dbContext.ContraceptiveRecordN select x;
            if (!String.IsNullOrEmpty(recordSearh))
            {
                record = record.Where(x => x.Method.Contains(recordSearh));

            }
            if (record != null)
            {
                
                record = from x in dbContext.ContraceptiveRecordN select x;
                if (!String.IsNullOrEmpty(recordSearh))
                {
                    record = record.Where(x => x.Method.Contains(recordSearh) );

                }
            }
            return View(await record.AsNoTracking().ToListAsync());
        }
        public IActionResult AllView()
        {
            IEnumerable<ContraceptiveRecordN> MyList = dbContext.ContraceptiveRecordN;
            return View(MyList);
        }
        [HttpGet]
        public async Task<IActionResult> AllView(string recordSearh)
        {

            ViewData["GetPatientRecord"] = recordSearh;

            var record = from x in dbContext.ContraceptiveRecordN select x;
            if (!String.IsNullOrEmpty(recordSearh))
            {
                record = record.Where(x => x.QSym.Contains(recordSearh));

            }
            if (record != null)
            {

                record = from x in dbContext.ContraceptiveRecordN select x;
                if (!String.IsNullOrEmpty(recordSearh))
                {
                    record = record.Where(x => x.QSym.Contains(recordSearh));

                }
            }
            return View(await record.AsNoTracking().ToListAsync());
        }
        //Nurse Side
        public IActionResult PregRes()
        {
            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PregRes(FPregnancy models)
        {
            if (ModelState.IsValid)
            {
                dbContext.FPregnancy.Add(models);
                dbContext.SaveChanges();
                if (models.Results == "Pregnant")
                {
                    return RedirectToAction("FPregnancyRefer");
                    
                }
                if (models.Results =="Not Pregnant")
                {
                    return RedirectToAction("FReg");
                }
            }
            return View(models);
        }
        public IActionResult FPregnancyRefer()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult FPregnancyRefer(FpregRefers models)
        {
            if (ModelState.IsValid)
            {
               
                dbContext.FpregRefers.Add(models);
                dbContext.SaveChanges();
                TempData["AlertMessPRE"] = "Patient successfully referred!";
                return RedirectToAction("FNurse");
            }
            return View(models);
        }
        public IActionResult FRegister()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult FRegister(RegisterFPatients models)
        {
            if (ModelState.IsValid)
            {
                dbContext.RegisterFPatients.Add(models);
                
                dbContext.SaveChanges();
                
                if (models.CheckRegister == "Registered")
                {
                    return RedirectToAction("Index2");

                }
                if (models.CheckRegister == "Not Registered")
                {
                    return RedirectToAction("NewFRecord");
                }
            }
            return View(models);
        }
        public IActionResult FReg()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult FReg(CheckType models)
        {
            if (ModelState.IsValid)
            {
                dbContext.CheckType.Add(models);

                dbContext.SaveChanges();

                if (models.CheckRegister == "Emergency contraceptive")
                {
                    return RedirectToAction("Index4");

                }
                if (models.CheckRegister == "Long term contraceptive")
                {
                    return RedirectToAction("FRegister");
                }
            }
            return View(models);
        }
        public IActionResult Index2()
        {
            IEnumerable<ContraceptiveRecordN> MyList = dbContext.ContraceptiveRecordN;
            return View(MyList);
        }
        [HttpGet]
        public async Task<IActionResult> Index2(string recordSearh)
        {
            ViewData["GetPatientRecord"] = recordSearh;
            var record = from x in dbContext.ContraceptiveRecordN select x;
            if (!String.IsNullOrEmpty(recordSearh))
            {
                record = record.Where(x => x.IdentityNumber.Contains(recordSearh));
            }
            return View(await record.AsNoTracking().ToListAsync());
        }
        public IActionResult Index5()
        {
            IEnumerable<Booking> MyList = dbContext.Bookings;
            return View(MyList);
        }
        [HttpGet]
        public async Task<IActionResult> Index5(string recordSearh)
        {
            ViewData["GetPatientRecord"] = recordSearh;
            var record = from x in dbContext.Bookings select x;
            if (!String.IsNullOrEmpty(recordSearh))
            {
                record = record.Where(x => x.email.Contains(recordSearh));
            }
            return View(await record.AsNoTracking().ToListAsync());
        }
        public IActionResult Index3()
        {
            IEnumerable<ContraceptiveRecordN> MyList = dbContext.ContraceptiveRecordN;
            return View(MyList);
        }
        [HttpGet]
        public async Task<IActionResult> Index3(string recordSearh)
        {
            ViewData["GetPatientRecord"] = recordSearh;
            var record = from x in dbContext.ContraceptiveRecordN select x;
            if (!String.IsNullOrEmpty(recordSearh))
            {
                record = record.Where(x => x.IdentityNumber.Contains(recordSearh));
            }
            return View(await record.AsNoTracking().ToListAsync());
        }
        public IActionResult Index4()
        {
            IEnumerable<EmeContra> MyList = dbContext.EmeContra;
            return View(MyList);
        }
        [HttpGet]
        public async Task<IActionResult> Index4(string recordSearh)
        {
            ViewData["GetPatientRecord"] = recordSearh;
            var record = from x in dbContext.EmeContra select x;
            if (!String.IsNullOrEmpty(recordSearh))
            {
                record = record.Where(x => x.IdentityNumber.Contains(recordSearh));
            }
            return View(await record.AsNoTracking().ToListAsync());
        }
        public IActionResult NewFRecord()
        {
            return View();
        }

        //Retrieve medical File
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult NewFRecord(ContraRegister models)
        {
            if (ModelState.IsValid)
            {
                
                dbContext.ContraRegister.Add(models);
                dbContext.SaveChanges();
                TempData["AlertMessage1"] = "Patient successfully registered for regular contraceptive!";
                return RedirectToAction("SympP");
            }
            return View(models);
        }
        public IActionResult SympP()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SympP(SympClass model)
        {
            if (model.QSym == "Yes")
            {
                return RedirectToAction("NviewSymp");

            }
            if (model.QSym == "No")
            {
                return RedirectToAction("FRecord");
            }
            return View(model);
        }
        public IActionResult FRecord()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult FRecord(ContraceptiveRecordN model)
        {
            model.QSym = "View All";
            model.Year = model.CurrentDate.Year.ToString();
            model.MonthYear = model.CurrentDate.ToString("MMMM yyyy");
            
            dbContext.ContraceptiveRecordN.Add(model);
            dbContext.SaveChanges();
            TempData["AlertMess2"] = "Patient record successfully addded!";
            return RedirectToAction("Index2");
           
        }
        public IActionResult EmeRecord()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EmeRecord(EmeContra model)
        {
            model.QSym = "View All";
            model.Year = model.CurrentDate.Year.ToString();
            
            dbContext.EmeContra.Add(model);
            dbContext.SaveChanges();
            TempData["AlertMessN"] = "Patient record successfully added!";
            return RedirectToAction("Index4");

        }
        public IActionResult CRUpdate(int? CRId)
        {
            if (CRId == null || CRId == 0)
            {
                return NotFound();
            }
            var dbC = dbContext.ContraceptiveRecordN.Find(CRId);
            if (dbC == null)
            {
                return NotFound();
            }
            return View(dbC);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CRUpdate(ContraceptiveRecordN record)
        {
            dbContext.ContraceptiveRecordN.Update(record);
            dbContext.SaveChanges();
            return RedirectToAction("index2");
        }
        
        public IActionResult WaitingPatients()
        {
            return View();
        }
        //Side Bar
        public IActionResult Serv()
        {
            return View();
        }
        public IActionResult FReport()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult FReport( RepOption model)
        {
            if (ModelState.IsValid)
            {
                dbContext.RepOption.Add(model);
                dbContext.SaveChanges();
                if (model.ReportOption == "Date")
                {
                    return RedirectToAction("PeriodsReport");
                }
                else if (model.ReportOption == "Method")
                {
                    return RedirectToAction("MethodReport");
                }
                else if (model.ReportOption == "View All")
                {
                    return RedirectToAction("AllView");
                }
            }
            return View();
        }
        ///Nurse Side
        public IActionResult WorkEthic()
        {
            return View();
        }
        public IActionResult NReport()
        {
            return View();
        }
        public IActionResult ScheduleUpdate()
        {
            return View();
        }

        //Generate PDF
        public IActionResult GenerateReport()
        {
            // Retrieve product data from the database
            var ContraceptiveRecordN = dbContext.ContraceptiveRecordN;



            // Generate report and save as PDF
            var filePath = "Contraceptive.pdf";
            GeneratePDFReport(ContraceptiveRecordN, filePath);

            // Return a file download response
            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "application/pdf", "ContraceptiveReport.pdf");
        }
        private void GeneratePDFReport(IEnumerable<ContraceptiveRecordN> contraceptiveRecordN, string filePath)
        {
            using (var writer = new PdfWriter(filePath))
            {
                using (var pdfDocument = new PdfDocument(writer))
                {
                    var document = new Document(pdfDocument);

                    // Add header
                    document.Add(new Paragraph("Contraceptive Report").SetTextAlignment(TextAlignment.CENTER).SetFontSize(20).SetBold());
                    document.Add(new Paragraph("-------------------------------").SetTextAlignment(TextAlignment.CENTER).SetMarginBottom(20));

                    //Adding Logo
                    string imagePath = "wwwroot/css/DGEEK.jpeg"; // Replace with the path to your image
                    Image logoImage = new Image(ImageDataFactory.Create(imagePath));
                    logoImage.SetWidth(100); // Set the desired width
                    logoImage.SetHeight(100); // Set the desired height
                    document.Add(logoImage);

                    // Add date and time
                    var currentDate = DateTime.Now.ToString("d-MMMM-yyyy");
                    var currentTime = DateTime.Now.ToString("HH:mm");
                    document.Add(new Paragraph($"Date: {currentDate}").SetTextAlignment(TextAlignment.RIGHT).SetMarginBottom(5));
                    document.Add(new Paragraph($"Time: {currentTime}").SetTextAlignment(TextAlignment.RIGHT).SetMarginBottom(10));

                    // Add address section
                    var addressParagraph = new Paragraph()
                        .Add(new Text("Nompilo PHC").SetBold()).Add(new LineSeparator(new SolidLine())).Add(Environment.NewLine)
                        .Add("22 Admiralty Crescent").AddTabStops(new TabStop(500, TabAlignment.LEFT)).Add("City, State, Zip Code").Add(Environment.NewLine)
                        .Add("Phone: (041) 456-7890").AddTabStops(new TabStop(500, TabAlignment.LEFT))
                        .Add(new Tab()).Add("Email: info@datageeks.com");
                    document.Add(addressParagraph);
                    document.Add(new Paragraph().SetMarginBottom(20));

                    // Add product table
                    var table = new iText.Layout.Element.Table(UnitValue.CreatePercentArray(4)).UseAllAvailableWidth();

                    // Set table headers
                    //table.AddHeaderCell(CreateHeaderCell(" Consultation Date"));
                    table.AddHeaderCell(CreateHeaderCell("Method"));
                    table.AddHeaderCell(CreateHeaderCell("Weight (KG)"));
                    table.AddHeaderCell(CreateHeaderCell("Symptoms"));
                    //table.AddHeaderCell(CreateHeaderCell("Pregnancy Test Results").SetWidth(70));
                    table.AddHeaderCell(CreateHeaderCell("Return Date").SetWidth(30));



                    // Set table data
                    foreach (var contraceptive in contraceptiveRecordN)
                    {

                        if (contraceptive.EmailAddres == @User.Identity?.Name)
                        {
                            
                            //table.AddCell(CreateTableCell(contraceptive.CurrentDate.ToString("dd MMMM yyyy")));
                            table.AddCell(CreateTableCell(contraceptive.Method.ToString()));
                            table.AddCell(CreateTableCell(contraceptive.Weight.ToString()));
                            table.AddCell(CreateTableCell(contraceptive.Symptoms.ToString()));
                            //table.AddCell(CreateTableCell(contraceptive.TestResults));
                            table.AddCell(CreateTableCell(contraceptive.ReturnDate.ToString("dd MMMM yyyy")));
                        }
                    }

                    // Add table footer with total
                    table.AddFooterCell(new Cell().SetBorder(Border.NO_BORDER));
                    table.AddFooterCell(new Cell().SetBorder(Border.NO_BORDER));
                    table.AddFooterCell(new Cell().SetBorder(Border.NO_BORDER));
                    //table.AddFooterCell(CreateHeaderCell("Total:")); // Header c
                    table.AddFooterCell(new Cell().SetBorder(Border.NO_BORDER));
                    table.AddFooterCell(new Cell().SetBorder(Border.NO_BORDER));
                    table.AddFooterCell(new Cell().SetBorder(Border.NO_BORDER));
                    //table.AddFooterCell(CreateTableCell(total.ToString("C"))); 

                    document.Add(table);

                    // Add page numbering
                    var pageNumber = new PageNumberDocumentEventHandler();
                    pdfDocument.AddEventHandler(PdfDocumentEvent.END_PAGE, pageNumber);

                    document.Close();
                }
            }

            Console.WriteLine("Report generated successfully and saved to the specified PDF file.");
        }
        private Cell CreateHeaderCell(string text)
        {
            var cell = new Cell().SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                .SetPadding(5).SetFontColor(DeviceRgb.WHITE).SetFont(PdfFontFactory.CreateFont());

            cell.Add(new Paragraph(text).SetBold());

            return cell;
        }
        private Cell CreateTableCell(string text)
        {
            return new Cell().SetTextAlignment(TextAlignment.CENTER).SetPadding(5).Add(new Paragraph(text));
        }
        public class PageNumberDocumentEventHandler : IEventHandler
        {
            private int pageCount;

            public void HandleEvent(Event currentEvent)
            {
                PdfDocumentEvent docEvent = (PdfDocumentEvent)currentEvent;
                PdfDocument pdfDoc = docEvent.GetDocument();
                PdfPage page = docEvent.GetPage();

                pageCount = pdfDoc.GetNumberOfPages();
                PdfCanvas canvas = new PdfCanvas(page.NewContentStreamBefore(), page.GetResources(), pdfDoc);
                iText.Kernel.Geom.Rectangle pageSize = page.GetPageSizeWithRotation();
                canvas.BeginText()
                    .SetFontAndSize(PdfFontFactory.CreateFont(), 10)
                    .MoveText(pageSize.GetWidth() - 100, 20)
                    .ShowText($"Page {pageCount}")
                    .EndText();
                canvas.Release();
            }
        }

    }
}
