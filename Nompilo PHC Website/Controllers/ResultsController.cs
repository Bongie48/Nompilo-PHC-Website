using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nompilo_PHC_Website.Data;
using Nompilo_PHC_Website.Models;
using System.Security.Claims;
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
using Nompilo_PHC_Website.ViewModels;
using Nompilo_PHC_Website.Repository;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using iText.IO.Image;
using iText.Commons.Actions.Contexts;

namespace Nompilo_PHC_Website.Controllers
{
    public class ResultsController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<DataGeeksUser> _userManager;
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        public ResultsController(ApplicationDbContext dbContext, 
            UserManager<DataGeeksUser> userManager,
            IUserRepository userRepository,
            IHttpContextAccessor contextAccessor)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _userRepository = userRepository;
            _contextAccessor = contextAccessor;
        }
        public IActionResult Index(string search)
        {
            var userId = _userManager.GetUserId(User);

            var records = _dbContext.TestResults.Where(s => s.status == "Active")
                .Include(d => d.User).Include(t => t.Tests)
                .Include(m => m.TestMethods)
                .Where(r => r.UserId == userId)
                .ToList();

            if (search != "" && search != null)
            {
                 records = _dbContext.TestResults.Where(s => s.Description.Contains(search) || s.Result.Contains(search))
                .Include(d => d.User).Include(t => t.Tests)
                .Include(m => m.TestMethods)
                .Where(r => r.UserId == userId)
                .ToList();
            }
            else
            {
                 records = _dbContext.TestResults
                .Include(d => d.User).Include(t => t.Tests)
                .Include(m => m.TestMethods)
                .Where(r => r.UserId == userId)
                .ToList();
            }
            
            return View(records);
        }

        public IActionResult Report()
        {
            var userId = _userManager.GetUserId(User);

            var records = _dbContext.TestResults
                .Include(d => d.User).Include(t => t.Tests)
                .Include(m => m.TestMethods)
                .Where(r => r.UserId == userId)
                .ToList();
                        
            return View(records);
        }

        [HttpGet]
        public IActionResult Insert()
        {

            ViewData["testId"] = new SelectList(_dbContext.Tests, "testId", "testfor");
            ViewData["testmethodId"] = new SelectList(_dbContext.TestMethods, "methodId", "MethodName");
            return View();
        }

        [HttpPost]
        public IActionResult Insert(TestResult res)
        {

            var userId = _userManager.GetUserId(User);
            if (res.Description == null || res.Result == null)
            {
                ViewData["testId"] = new SelectList(_dbContext.Tests, "testId", "testfor");
                ViewData["testmethodId"] = new SelectList(_dbContext.TestMethods, "methodId", "MethodName");
                return View("Insert", res);
            }

            res.UserId = userId;
            res.status = "Active";
            _dbContext.TestResults.Add(res);
            _dbContext.SaveChanges();
            TempData["successAlert"] = "Record saved successfully!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            ViewData["testId"] = new SelectList(_dbContext.Tests, "testId", "testfor");
            ViewData["testmethodId"] = new SelectList(_dbContext.TestMethods, "methodId", "MethodName");
            if (id == 0)
            {
                return View();
            }
            else
            {
                ViewData["testId"] = new SelectList(_dbContext.Tests, "testId", "testfor");
                ViewData["testmethodId"] = new SelectList(_dbContext.TestMethods, "methodId", "MethodName");
                return View(_dbContext.TestResults.Find(id));
            }
        }
        [HttpPost]
        public IActionResult Update(TestResult result)
        {
            

            if (result.Result == null || result.Description == null)
            {
                ViewData["testId"] = new SelectList(_dbContext.Tests, "testId", "testfor");
                ViewData["testmethodId"] = new SelectList(_dbContext.TestMethods, "methodId", "MethodName");
                return View("Update", result);
            }
            
            result.status = "Active";
            _dbContext.TestResults.Update(result);
            _dbContext.SaveChanges();
            TempData["successAlert"] = "Record updated successfully!";
            return RedirectToAction("Index");


        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _dbContext.TestResults == null)
            {
                return NotFound();
            }
            var result = await _dbContext.TestResults.FirstOrDefaultAsync(x => x.resultsId == id);
            if (result == null)
            {
                return NotFound();
            }
            return View(result);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_dbContext.TestResults == null)
            {
                return Problem("Entity set 'dbContext' is null");
            }
            var result = await _dbContext.TestResults.FindAsync(id);
            
            result.status = "In-Active";
            _dbContext.TestResults.Update(result);
            await _dbContext.SaveChangesAsync();
            TempData["successAlert"] = "Record deleted successfully!";
            return RedirectToAction(nameof(Index));

            

        }


        public IActionResult GenerateReport()
        {
            // Getting test results from the database
            var results = _dbContext.TestResults;


            // Generate report and save as PDF
            var filePath = "TestResults.pdf";
            GeneratePDFReport(results, filePath);

            // Return a file download response
            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "application/pdf", "TestResultsReport.pdf");
        }
        private void GeneratePDFReport(IEnumerable<TestResult> testResults, string filePath)
        {
            using (var writer = new PdfWriter(filePath))
            {
                using (var pdfDocument = new PdfDocument(writer))
                {
                    var document = new Document(pdfDocument);

                    // Add header
                    document.Add(new Paragraph("Test Results Report").SetTextAlignment(TextAlignment.CENTER).SetFontSize(20).SetBold());
                    document.Add(new Paragraph("---------------------").SetTextAlignment(TextAlignment.CENTER).SetMarginBottom(20));

                    // Generate report date
                    var UserId = _userManager.GetUserId(User);
                    var scientistName = _dbContext.TestResults
                        .Include(r => r.User) 
                        .Where(r => r.UserId == UserId)
                        .Select(r => r.User.FullName) 
                        .FirstOrDefault();

                    var currentDate = DateTime.Now.ToString("d-MMMM-yyyy");
                    document.Add(new Paragraph($"Generated By: {scientistName}").SetTextAlignment(TextAlignment.RIGHT).SetMarginBottom(5));
                    document.Add(new Paragraph($"Date: {currentDate}").SetTextAlignment(TextAlignment.RIGHT).SetMarginBottom(5));

                    //Address
                    var addressParagraph = new Paragraph()
                        .Add(new Text("Nompilo PHC").SetBold()).Add(new LineSeparator(new SolidLine())).Add(Environment.NewLine)
                        .Add("22 Admiralty Crescent").AddTabStops(new TabStop(500, TabAlignment.LEFT)).Add("City, State, Zip Code").Add(Environment.NewLine)
                        .Add("Phone: (041) 456-7890").AddTabStops(new TabStop(500, TabAlignment.LEFT))
                        .Add(new Tab()).Add("Email: info@datageeks.com");
                    document.Add(addressParagraph);
                    document.Add(new Paragraph().SetMarginBottom(20));

                    // Keeping track of positive and negative results
                    var positiveCounts = new Dictionary<string, int>();
                    var negativeCounts = new Dictionary<string, int>();

                    // Looping through the test results
                    foreach (var result in testResults)
                    {
                        string testName = _dbContext.Tests
                            .Where(t => t.testId == result.testId)
                            .Select(t => t.testfor)
                            .FirstOrDefault();

                        // Check if the result is positive or negative
                        if (result.Result == "Positive")
                        {
                            // Increment the positive count for this test
                            if (positiveCounts.ContainsKey(testName))
                                positiveCounts[testName]++;
                            else
                                positiveCounts[testName] = 1;
                        }
                        else if (result.Result == "Negative")
                        {
                            // Increment the negative count for this test
                            if (negativeCounts.ContainsKey(testName))
                                negativeCounts[testName]++;
                            else
                                negativeCounts[testName] = 1;
                        }

                        
                    }

                    // Add table for positive results
                    document.Add(new Paragraph("Positive Results").SetBold().SetMarginTop(20));
                    var positiveTable = new Table(UnitValue.CreatePercentArray(2)).UseAllAvailableWidth();
                    positiveTable.AddHeaderCell(CreateHeaderCell("Test For"));
                    positiveTable.AddHeaderCell(CreateHeaderCell("Count"));

                    foreach (var testName in positiveCounts.Keys)
                    {
                        positiveTable.AddCell(CreateTableCell(testName));
                        positiveTable.AddCell(CreateTableCell(positiveCounts[testName].ToString()));
                    }

                    document.Add(positiveTable);

                    // Add table for negative results
                    document.Add(new Paragraph("Negative Results").SetBold().SetMarginTop(20));
                    var negativeTable = new Table(UnitValue.CreatePercentArray(2)).UseAllAvailableWidth();
                    negativeTable.AddHeaderCell(CreateHeaderCell("Test For"));
                    negativeTable.AddHeaderCell(CreateHeaderCell("Count"));

                    foreach (var testName in negativeCounts.Keys)
                    {
                        negativeTable.AddCell(CreateTableCell(testName));
                        negativeTable.AddCell(CreateTableCell(negativeCounts[testName].ToString()));
                    }

                    document.Add(negativeTable);

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
