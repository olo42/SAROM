using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SAROM.Models;

using System.ComponentModel.DataAnnotations;

namespace SAROM.Controllers
{
  public class OperationsController : Controller
  {
    private readonly OperationContext _context;

    public OperationsController(OperationContext context)
    {
      _context = context;
    }

    // GET: Operations
    public async Task<IActionResult> Index()
    {
      return View(await _context.Operation.ToListAsync());
    }

    // GET: Operations/Details/5
    public async Task<IActionResult> Details(string id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var operation = await _context.Operation
        .Include(o => o.OperationActions)
        .FirstOrDefaultAsync(m => m.Id == id);
      if (operation == null)
      {
        return NotFound();
      }

      return View(operation);
    }

    // GET: Operations/Create
    public IActionResult Create()
    {
      return View();
    }

    // POST: Operations/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,AlertDate,AlertTime")] Operation operation)
    {
      if (ModelState.IsValid)
      {
        _context.Add(operation);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
      }
      return View(operation);
    }

    // GET: Operations/Edit/5
    public async Task<IActionResult> Close(string id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var operation = await _context.Operation.FindAsync(id);
      if (operation == null)
      {
        return NotFound();
      }
      return View(operation);
    }

    // POST: Operations/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Close(string id, [Bind("Id,ClosingReport")] Operation operation)
    {
      if (id != operation.Id)
      {
        return NotFound();
      }

      VerifyClosingReport(operation.ClosingReport);

      if (ModelState.IsValid)
      {
        operation.IsClosed = true;
        try
        {
          _context.Update(operation);
          await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!OperationExists(operation.Id))
          {
            return NotFound();
          }
          else
          {
            throw;
          }
        }
        return RedirectToAction(nameof(Index));
      }
      return View(operation);
    }

    // POST: Operations/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
      var operation = await _context.Operation.FindAsync(id);
      _context.Operation.Remove(operation);
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }

    private bool OperationExists(string id)
    {
      return _context.Operation.Any(e => e.Id == id);
    }

    [AcceptVerbs("Get", "Post")]
    public void VerifyClosingReport(string closingReport)
    {
      string key = "ClosingReport";
      if (string.IsNullOrEmpty(closingReport))
      {
        ModelState.AddModelError(key, "Closing report cant be empty!");
      }
      else
      {
        if (ModelState.ContainsKey("{key}"))
          ModelState["{key}"].Errors.Clear();
      }
    }
  }
}