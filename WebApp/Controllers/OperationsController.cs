using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Olo42.SAROM.WebApp.Models;
using Olo42.SAROM.DataAccess.Contracts;
using System;
using System.Collections.Generic;
using AutoMapper;

namespace Olo42.SAROM.WebApp.Controllers
{
  [Authorize]
  public class OperationsController : Controller
  {
    private readonly IMapper mapper;
    private readonly IOperationsRepository repository;

    public OperationsController(
      IMapper mapper,
      IOperationsRepository repository)
    {
      this.mapper = mapper;
      this.repository = repository;
    }

    // GET: Operations/Edit/5
    public IActionResult Close(string id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var operation = this.repository.Read(Guid.Parse(id));
      if (operation == null)
      {
        return NotFound();
      }
      var viewModel = this.mapper.Map<OperationViewModel>(operation);

      return View(viewModel);
    }

    // POST: Operations/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Close(string id, string closingReport)
    {
      Operation operation = this.repository.Read(Guid.Parse(id));
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
          this.repository.Update(operation);
        }
        catch (KeyNotFoundException)
        {
          return NotFound();
        }

        return RedirectToAction(nameof(Index));
      }
      var viewModel = this.mapper.Map<OperationViewModel>(operation);

      return View(viewModel);
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
    public async Task<IActionResult> Create([Bind("Id,Name,AlertDateTime")] OperationCreateModel model)
    {
      if (ModelState.IsValid)
      {
        var operation = this.mapper.Map<Operation>(model);
        this.repository.Create(operation);

        return RedirectToAction(nameof(Index));
      }

      return View(model);
    }

    // POST: Operations/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
      this.repository.Delete(Guid.Parse(id));

      return RedirectToAction(nameof(Index));
    }

    // GET: Operations/Details/5
    public async Task<IActionResult> Details(string id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var operation = this.repository.Read(Guid.Parse(id));
      // .Include(o => o.Units)
      // .FirstOrDefaultAsync(m => m.Id == id);

      if (operation == null)
      {
        return NotFound();
      }

      // var actions = _context.OperationAction
      //   .Where(a => a.OperationId == operation.Id)
      //   .OrderByDescending(a => a.Created)
      //   .Take(6)
      //   .ToList();

      // operation.OperationActions = actions;
      var viewModel = this.mapper.Map<OperationViewModel>(operation);

      return View(viewModel);
    }

    // GET: Operations/Edit/5
    public async Task<IActionResult> Edit(string id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var operation = this.repository.Read(Guid.Parse(id));
      if (operation == null)
      {
        return NotFound();
      }
      var viewModel = this.mapper.Map<OperationViewModel>(operation);

      return View(viewModel);
    }

    // POST: Operations/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
      string id,
      [Bind("Id,Name,Number,Headquarter,AlertDate,AlertTime,HeadquarterContact,PoliceContact,PoliceContactPhone,OperationLeader")]
        OperationViewModel operationViewModel)
    {
      if (id != operationViewModel.Id)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        try
        {
          var operation = this.mapper.Map<Operation>(operationViewModel);
          this.repository.Update(operation);
        }
        catch (KeyNotFoundException)
        {
          return NotFound();
        }

        return RedirectToAction(nameof(Details), new { id });
      }

      return View(operationViewModel);
    }

    // GET: Operations
    public async Task<IActionResult> Index()
    {
      var operationFiles = this.repository.Read();
      var viewModel =
        mapper.Map<IEnumerable<OperationIndexViewModel>>(operationFiles);

      return View(viewModel);
      // return View(
      //   await _context.Operation
      //     .OrderByDescending(o => o.AlertDate)
      //     .ThenByDescending(a => a.AlertTime)
      //     .ToListAsync());
    }

    public async Task<IActionResult> Print(string id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var operation = this.repository.Read(Guid.Parse(id));
      //   .Include(o => o.OperationActions)
      //   .Include(o => o.Units)
      //   .Include(o => o.MissingPeople)
      //     .ThenInclude(missingPerson => missingPerson.Documents)
      //   .FirstOrDefaultAsync(m => m.Id == id);
      if (operation == null)
      {
        return NotFound();
      }
      var viewModel = this.mapper.Map<OperationViewModel>(operation);

      return View(viewModel);
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
  }
}