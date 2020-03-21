using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAROM.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SAROM.Controllers
{
  public class OperationsController : Controller
  {
    private readonly OperationContext _context;

    public OperationsController(OperationContext context)
    {
      _context = context;
    }

    // GET: Operations/Edit/5
    public IActionResult Close(string id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var operation = FindOperation(id);
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
    public async Task<IActionResult> Close(string id, string closingReport)
    {
      Operation operation = FindOperation(id);
      if (operation == null)
      {
        return NotFound();
      }

      VerifyClosingReport(closingReport);

      if (ModelState.IsValid)
      {
        operation.IsClosed = true;
        operation.ClosingReport = closingReport;

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
        return RedirectToAction(nameof(Details), new { Id = operation.Id });
      }

      return View(operation);
    }

    // POST: Operations/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
      var operation = FindOperation(id);
      _context.Operation.Remove(operation);
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }

    // GET: Operations/Details/5
    public async Task<IActionResult> Details(string id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var operation = await _context.Operation
        .Include(o => o.Units)
        .FirstOrDefaultAsync(m => m.Id == id);

      if (operation == null)
      {
        return NotFound();
      }

      var actions = _context.OperationAction
        .Where(a => a.OperationId == operation.Id)
        .OrderByDescending(a => a.Created)
        .Take(6)
        .ToList();

      operation.OperationActions = actions;

      return View(operation);
    }

    // GET: Operations/Edit/5
    public async Task<IActionResult> Edit(string id)
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
    public async Task<IActionResult> Edit(
      string id,
      [Bind("Id,Name,Number,Headquarter,AlertDate,AlertTime,HeadquarterContact,PoliceContact,PoliceContactPhone,OperationLeader")] Operation operation)
    {
      if (id != operation.Id)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
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
        return RedirectToAction(nameof(Details), new { id });
      }
      return View(operation);
    }

    // GET: Operations
    public async Task<IActionResult> Index()
    {
      return View(
        await _context.Operation
          .OrderByDescending(o => o.AlertDate)
          .ThenByDescending(a => a.AlertTime)
          .ToListAsync());
    }

    public async Task<IActionResult> Print(string id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var operation = await _context.Operation
        .Include(o => o.OperationActions)
        .Include(o => o.Units)
        .Include(o => o.MissingPeople)
          .ThenInclude(missingPerson => missingPerson.Documents)
        .FirstOrDefaultAsync(m => m.Id == id);
      if (operation == null)
      {
        return NotFound();
      }

      return View(operation);
    }

    [AcceptVerbs("Get", "Post")]
    public void VerifyClosingReport(string closingReport)
    {
      string key = "ClosingReport";
      if (string.IsNullOrEmpty(closingReport))
      {
        ModelState.AddModelError(key, "Closing report can't be empty!");
      }
      else
      {
        if (ModelState.ContainsKey("{key}"))
          ModelState["{key}"].Errors.Clear();
      }
    }

    private Operation FindOperation(string id)
    {
      return _context.Operation.Find(id);
    }

    private bool OperationExists(string id)
    {
      return _context.Operation.Any(e => e.Id == id);
    }
  }
}