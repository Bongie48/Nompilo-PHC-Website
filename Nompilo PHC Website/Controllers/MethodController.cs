using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nompilo_PHC_Website.Data;
using Nompilo_PHC_Website.Models;
using System.Diagnostics.Metrics;

namespace Nompilo_PHC_Website.Controllers
{
    public class MethodController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public MethodController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index(string search)
        {
            var method = _dbContext.TestMethods.Include(t => t.Tests)
                .Include(i => i.Instruments)
                .Where(s =>s.status =="Active")
                .ToList();

            if (search != "" && search != null)
            {
                method = _dbContext.TestMethods.Include(t => t.Tests)
                .Where(m => m.MethodName.Contains(search)||m.Description.Contains(search))
                .Include(i => i.Instruments)
                .Where(s => s.status == "Active")
                .ToList();
            }
            else
            {
                method = _dbContext.TestMethods.Include(t => t.Tests)
                .Include(i => i.Instruments)
                .Where(s => s.status == "Active")
                .ToList();
            }

            return View(method);
        }

        
        [HttpGet]
        public IActionResult Insert()
        {
            ViewData["testId"] = new SelectList(_dbContext.Tests, "testId", "testfor");
            ViewData["instrumentId"] = new SelectList(_dbContext.Instruments, "instrumentId", "name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Insert(TestMethod method)
        {


            if(method.Description == null||method.MethodName == null)
            {
                ViewData["instrumentId"] = new SelectList(_dbContext.Instruments, "instrumentId", "name");
                ViewData["testId"] = new SelectList(_dbContext.Tests, "testId", "testfor");
                return View("Insert", method);
            }

            
            method.status = "Active";
            _dbContext.TestMethods.Add(method);
            _dbContext.SaveChanges();
            TempData["successAlert"] = "Record saved successfully!";
            return RedirectToAction("Index");
                       
            
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            ViewData["instrumentId"] = new SelectList(_dbContext.Instruments, "instrumentId", "name");
            ViewData["testId"] = new SelectList(_dbContext.Tests, "testId", "testfor");
            if (id == 0)
            {
                return View();
            }
            else
            {
                return View(_dbContext.TestMethods.Find(id));
            }
        }
        [HttpPost]
        public IActionResult Update(TestMethod method)
        {
            if (method.MethodName == null || method.Description == null)
            {
                ViewData["instrumentId"] = new SelectList(_dbContext.Instruments, "instrumentId", "name");
                ViewData["testId"] = new SelectList(_dbContext.Tests, "testId", "testfor");
                return View("Update", method);
            }
            
            method.status = "Active";
            _dbContext.TestMethods.Update(method);
            _dbContext.SaveChanges();
            TempData["successAlert"] = "Record updated successfully!";
            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _dbContext.TestMethods == null)
            {
                return NotFound();
            }
            var method = await _dbContext.TestMethods.FirstOrDefaultAsync(x => x.methodId == id);
            if (method == null)
            {
                return NotFound();
            }
            return View(method);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_dbContext.TestMethods == null)
            {
                return Problem("Entity set 'dbContext' is null");
            }
            var method = await _dbContext.TestMethods.FindAsync(id);
            if (method != null)
            {
                method.status = "In-Active";
                _dbContext.TestMethods.Update(method);
            }
            await _dbContext.SaveChangesAsync();
            TempData["successAlert"] = "Record deleted successfully!";
            return RedirectToAction(nameof(Index));

        }

    }
}
