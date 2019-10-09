using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAROM.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SAROM.Controllers
{
  public class MissingPersonsController : Controller
  {
    private readonly OperationContext _context;

    public MissingPersonsController(OperationContext context)
    {
      _context = context;
    }

    // GET: MissingPersons
    public async Task<IActionResult> Index(string id)
    {
      ViewBag.OperationId = id;

      return View(await _context.MissingPerson.Where(m => m.OperationId == id).ToListAsync());
    }

    // GET: MissingPersons/Details/5
    public async Task<IActionResult> Details(string id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var missingPerson = await _context.MissingPerson
          .FirstOrDefaultAsync(m => m.Id == id);
      if (missingPerson == null)
      {
        return NotFound();
      }

      return View(missingPerson);
    }

    // GET: MissingPersons/Create
    public IActionResult Create(string id)
    {
      ViewBag.OperationId = id;

      return View();
    }

    // POST: MissingPersons/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Ailments,Clothes,DateOfBirth,EyesColour,FurtherInformation,Gender,HairColor,KnownPlaces,Medications,MissingSince,Name,OperationId,Size,SkinType,SpecialCharacteristics,Weight")] MissingPerson missingPerson)
    {
      if (ModelState.IsValid)
      {
        _context.Add(missingPerson);
        await _context.SaveChangesAsync();

        OperationActionsController operationActionsController = new OperationActionsController(_context);
        await operationActionsController.Create(missingPerson.OperationId,
          "Vermisstendaten erfasst",
          string.Empty,
          $"{missingPerson.Name}, vermisst seit {missingPerson.MissingSince}"); // TODO: I18n

        return RedirectToAction(nameof(Index), new { id = missingPerson.OperationId });
      }
      return View(missingPerson);
    }

    // GET: MissingPersons/Edit/5
    public async Task<IActionResult> Edit(string id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var missingPerson = await _context.MissingPerson.FindAsync(id);
      if (missingPerson == null)
      {
        return NotFound();
      }
      return View(missingPerson);
    }

    // POST: MissingPersons/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, [Bind("Ailments,Clothes,DateOfBirth,EyesColour,FurtherInformation,Gender,HairColor,Id,KnownPlaces,Medications,MissingSince,Name,OperationId,Size,SkinType,SpecialCharacteristics,Weight")] MissingPerson missingPerson)
    {
      if (id != missingPerson.Id)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        try
        {
          _context.Update(missingPerson);
          await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!MissingPersonExists(missingPerson.Id))
          {
            return NotFound();
          }
          else
          {
            throw;
          }
        }
        return RedirectToAction(nameof(Index), new { id = missingPerson.OperationId });
      }
      return View(missingPerson);
    }

    // GET: MissingPersons/Delete/5
    public async Task<IActionResult> Delete(string id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var missingPerson = await _context.MissingPerson
          .FirstOrDefaultAsync(m => m.Id == id);
      if (missingPerson == null)
      {
        return NotFound();
      }

      return View(missingPerson);
    }

    // POST: MissingPersons/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
      var missingPerson = await _context.MissingPerson.FindAsync(id);
      _context.MissingPerson.Remove(missingPerson);
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }

    private bool MissingPersonExists(string id)
    {
      return _context.MissingPerson.Any(e => e.Id == id);
    }
  }
}