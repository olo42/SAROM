using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SAROM.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SAROM.Controllers
{
  public class MissingPersonsController : Controller
  {
    private readonly OperationContext _context;
    private readonly IOptions<SAROMSettings> _settings;

    public MissingPersonsController(OperationContext context, IOptions<SAROMSettings> settings)
    {
      _context = context;
      _settings = settings;
    }

    // GET: MissingPersons
    public async Task<IActionResult> Index(string id)
    {
      ViewBag.OperationId = id;
      List<MissingPerson> missingPeople = 
        await _context.MissingPerson
        .Where(m => m.OperationId == id)
        .Include(x => x.Documents)
        .ToListAsync();

      return View(missingPeople);
    }

    // GET: MissingPersons/Details/5
    public async Task<IActionResult> Details(string id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var missingPerson = await _context.MissingPerson
        .Include(x => x.Documents)
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

    public async Task<IActionResult> UploadDocument(string id)
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

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UploadDocument(IFormFile formFile, string id)
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

      if (formFile.Length > 0)
      {
        var extension = Path.GetExtension(formFile.FileName).ToLowerInvariant();
        if (string.IsNullOrEmpty(extension) || !_settings.Value.PermittedFileUpoadExtensions.Contains(extension))
        {
          ModelState.AddModelError(extension, "Forbidden file type!");
        }

        Document document = new Document(formFile);

        var documentDirectoryPath = _settings.Value.GetMissingPeoplePhysicalPath(missingPerson.OperationId, missingPerson.Id);
        Directory.CreateDirectory(documentDirectoryPath);
        var documentPath = Path.Combine(documentDirectoryPath, document.FullName);

        using (var stream = System.IO.File.Create(documentPath))
        {
          await formFile.CopyToAsync(stream);
        }

        missingPerson.Documents.Add(document);
        _context.Add(document);
        _context.Update(missingPerson);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Details), new { id = missingPerson.Id });
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