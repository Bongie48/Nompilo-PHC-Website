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

namespace Nompilo_PHC_Website.Controllers
{
    public class InstrumentController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public InstrumentController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index(string search)
        {
            ViewBag.success = TempData["successAlert"] as string;
            var method = _dbContext.Instruments.Include(t => t.Tests)
                .Where(s => s.status =="Active").ToList();

            if (search != "" && search != null)
            {
                method = _dbContext.Instruments.
                 Where(i => i.name.Contains(search)||i.description.Contains(search))
                .Include(t => t.Tests)
                .Where(s => s.status == "Active").ToList();
            }
            else
            {
                method = _dbContext.Instruments.Include(t => t.Tests)
                .Where(s => s.status == "Active").ToList();
            }

            return View(method);
        }

        [HttpGet]
        public IActionResult AddOrEdit()
        {
            //Foreign Key
            ViewData["testId"] = new SelectList(_dbContext.Tests, "testId", "testfor");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddOrEdit(Instrument instrument)
        {
            instrument.status = "Active";
            if (ModelState.IsValid)
            {
                
                  _dbContext.Instruments.Add(instrument);
                    _dbContext.SaveChanges();
                    TempData["successAlert"] = "Record saved successfully!";
                    return RedirectToAction("Index");
                

            }

            ViewData["testId"] = new SelectList(_dbContext.Tests, "testId", "testfor");
            return View("AddOrEdit", instrument);
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            ViewData["testId"] = new SelectList(_dbContext.Tests, "testId", "testfor");
            if (id == 0)
            {
                return View();
            }
            else
            {
                return View(_dbContext.Instruments.Find(id));
            }
            
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Instrument instrument)
        {
            if (ModelState.IsValid)
            {
                instrument.status = "Active";
                _dbContext.Instruments.Update(instrument);
                _dbContext.SaveChanges();
                TempData["successAlert"] = "Record updated successfully!";
                return RedirectToAction("Index");


            }

            ViewData["testId"] = new SelectList(_dbContext.Tests, "testId", "testfor");
            return View("Update", instrument);
        }
        

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _dbContext.Instruments == null)
            {
                return NotFound();
            }
            var instrument = await _dbContext.Instruments.FirstOrDefaultAsync(x => x.instrumentId == id);
            if (instrument == null)
            {
                return NotFound();
            }
            return View(instrument);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_dbContext.Instruments == null)
            {
                return Problem("Entity set 'dbContext' is null");
            }
            var instrument = await _dbContext.Instruments.FindAsync(id);
            if (instrument != null)
            {
                instrument.status = "In-Active";
                _dbContext.Instruments.Update(instrument);
            }
            await _dbContext.SaveChangesAsync();
            TempData["successAlert"] = "Record deleted successfully!";
            return RedirectToAction(nameof(Index));

        }


        public IActionResult GenerateReport()
        {
            // Retrieve product data from the database
            var instruments = _dbContext.Instruments;

          

            // Generate report and save as PDF
            var filePath = "Instruments.pdf";
            GeneratePDFReport(instruments, filePath);

            // Return a file download response
            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "application/pdf", "InstrumentReport.pdf");
        }
        private void GeneratePDFReport(IEnumerable<Instrument> instruments, string filePath)
        {
            using (var writer = new PdfWriter(filePath))
            {
                using (var pdfDocument = new PdfDocument(writer))
                {
                    var document = new Document(pdfDocument);

                    // Add header
                    document.Add(new Paragraph("-----------------").SetTextAlignment(TextAlignment.CENTER).SetMarginBottom(20));
                    document.Add(new Paragraph("Test Results").SetTextAlignment(TextAlignment.CENTER).SetFontSize(20).SetBold());
                    document.Add(new Paragraph("-----------------").SetTextAlignment(TextAlignment.CENTER).SetMarginBottom(20));

                    //Adding Logo
                    string imagePath = "wwwroot/css/body.png"; // Replace with the path to your image
                    Image logoImage = new Image(ImageDataFactory.Create(imagePath));
                    logoImage.SetWidth(100); // Set the desired width
                    logoImage.SetHeight(100); // Set the desired height
                    document.Add(logoImage);

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
                    table.AddHeaderCell(CreateHeaderCell("Test For"));
                    table.AddHeaderCell(CreateHeaderCell("Description"));

                    

                    // Set table data
                    foreach (var instrument in instruments)
                    {
                        string testName = _dbContext.Tests
                        .Where(t => t.testId == instrument.testId)
                        .Select(t => t.testfor)
                        .FirstOrDefault();

                        table.AddCell(CreateTableCell(instrument.instrumentId.ToString()));
                        table.AddCell(CreateTableCell(instrument.name.ToString()));
                        table.AddCell(CreateTableCell(testName));
                        table.AddCell(CreateTableCell(instrument.description.ToString()));

                        
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
