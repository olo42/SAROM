using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Olo42.SAROM.WebApp.Models;

namespace Olo42.SAROM.WebApp.Controllers
{
  public class UnitsController : Controller
  {
    private readonly OperationContext _context;

    public UnitsController(OperationContext context)
    {
      _context = context;
    }

    // GET: Units
    public async Task<IActionResult> Index(string id)
    {
      ViewBag.OperationId = id;

      return View(await _context.Unit
        .OrderBy(u => u.Name)
        .Where(u => u.OperationId == id)
        .ToListAsync());
    }

    // GET: Units/Details/5
    public async Task<IActionResult> Details(string id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var unit = await _context.Unit
          .FirstOrDefaultAsync(m => m.Id == id);
      if (unit == null)
      {
        return NotFound();
      }
      ViewBag.OperationId = unit.OperationId;

      return View(unit);
    }

    // GET: Units/Create
    public IActionResult Create(string id)
    {
      ViewBag.OperationId = id;

      return View();
    }

    // POST: Units/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name,GroupLeader,PagerNumber,OperationId,AreaSeeker,DebrisSearcher,WaterLocators,Mantrailer,Helpers")] Unit unit)
    {
      if (ModelState.IsValid)
      {
        _context.Add(unit);
        await _context.SaveChangesAsync();

        OperationActionsController operationActionsController = new OperationActionsController(_context);
        await operationActionsController.Create(unit.OperationId, "Einheit eingetroffen / erfasst", unit.Name); // TODO: I18n

        return RedirectToAction(nameof(Index), new { id = unit.OperationId });
      }
      return View(unit);
    }

    // GET: Units/Edit/5
    public async Task<IActionResult> Edit(string id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var unit = await _context.Unit.FindAsync(id);
      if (unit == null)
      {
        return NotFound();
      }

      ViewBag.OperationId = unit.OperationId;

      return View(unit);
    }

    // POST: Units/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, [Bind("GroupLeader,Id,OperationId,Name,PagerNumber,AreaSeeker,DebrisSearcher,WaterLocators,Mantrailer,Helpers")] Unit unit)
    {
      if (id != unit.Id)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        try
        {
          _context.Update(unit);
          await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!UnitExists(unit.Id))
          {
            return NotFound();
          }
          else
          {
            throw;
          }
        }
        return RedirectToAction(nameof(Index), new { id = unit.OperationId });
      }
      return View(unit);
    }

    // GET: Units/Delete/5
    public async Task<IActionResult> Delete(string id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var unit = await _context.Unit
          .FirstOrDefaultAsync(m => m.Id == id);
      if (unit == null)
      {
        return NotFound();
      }

      return View(unit);
    }

    // POST: Units/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
      var unit = await _context.Unit.FindAsync(id);
      _context.Unit.Remove(unit);
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }

    private bool UnitExists(string id)
    {
      return _context.Unit.Any(e => e.Id == id);
    }
  }
}
