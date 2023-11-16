using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Nompilo_PHC_Website.Data;
using Nompilo_PHC_Website.Models;
using System.Data;
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
using iText.IO.Image;

namespace Nompilo_PHC_Website.Controllers
{
    public class PrenatalController : Controller
    {
        /*static DateOnly Calc*/
        private readonly ApplicationDbContext _Booking;
        private readonly ApplicationDbContext _Pregnancy;
        private readonly ApplicationDbContext _Patients;

        public PrenatalController(ApplicationDbContext booking)
        {
            _Booking = booking;
            _Pregnancy = booking;
            _Patients = booking;

        }
        private static List<Calculator> Calc = new List<Calculator>();
        private static List<Report> Rep = new List<Report>();
        public IActionResult Dashboard()
        {
            IEnumerable<Booking> bookings = _Booking.Bookings;
            return View(bookings);
        }
        public IActionResult Calculator()
        {
            var Cal = new Calculator();
            return View(Cal);
        }
        [HttpPost]
        public IActionResult CalculatorSub(Calculator cal)
        {
            if (ModelState.IsValid)
            {
                Calc.Add(cal);
                return RedirectToAction("Results");
            }
            return RedirectToAction("Calculator");
        }
        public IActionResult Results()
        {
            return View(Calc);
        }
        public IActionResult Support()
        {
            return View();
        }
        public IActionResult DocDash()
        {
            return View();
        }
        public IActionResult Pregnantpatients()
        {

            IEnumerable<FpregRefers> preg = _Pregnancy.FpregRefers;
            return View(preg);
        }
        public IActionResult Patients()
        {
            var patient = new PrenatalPatients();
            IEnumerable<FpregRefers> preg = _Pregnancy.FpregRefers;
            ViewBag.PatientN = new SelectList(preg, "RefName", "RefName");
            ViewBag.PatientL = new SelectList(preg, "RefLastName", "RefLastName");
            ViewBag.PatientD = new SelectList(preg, "RefDate", "RefDate");
            return View(patient);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddPatients(PrenatalPatients patients)
        {
            if (ModelState.IsValid)
            {
                _Patients.PrenatalPatients.Add(patients);
                _Patients.SaveChanges();
                return RedirectToAction("DisplayP");
            }
            return RedirectToAction("Patients");
        }
        public IActionResult DisplayP()
        {
            IEnumerable<PrenatalPatients> preg = _Patients.PrenatalPatients;
            return View(preg);
        }
        public IActionResult Report()
        {
            var report = new Report();
            return View(report);
        }
        public IActionResult SearchReport(Report report)
        {

            if (ModelState.IsValid)
            {
                Rep.Clear();
                Rep.Add(report);
                return RedirectToAction("DisplayReport");
            }
            return RedirectToAction("Report");
        }
        public IActionResult DisplayReport()
        {
            IEnumerable<PrenatalPatients> preg = _Patients.PrenatalPatients;

            var combinedclass = new Combined
            {
                Patients = preg,
                Report = Rep
            };
            return View(combinedclass);
        }

        public IActionResult GenerateReport()
        {
            // Retrieve product data from the database
            var prenatal = _Booking.PrenatalPatients;

            // Generate report and save as PDF
            var filePath = "Prenatal.pdf";
            GeneratePDFReport(prenatal, filePath);

            // Return a file download response
            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "application/pdf", "PreNatalReport.pdf");
        }
        private void GeneratePDFReport(IEnumerable<PrenatalPatients> prenatal, string filePath)
        {
            using (var writer = new PdfWriter(filePath))
            {
                using (var pdfDocument = new PdfDocument(writer))
                {
                    var document = new Document(pdfDocument);

                    // Add header
                    document.Add(new Paragraph("-----------------").SetTextAlignment(TextAlignment.CENTER).SetMarginBottom(20));
                    document.Add(new Paragraph("PreNatal Report").SetTextAlignment(TextAlignment.CENTER).SetFontSize(20).SetBold());
                    document.Add(new Paragraph("-----------------").SetTextAlignment(TextAlignment.CENTER).SetMarginBottom(20));

                    //Adding Logo


                    // Add date and time
                    var currentDate = DateTime.Now.ToString("d-MMMM-yyyy");
                    document.Add(new Paragraph($"Date: {currentDate}").SetTextAlignment(TextAlignment.RIGHT).SetMarginBottom(5));

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

                    table.AddHeaderCell(CreateHeaderCell("Name"));
                    table.AddHeaderCell(CreateHeaderCell("Last Name"));
                    table.AddHeaderCell(CreateHeaderCell("Months"));
                    table.AddHeaderCell(CreateHeaderCell("Weight"));




                    // Set table data
                    foreach (var prenatals in prenatal)
                    {

                        table.AddCell(CreateTableCell(prenatals.Name.ToString()));
                        table.AddCell(CreateTableCell(prenatals.LastName.ToString()));
                        table.AddCell(CreateTableCell(prenatals.Months.ToString()));
                        table.AddCell(CreateTableCell(prenatals.Weight.ToString()));


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
        public IActionResult Menu()
        {
            return View();
        }
    }
}
