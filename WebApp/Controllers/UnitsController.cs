using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Olo42.SAROM.WebApp.Models;
using Olo42.SAROM.Logic.Operations;
using AutoMapper;
using System.Collections.Generic;
using System;

namespace Olo42.SAROM.WebApp.Controllers
{
  public class UnitsController : Controller
  {
    private readonly IMapper mapper;
    private readonly IUnitsRepository repository;

    public UnitsController(IMapper mapper, IUnitsRepository repository)
    {
      this.mapper = mapper;
      this.repository = repository;
    }

    // GET: Units
    public async Task<IActionResult> Index(string id)
    {
      ViewBag.OperationId = id;
      var units = await repository.Get(id);
      var viewModel = this.mapper.Map<IEnumerable<UnitViewModel>>(units);

      viewModel = viewModel.OrderByDescending(x => x.Name);

      return View(viewModel);
    }

    // GET: Units/Details/5
    [HttpGet("Details/{operdationId}/{id}")]
    public async Task<IActionResult> Details(string operationId, string id)
    {
      var unit = await repository.Get(operationId, id);
      ViewBag.OperationId = operationId;

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
    public async Task<IActionResult> Create([Bind("Name,GroupLeader,PagerNumber,OperationId,AreaSeeker,DebrisSearcher,WaterLocators,Mantrailer,Helpers")] UnitViewModel unitViewModel)
    {
      if (ModelState.IsValid)
      {
        var unit = this.mapper.Map<Unit>(unitViewModel);
        await repository.Write(unit.OperationId, unit);

        // OperationActionsController operationActionsController = new OperationActionsController(repository);
        // await operationActionsController.Create(unit.OperationId, "Einheit eingetroffen / erfasst", unit.Name); // TODO: I18n

        return RedirectToAction(nameof(Index), new { id = unit.OperationId });
      }
      return View(unitViewModel);
    }

    // GET: Units/Edit/5
    // [HttpGet("Edit/{id}/{operationId}")]
    public async Task<IActionResult> Edit(string operationId, string id)
    {
      var unit = await this.repository.Get(operationId, id);
      ViewBag.OperationId = unit.OperationId;

      var viewModel = this.mapper.Map<UnitViewModel>(unit);

      return View(viewModel);
    }

    // POST: Units/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, [Bind("GroupLeader,Id,OperationId,Name,PagerNumber,AreaSeeker,DebrisSearcher,WaterLocators,Mantrailer,Helpers")] UnitViewModel unitViewModel)
    {
      if (ModelState.IsValid)
      {
        var unit = this.mapper.Map<Unit>(unitViewModel);  
        await this.repository.Write(unit.OperationId, unit);

        return RedirectToAction(nameof(Index), new { id = unit.OperationId });
      }
      return View(unitViewModel);
    }

    // GET: Units/Delete/5
    // public async Task<IActionResult> Delete(string id)
    // {
    //   if (id == null)
    //   {
    //     return NotFound();
    //   }

    //   var unit = await repository.Unit
    //       .FirstOrDefaultAsync(m => m.Id == id);
    //   if (unit == null)
    //   {
    //     return NotFound();
    //   }

    //   return View(unit);
    // }

    // // POST: Units/Delete/5
    // [HttpPost, ActionName("Delete")]
    // [ValidateAntiForgeryToken]
    // public async Task<IActionResult> DeleteConfirmed(string id)
    // {
    //   var unit = await repository.Unit.FindAsync(id);
    //   repository.Unit.Remove(unit);
    //   await repository.SaveChangesAsync();
    //   return RedirectToAction(nameof(Index));
    // }
  }
}