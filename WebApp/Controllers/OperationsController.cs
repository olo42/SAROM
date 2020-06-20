using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Olo42.SAROM.WebApp.Models;
using System;
using System.Collections.Generic;
using AutoMapper;
using Olo42.SAROM.Logic.Operations;

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
    public async Task<IActionResult> CloseAsync(string id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var operation = await this.repository.Get(id);
      if (operation == null)
      {
        return NotFound();
      }
      var viewModel = this.mapper.Map<OperationEditModel>(operation);

      return View(viewModel);
    }

    // POST: Operations/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Close(string id, string closingReport)
    {
      Operation operation = await this.repository.Get(id);
      if (operation == null)
      {
        return NotFound();
      }

      VerifyClosingReport(closingReport);

      if (ModelState.IsValid)
      {
        operation.Status = EStatus.Closed;
        operation.ClosingReport = closingReport;

        try
        {
          await this.repository.Write(operation);
        }
        catch (KeyNotFoundException)
        {
          return NotFound();
        }

        return RedirectToAction(nameof(Index));
      }
      var viewModel = this.mapper.Map<OperationEditModel>(operation);

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
    public async Task<IActionResult> Create([Bind("Name,Number,AlertDateTime")] OperationCreateModel model)
    {
      if (ModelState.IsValid)
      {
        var operation = this.mapper.Map<Operation>(model);
        await this.repository.Write(operation);

        return RedirectToAction(nameof(Index));
      }

      return View(model);
    }

    // POST: Operations/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
      await this.repository.Delete(id);

      return RedirectToAction(nameof(Index));
    }

    // GET: Operations/Details/5
    public async Task<IActionResult> Details(string id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var operation = await this.repository.Get(id);
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
      var viewModel = this.mapper.Map<OperationDetailsViewModel>(operation);

      return View(viewModel);
    }

    // GET: Operations/Edit/5
    public async Task<IActionResult> Edit(string id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var operation = await this.repository.Get(id);
      if (operation == null)
      {
        return NotFound();
      }
      var viewModel = this.mapper.Map<OperationEditModel>(operation);

      return View(viewModel);
    }

    // POST: Operations/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
      string id,
      [Bind("Id,Name,Number,Headquarter,HeadquarterContact,PoliceContact,PoliceContactPhone,OperationLeader")]
        OperationEditModel editModel)
    {
      if (id != editModel?.Id)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        Operation operation = null;
        try
        {
          operation = await this.repository.Get(editModel.Id);
        }
        catch (KeyNotFoundException)
        {
          return NotFound();
        }

        operation.Name = editModel.Name;
        operation.Number = editModel.Number;
        operation.PoliceContact = editModel.PoliceContact;
        operation.PoliceContactPhone = editModel.PoliceContactPhone;
        operation.Headquarter = editModel.Headquarter;
        operation.HeadquarterContact = editModel.HeadquarterContact;
        operation.OperationLeader = editModel.OperationLeader;

        await this.repository.Write(operation);

        return RedirectToAction(nameof(Details), new { id });
      }

      return View(editModel);
    }

    // GET: Operations
    public async Task<IActionResult> Index()
    {
      var operations = await this.repository.Get();
      var viewModel =
        mapper.Map<IEnumerable<OperationIndexViewModel>>(operations);
      viewModel = viewModel.OrderByDescending(x=>x.Alert);

      return View(viewModel);
    }

    public async Task<IActionResult> Print(string id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var operation = await this.repository.Get(id);
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