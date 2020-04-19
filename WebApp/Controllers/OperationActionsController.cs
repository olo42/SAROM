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
  public class OperationActionsController : Controller
  {
    private readonly OperationContext _context;

    public OperationActionsController(OperationContext context)
    {
      _context = context;
    }

    // GET: OperationActions
    public async Task<IActionResult> Index(string id)
    {
      ViewBag.OperationId = id;

      return View(await _context.OperationAction
        .Where(p => p.OperationId == id)
        .OrderByDescending(p => p.Created)
        .ToListAsync());
    }

    // GET: OperationActions/Details/5
    public async Task<IActionResult> Details(string id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var operationAction = await _context.OperationAction
          .FirstOrDefaultAsync(m => m.Id == id);
      if (operationAction == null)
      {
        return NotFound();
      }

      return View(operationAction);
    }

    // GET: OperationActions/Create
    public IActionResult Create(string id)
    {
      var viewModel = new OperationActionViewModel();
      viewModel.OperationId = id;
      return View(viewModel);
    }

    // POST: OperationActions/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(string operationId, string oAction, string unitName = "", string message = "")
    {
      if (ParameterCombinationIsValid(oAction, unitName, message))
      {
        var viewModel = new OperationActionViewModel
        {
          Created = DateTime.Now,
          OperationId = operationId,
          Action = oAction,
          UnitName = unitName,
          Message = message
        };

        _context.Add(viewModel);
        await _context.SaveChangesAsync();
      }

      return RedirectToAction("Details", "Operations", new { id = operationId });
    }

    private bool ParameterCombinationIsValid(string oAction, string unitName, string message)
    {
      if (string.IsNullOrEmpty(oAction) && string.IsNullOrEmpty(unitName) && string.IsNullOrEmpty(message))
      {
        return false;
      }

      if (!string.IsNullOrEmpty(oAction) && (string.IsNullOrEmpty(unitName) && string.IsNullOrEmpty(message)))
      {
        return false;
      }

      if (!string.IsNullOrEmpty(unitName) && (string.IsNullOrEmpty(oAction) && string.IsNullOrEmpty(message)))
      {
        return false;
      }

      return true;
    }

    // GET: OperationActions/Edit/5
    public async Task<IActionResult> Edit(string id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var operationAction = await _context.OperationAction.FindAsync(id);
      if (operationAction == null)
      {
        return NotFound();
      }
      return View(operationAction);
    }

    // POST: OperationActions/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, [Bind("Id,Message")] OperationActionViewModel viewModel)
    {
      if (id != viewModel.Id)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        try
        {
          _context.Update(viewModel);
          await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!OperationActionExists(viewModel.Id))
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
      return View(viewModel);
    }

    // GET: OperationActions/Delete/5
    public async Task<IActionResult> Delete(string id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var operationAction = await _context.OperationAction
          .FirstOrDefaultAsync(m => m.Id == id);
      if (operationAction == null)
      {
        return NotFound();
      }

      return View(operationAction);
    }

    // POST: OperationActions/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
      var operationAction = await _context.OperationAction.FindAsync(id);
      _context.OperationAction.Remove(operationAction);
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }

    private bool OperationActionExists(string id)
    {
      return _context.OperationAction.Any(e => e.Id == id);
    }
  }
}